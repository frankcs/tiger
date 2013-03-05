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
            if (Children != null)
            {
                for (int i = 0; i < ChildCount - 1; i++)
                {
                    var child = Children[i] as ASTNode;
                    child.GenerateCode(cg);
                    if (child.ReturnType != TypeInfo.Void)
                        cg.IlGenerator.Emit(OpCodes.Pop);
                }
                (Children[ChildCount - 1] as ASTNode).GenerateCode(cg);
            }

        }
    }
}
