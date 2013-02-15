using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Helpers
{
    class IdNode : HelperNode
    {
        public IdNode(IToken payload) : base(payload)
        {
        }
    }
}
