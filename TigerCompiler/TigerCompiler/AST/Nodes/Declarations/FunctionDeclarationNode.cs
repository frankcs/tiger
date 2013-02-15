using System;
using System.Collections.Generic;
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

        private string FunctionName { get { return Children[0].Text; } }

        public Scope FunctionScope
        {
            get { return ((FunctionInfo) Scope.ResolveVarOrFunction(FunctionName)).FunctionScope; }
        } 

        private TypeInfo FunctionReturnType
        {
            get
            {
                if (Children[2] is TypeIDNode)
                    return Scope.ResolveType((Children[2] as TypeIDNode).Text);
                return TypeInfo.Void;
            }
        }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            // - Add the function declaration inside this block to the current scope.
            // - Create a child scope for every function.

            ParameterTypesNode.CheckSemantics(scope, report);

            Scope = scope;

            var newFunc = scope.DefineFunction(FunctionName, FunctionReturnType);
            foreach (KeyValuePair<string, TypeInfo> info in ParameterTypesNode.Parameters)
                newFunc.AddParameter(info.Key, info.Value);

            // The function body semantic check will be done by the parent block.
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            var funcinfo= (FunctionInfo) Scope.ResolveVarOrFunction(FunctionName);
            var parameters = funcinfo.Parameters.Values;
            //funcinfo.ILMethod = cg.CreateFunction(FunctionReturnType.GetILType(), funcinfo.Parameters.Keys);
            //TODO
        }
        

    }
}
