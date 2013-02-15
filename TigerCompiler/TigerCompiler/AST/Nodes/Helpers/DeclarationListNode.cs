using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Helpers
{
    internal class DeclarationListNode : HelperNode
    {
        public DeclarationListNode(IToken payload) : base(payload)
        {
        }
    }
}