using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Instructions
{
    class RecordCreationNode : InstructionNode
    {
        public RecordCreationNode(IToken payload) : base(payload)
        {
        }
    }
}
