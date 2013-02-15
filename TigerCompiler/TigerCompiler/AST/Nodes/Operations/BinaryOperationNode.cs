using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Operations
{
    public abstract class BinaryOperationNode : OperationNode
    {
        public BinaryOperationNode(IToken payload) : base(payload)
        {
        }

        public ASTNode LeftOperand
        {
            get { return (ASTNode)Children[0]; }
        }

        public ASTNode RightOperand
        {
            get { return (ASTNode)Children[1]; }
        }

    }
}
