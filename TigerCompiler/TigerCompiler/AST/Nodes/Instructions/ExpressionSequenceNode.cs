using Antlr.Runtime;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Instructions
{
    class ExpressionSequenceNode : InstructionNode
    {
        public ExpressionSequenceNode(IToken payload) : base(payload)
        {
        }

        public bool HasBreak { get; set; }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            ReturnType = HasBreak || Children == null ? TypeInfo.Void : ((ASTNode) Children[Children.Count - 1]).ReturnType;
        }
    }
}
