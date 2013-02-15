using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.CodeGeneration;

namespace TigerCompiler.AST.Nodes.Operations.Arithmetic
{
    class MultiplyNode : ArithmeticOperationNode
    {
        public MultiplyNode(IToken payload) : base(payload)
        {
        }

        public override void GenerateCode(CodeGenerator cg)
        {
            base.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Mul);
        }
    }
}
