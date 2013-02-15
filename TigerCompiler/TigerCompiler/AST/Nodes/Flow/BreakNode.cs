using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Declarations;
using TigerCompiler.AST.Nodes.Helpers;
using TigerCompiler.AST.Nodes.Instructions;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Flow
{
    class BreakNode : FlowControlNode
    {
        public BreakNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            var current = (ASTNode) Parent;

            while (current!=null)
            {
                if (current is ForNode || current is WhileNode)
                {
                    EnclosingForOrWhile = (FlowControlNode) current;
                    break;
                }
                if (current is FunctionDeclarationNode || current is ProgramNode)
                {
                    //TODO: Is this right?
                    break;
                }
                if (current is ExpressionSequenceNode)
                {
                    ((ExpressionSequenceNode)current).HasBreak = true;
                }
                current = current.Parent as ASTNode;
            }

            report.Assert(this,EnclosingForOrWhile != null,"Invalid break.");

            ReturnType = TypeInfo.Void;
        }

        private FlowControlNode EnclosingForOrWhile { get; set; }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            cg.IlGenerator.Emit(OpCodes.Br, EnclosingForOrWhile.EndofCicle);
        }
    }
}
