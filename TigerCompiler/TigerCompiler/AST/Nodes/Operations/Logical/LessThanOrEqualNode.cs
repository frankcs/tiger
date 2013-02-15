using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Operations.Logical
{
    class LessThanOrEqualNode : ComparisonNode
    {
        public LessThanOrEqualNode(IToken payload) : base(payload)
        {
        }
    }
}
