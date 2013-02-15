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

            scope.DefineAlias(NewTypeNode.TypeName, TargetTypeNode.TypeName);
        }

        TypeIDNode TargetTypeNode
        {
            get { return Children[1] as TypeIDNode; }
        }
    }
}
