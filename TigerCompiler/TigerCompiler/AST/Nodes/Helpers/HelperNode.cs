using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Helpers
{
    public abstract class HelperNode : ASTNode
    {
        protected HelperNode(IToken payload) : base(payload)
        {
        }

    }
}
