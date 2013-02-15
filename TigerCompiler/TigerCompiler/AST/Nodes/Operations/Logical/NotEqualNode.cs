using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Operations.Logical
{
    class NotEqualNode : EqualityNode
    {
        public NotEqualNode(IToken payload) : base(payload)
        {
        }
    }
}
