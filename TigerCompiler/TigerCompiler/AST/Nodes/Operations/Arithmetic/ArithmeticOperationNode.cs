using Antlr.Runtime;
using TigerCompiler.Semantic;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Operations.Arithmetic
{
    public abstract class ArithmeticOperationNode : BinaryOperationNode
    {
        protected ArithmeticOperationNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            base.CheckSemantics(scope,report);

            report.Assert(this, LeftOperand.ReturnType == TypeInfo.Int, "Left operand must be an integer.");
            report.Assert(this, RightOperand.ReturnType == TypeInfo.Int, "Right operand must be an integer.");

            ReturnType = TypeInfo.Int;
        }
    }
}
