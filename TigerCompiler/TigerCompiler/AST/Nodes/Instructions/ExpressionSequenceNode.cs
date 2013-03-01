using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Instructions
{
    class ExpressionSequenceNode : InstructionNode
    {
        public ExpressionSequenceNode(IToken payload) : base(payload)
        {
        }

        public bool HasBreak { get; set; }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            ReturnType = HasBreak || Children == null ? TypeInfo.Void : ((ASTNode) Children[Children.Count - 1]).ReturnType;
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            //base.GenerateCode(cg);
            if(Children!=null)
                for (int i = 0; i < Children.Count; i++)
                {
                    ((ASTNode)Children[i]).GenerateCode(cg);
                    //if yup are not the last and return value pop it from the stack
                    if (((ASTNode)Children[i]).ReturnType != TypeInfo.Void && i!=Children.Count-1)
                        cg.IlGenerator.Emit(OpCodes.Pop);
                }
        }
    }
}
