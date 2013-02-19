using System;
using System.Diagnostics;
using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Helpers;
using TigerCompiler.Semantic;
using TigerCompiler.Semantic.Symbols;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Declarations
{
    /// <summary>
    /// 'var' ID (':' type_id)? ASSIGN expr -> ^(VAR_DECL ID type_id? expr);
    /// </summary>
    class VariableDeclarationNode : DeclarationNode
    {
        public VariableDeclarationNode(IToken payload) : base(payload)
        {
        }

        private ASTNode VariableID
        {
            get { return (ASTNode) Children[0]; }
        }

        private TypeIDNode TypeID
        {
            get { return (TypeIDNode)((Children[1] is TypeIDNode) ? Children[1] : null); }
        }

        private ASTNode InitialValue
        {
            get { return(ASTNode) (TypeID == null ? Children[1] : Children[2]); }
        }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            base.CheckSemantics(scope,report);



            if (TypeID == null)
            {
                report.Assert(this, InitialValue.ReturnType != TypeInfo.Unknown,
                              "The variable type cannot be inferred from usage. Specify the type explicitly.");
                report.Assert(this, InitialValue.ReturnType != TypeInfo.Void,
                              "Initialization value expression must return a value.");
                ReturnType = InitialValue.ReturnType;
            }
            else
            {
                TypeInfo type = (scope.ResolveType(TypeID.TypeName));
                if (report.Assert(this, !ReferenceEquals(type, null),"Cannot find type {0} in variable {1}'s declaration.",TypeID.TypeName,VariableID.Text))
                {
                    report.Assert(this, type == InitialValue.ReturnType,
                              "The initial expression type \"{0}\" does not match with the specified type {1}.",
                              InitialValue.ReturnType, TypeID.TypeName);
                    ReturnType = type;
                }
            }

            if (report.Assert(this, !scope.IsDefinedInCurrentScopeAsVarOrFunc(VariableID.Text),
                          "A function or variable named {0} is already defined in the current scope.", VariableID.Text))
                scope.DefineVariable(VariableID.Text, ReturnType);

            VariableID.CheckSemantics(scope,report);
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            var varinfo = (VariableInfo)((IdNode) VariableID).ReferencedThing;
            varinfo.ILLocalVariable = cg.IlGenerator.DeclareLocal(varinfo.VariableType.GetILType());
//            varinfo.IsHidingAnother = false;
            InitialValue.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Stloc,varinfo.ILLocalVariable);
             
        }
    }
}
