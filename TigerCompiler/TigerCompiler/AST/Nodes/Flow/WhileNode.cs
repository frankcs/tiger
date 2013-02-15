using Antlr.Runtime;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Flow
{
    /// <summary>
    /// WHILE condition=expr 'do' something=expr -> ^(WHILE $condition $something)
    /// </summary>
    class WhileNode : FlowControlNode
    {
        public WhileNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            report.Assert(ConditionExpression, ConditionExpression.ReturnType == TypeInfo.Int,"The while condition must return an int.");
            report.Assert(BodyExpression, BodyExpression.ReturnType == TypeInfo.Void, "The while expression must not return a value.");

            ReturnType = TypeInfo.Void;
        }

        ASTNode ConditionExpression
        {
            get { return (ASTNode) Children[0]; }
        }

        ASTNode BodyExpression
        {
            get { return (ASTNode)Children[1]; }
        }

    }
}
