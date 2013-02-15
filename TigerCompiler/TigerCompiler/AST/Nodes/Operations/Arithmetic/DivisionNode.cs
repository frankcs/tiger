using System.Reflection.Emit;
using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Operations.Arithmetic
{
    class DivisionNode : ArithmeticOperationNode
    {
        public DivisionNode(IToken payload) : base(payload)
        {
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            base.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Div);
        }
    }
}
