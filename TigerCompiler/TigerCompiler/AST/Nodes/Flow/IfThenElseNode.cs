using Antlr.Runtime;
using TigerCompiler.Semantic;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Flow
{
    /// <summary>
    /// ('if' expr 'then' expr 'else') => ('if' ifx=expr 'then' thenx=expr 'else' elsex=expr) -> ^(IF_THEN_ELSE $ifx $thenx $elsex)
    /// </summary>
    class IfThenElseNode : FlowControlNode
    {
        public IfThenElseNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            report.Assert(this,IfCondition.ReturnType == TypeInfo.Int,"An if expression must return int, not {0}.",IfCondition.ReturnType);
            report.Assert(this,ThenExpression.ReturnType == ElseExpression.ReturnType,"The if and else expression types do not match ({0};{1}).",ThenExpression.ReturnType,ElseExpression.ReturnType);

            ReturnType = IfCondition.ReturnType;
        }

        private ASTNode IfCondition
        {
            get { return (ASTNode)Children[0]; }
        }

        private ASTNode ThenExpression
        {
            get { return (ASTNode)Children[1]; }
        }

        private ASTNode ElseExpression
        {
            get { return (ASTNode)Children[2]; }
        }
    }
}
