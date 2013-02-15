using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Declarations.Types;

namespace TigerCompiler.AST.Nodes.Declarations.Blocks
{
    internal class TypeDeclarationBlockNode : ASTNode
    {
        public TypeDeclarationBlockNode(IToken payload): base(payload) {}

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            foreach (TypeDeclarationNode node in Children)
                node.ResolveReferencedTypes(scope);
        }
    }
}