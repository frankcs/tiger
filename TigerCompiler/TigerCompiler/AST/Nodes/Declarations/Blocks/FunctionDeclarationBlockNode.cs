using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Declarations.Blocks
{
    internal class FunctionDeclarationBlockNode : ASTNode
    {
        public FunctionDeclarationBlockNode(IToken payload)
            : base(payload)
        {

        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            // Delegated to the FunctionDeclarationNodes:
                // - Add every function declaration inside this block to the current scope.
                // - Create a child scope for every function.
            base.CheckSemantics(scope, report);

            // - Check the semantics of the bodies of the functions.
            foreach (FunctionDeclarationNode declarationNode in Children)
                declarationNode.FunctionBody.CheckSemantics(declarationNode.FunctionScope,report);
        }
    }
}