using Antlr.Runtime;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Operations.Logical
{
    public abstract class LogicalOperationNode : BinaryOperationNode
    {
        protected LogicalOperationNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            report.Assert(this,LeftOperand.ReturnType == TypeInfo.Int && RightOperand.ReturnType == TypeInfo.Int, "Both operands must be integers.");

            ReturnType = TypeInfo.Int;
        }
    }
}
