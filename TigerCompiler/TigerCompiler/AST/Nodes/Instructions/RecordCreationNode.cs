using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Instructions
{
    /// <summary>
    /// ^(RECORD_CREATION ID field_list?)
    /// (ID EQUAL! expr) (','! ID EQUAL! expr)*;
    /// </summary>
    class RecordCreationNode : InstructionNode
    {
        public RecordCreationNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);



        }
    }
}
