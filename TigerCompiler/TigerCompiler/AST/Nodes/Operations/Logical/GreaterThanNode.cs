using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Operations.Logical
{
    class GreaterThanNode : ComparisonNode
    {
        public GreaterThanNode(IToken payload) : base(payload)
        {
        }
    }
}
