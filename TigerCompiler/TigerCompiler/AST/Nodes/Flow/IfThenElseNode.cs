using System.Reflection.Emit;
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

            ReturnType = ThenExpression.ReturnType;
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

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            Label elseeval = cg.IlGenerator.DefineLabel();
            Label endofif = cg.IlGenerator.DefineLabel();

            IfCondition.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Brfalse, elseeval);
            ThenExpression.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Br, endofif);
            cg.IlGenerator.MarkLabel(elseeval);
            ElseExpression.GenerateCode(cg);
            cg.IlGenerator.MarkLabel(endofif);
        }
    }
}
