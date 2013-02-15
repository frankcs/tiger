using Antlr.Runtime;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Operations.Logical
{
    public abstract class EqualityNode : BinaryOperationNode
    {
        protected EqualityNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            report.Assert(this,LeftOperand.ReturnType == RightOperand.ReturnType, "Equality comparisons are only allowed between elements of the same type.");

            ReturnType = TypeInfo.Int;
        }
    }
}
