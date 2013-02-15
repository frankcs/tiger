using Antlr.Runtime;
using TigerCompiler.Semantic;

namespace TigerCompiler.AST.Nodes.Operations
{
    abstract public class OperationNode : ASTNode
    {
        protected OperationNode(IToken payload) : base(payload)
        {
        }

    }
}
