using Antlr.Runtime;
using TigerCompiler.Semantic;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Operations.Logical
{
    abstract public class ComparisonNode : BinaryOperationNode
    {
        protected ComparisonNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            base.CheckSemantics(scope,report);

            report.Assert(this, (LeftOperand.ReturnType == TypeInfo.Int && RightOperand.ReturnType == TypeInfo.Int) 
                || (RightOperand.ReturnType == TypeInfo.String && LeftOperand.ReturnType == TypeInfo.String), "Return types must be both int or string for comparisons.");

            ReturnType = TypeInfo.Int;
        }

    }
}
