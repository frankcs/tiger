using Antlr.Runtime;
using System.Reflection.Emit;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Flow
{
    class IfThenNode : FlowControlNode
    {
        public IfThenNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            ReturnType = TypeInfo.Void;
        }

        public ASTNode IfCondition
        {
            get { return (ASTNode) Children[0]; }
        }

        public ASTNode ThenExpresion
        {
            get { return (ASTNode)Children[1]; }
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
