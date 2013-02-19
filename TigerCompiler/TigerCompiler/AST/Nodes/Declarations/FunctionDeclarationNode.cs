using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Helpers;
using TigerCompiler.Semantic;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Declarations
{
    /// <summary>
    /// function_declaration: 'function' ID '(' type_fields? ')' (':' type_id)? EQUAL expr 
    ///     -> ^(FUNCTION_DECL ID ^(TYPE_FIELDS type_fields?) type_id? expr);
    /// type_fields:	type_field (','! type_field)*;
    /// type_field:	ID ':' type_id -> ^(ID type_id);
    /// </summary>
    class FunctionDeclarationNode : DeclarationNode
    {
        public FunctionDeclarationNode(IToken payload) : base(payload){}

        TypeFieldsNode ParameterTypesNode
        {
            get { return Children[1] as TypeFieldsNode; }
        }

        public ASTNode FunctionBody
        {
            get
            {
                if (FunctionReturnType == TypeInfo.Void)
                    return (ASTNode)Children[2];
                return (ASTNode) Children[3];
            }
        }

        public string FunctionName { get { return Children[0].Text; } }

        public Scope FunctionScope
        {
            get { return ((FunctionInfo) Scope.ResolveVarOrFunction(FunctionName)).FunctionScope; }
        } 

        public TypeInfo FunctionReturnType
        {
            get
            {
                var typeIDNode = Children[2] as TypeIDNode;
                return typeIDNode != null ? Scope.ResolveType(typeIDNode.TypeName) : TypeInfo.Void;
            }
        }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            // - Add the function declaration inside this block to the current scope.
            // - Create a child scope for every function.

            ParameterTypesNode.CheckSemantics(scope, report);

            Scope = scope;

            var func = scope.ResolveVarOrFunction(FunctionName) as FunctionInfo;
            if (report.Assert(this, func == null || func.IsReadOnly, "Cannot overwrite {0} function.", FunctionName) && 
                report.Assert(this, !scope.IsDefinedInCurrentScopeAsVarOrFunc(FunctionName),
                                  "A function or variable named {0} is already defined in the current scope.", FunctionName))
            {
                var newFunc = scope.DefineFunction(FunctionName, FunctionReturnType);
                foreach (KeyValuePair<string, TypeInfo> info in ParameterTypesNode.Parameters)
                    newFunc.AddParameter(info.Key, info.Value);
            }
            // The function body semantic check will be done by the parent block.
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            var funcinfo= (FunctionInfo) Scope.ResolveVarOrFunctionOnCodeGen(FunctionName);
            var parameterstypes =
                (from paramstypeinfos in funcinfo.Parameters.Values select paramstypeinfos.GetILType()).ToArray();

            //create the function in the module
            funcinfo.ILMethod = cg.CreateFunction(FunctionReturnType.GetILType(), parameterstypes);
            //get it's ILGen
            var innergenerator = funcinfo.ILMethod.GetILGenerator();
            cg.EnterGenerationScope(innergenerator);

            var paramarr = funcinfo.Parameters.ToArray();

            for (int i = 0; i < paramarr.Length; i++)
            {
                var parameter = paramarr[i];
                //get the variable for the param
                var paramvarinfo = (VariableInfo)funcinfo.FunctionScope.ResolveVarOrFunctionOnCodeGen(parameter.Key);
                //link the var with is Ilvariable
                paramvarinfo.ILLocalVariable = cg.IlGenerator.DeclareLocal(parameter.Value.GetILType());
                //load the argument
                cg.IlGenerator.Emit(OpCodes.Ldarg,i);
                cg.IlGenerator.Emit(OpCodes.Stloc,paramvarinfo.ILLocalVariable);
            }
            FunctionBody.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Ret);
            cg.LeaveGenerationScope();
        }
        

    }
}
