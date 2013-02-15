using System.Reflection.Emit;
using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Operations.Arithmetic
{
    class PlusNode : ArithmeticOperationNode
    {
        public PlusNode(IToken payload) : base(payload)
        {
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            base.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Add);
        }
    }
}
