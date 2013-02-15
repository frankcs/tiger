using System.Reflection.Emit;
using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Operations.Logical
{
    class OrNode : LogicalOperationNode
    {
        public OrNode(IToken payload) : base(payload)
        {

        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            Label rettrue = cg.IlGenerator.DefineLabel();
            Label end = cg.IlGenerator.DefineLabel();

            LeftOperand.GenerateCode(cg);
            //if eval its 1 then ret 1 
            cg.IlGenerator.Emit(OpCodes.Brtrue, rettrue);
            RightOperand.GenerateCode(cg);
            //if eval its 1 then ret 1 
            cg.IlGenerator.Emit(OpCodes.Brfalse, rettrue);
            //ret 0 and jump to end
            cg.IlGenerator.Emit(OpCodes.Ldc_I4_0);
            cg.IlGenerator.Emit(OpCodes.Br, end);

            //ret 0
            cg.IlGenerator.MarkLabel(rettrue);
            cg.IlGenerator.Emit(OpCodes.Ldc_I4_1);

            cg.IlGenerator.MarkLabel(end);
        }
    }
}
