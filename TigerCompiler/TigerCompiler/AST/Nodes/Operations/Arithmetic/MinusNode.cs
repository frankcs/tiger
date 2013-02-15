using System.Reflection.Emit;
using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Operations.Arithmetic
{
    class MinusNode : ArithmeticOperationNode
    {
        public MinusNode(IToken payload) : base(payload)
        {
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            base.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Sub);
        }
    }
}
