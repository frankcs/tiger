using System;
using System.Diagnostics;
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

        private ASTNode TypeID
        {
            get { return (ASTNode)((Children[1] is TypeIDNode)?Children[1]:null); } // TODO: Is this ok?
        }

        private ASTNode InitialValue
        {
            get { return(ASTNode) (TypeID == null ? Children[1] : Children[2]); }
        }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            base.CheckSemantics(scope,report);

            if (TypeID != null)
            {
                TypeInfo type = (scope.ResolveType(TypeID.Text));
                report.Assert(this,InitialValue.ReturnType == type,"The initialization expression type ({0}) does not match with the specified type {1}.",InitialValue.ReturnType,TypeID.Text);
                ReturnType = type;
            }
            else
            {
                report.Assert(this,InitialValue.ReturnType != TypeInfo.Unknown,"The variable type cannot be inferred from usage. Specify the type explicitly.");
                ReturnType = InitialValue.ReturnType;
                Debug.Assert(ReturnType != null);
            }

            scope.DefineVariable(VariableID.Text, ReturnType);
        }
    }
}
