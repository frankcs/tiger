using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Declarations
{
    public abstract class DeclarationNode : ASTNode
    {
        public DeclarationNode(IToken payload) : base(payload)
        {
        }
    }
}
