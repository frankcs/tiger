using Antlr.Runtime;
using System.Reflection.Emit;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Flow
{
    /// <summary>
    /// 'if' ifx=expr 'then' elsex=expr -> ^(IF_THEN $ifx $elsex)
    /// </summary>
    class IfThenNode : FlowControlNode
    {
        public IfThenNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            report.Assert(this, IfExpression.ReturnType == TypeInfo.Int,"An if expression must return an integer value.");
            report.Assert(this,ThenExpression.ReturnType == TypeInfo.Void,"If-then must not return a value.");

            ReturnType = TypeInfo.Void;
        }

        private ASTNode IfExpression
        {
            get { return Children[0] as ASTNode; }
        }

        private ASTNode ThenExpression
        {
            get { return Children[1] as ASTNode; }
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            Label endofif = cg.IlGenerator.DefineLabel();
            
            IfCondition.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Brfalse, endofif);
            ThenExpresion.GenerateCode(cg);
            cg.IlGenerator.MarkLabel(endofif);
        }
    }
}
