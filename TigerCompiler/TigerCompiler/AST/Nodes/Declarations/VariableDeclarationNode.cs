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
                ReturnType = InitialValue.ReturnType;
                Debug.Assert(ReturnType != null);
            }
            else
            {
                TypeInfo type = (scope.ResolveType(TypeID.TypeName));
                if (report.Assert(this, !ReferenceEquals(type, null),"Cannot find type {0} in variable {1}'s declaration.",TypeID.TypeName,VariableID.Text))
                {
                    report.Assert(this, InitialValue.ReturnType == type,
                              "The initial expression type \"{0}\" does not match with the specified type {1}.",
                              InitialValue.ReturnType, TypeID.TypeName);
                    ReturnType = type;
                }
            }

            scope.DefineVariable(VariableID.Text, InitialValue.ReturnType);
        }
    }
}
