using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Operations.Constants
{
    public abstract class ConstantNode : InstructionNode
    {
        protected ConstantNode(IToken payload) : base(payload)
        {
        }
    }
}
