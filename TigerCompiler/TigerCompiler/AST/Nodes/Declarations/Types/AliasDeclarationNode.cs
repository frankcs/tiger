using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Helpers;

namespace TigerCompiler.AST.Nodes.Declarations.Types
{
    class AliasDeclarationNode : TypeDeclarationNode
    {
        public AliasDeclarationNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            if (report.Assert(this, !scope.IsDefinedInCurrentScopeAsType(NewTypeNode.TypeName), "Type {0} is already defined in the current scope.", NewTypeNode.TypeName))

            scope.DefineAlias(NewTypeNode.TypeName, TargetTypeNode.TypeName);
        }

        TypeIDNode TargetTypeNode
        {
            get { return Children[1] as TypeIDNode; }
        }
    }
}
