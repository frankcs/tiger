using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Operations.Logical
{
    class OrNode : LogicalOperationNode
    {
        public OrNode(IToken payload) : base(payload)
        {
        }
    }
}
