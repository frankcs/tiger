using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Operations.Logical
{
    class GreaterThanOrEqualNode : ComparisonNode
    {
        public GreaterThanOrEqualNode(IToken payload) : base(payload)
        {
        }
    }
}
