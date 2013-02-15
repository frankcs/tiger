using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Operations.Logical
{
    class LessThanNode : ComparisonNode
    {
        public LessThanNode(IToken payload) : base(payload)
        {
        }
    }
}
