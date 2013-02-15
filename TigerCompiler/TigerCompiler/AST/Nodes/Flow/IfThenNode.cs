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

            report.Assert(this, IfCondition.ReturnType == TypeInfo.Int, "An if expression must return an integer value.");
            report.Assert(this,ThenExpression.ReturnType == TypeInfo.Void,"If-then must not return a value.");

            ReturnType = TypeInfo.Void;
        }

        public ASTNode IfCondition
        {
            get { return (ASTNode) Children[0]; }
        }

        public ASTNode ThenExpression
        {
            get { return (ASTNode)Children[1]; }
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            Label endofif = cg.IlGenerator.DefineLabel();
            
            IfCondition.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Brfalse, endofif);
            ThenExpression.GenerateCode(cg);
            cg.IlGenerator.MarkLabel(endofif);
        }
    }
}
