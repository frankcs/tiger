using System.Reflection.Emit;
using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Operations.Logical
{
    class AndNode : LogicalOperationNode
    {
        public AndNode(IToken payload) : base(payload)
        {
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            Label retfalse = cg.IlGenerator.DefineLabel();
            Label end = cg.IlGenerator.DefineLabel();
            
            LeftOperand.GenerateCode(cg);
            //if eval its cero then ret 0 
            cg.IlGenerator.Emit(OpCodes.Brfalse,retfalse);
            RightOperand.GenerateCode(cg);
            //if eval its cero then ret 0 
            cg.IlGenerator.Emit(OpCodes.Brfalse,retfalse);
            //ret 1 and jump to end
            cg.IlGenerator.Emit(OpCodes.Ldc_I4_1);
            cg.IlGenerator.Emit(OpCodes.Br,end);

            //ret 0
            cg.IlGenerator.MarkLabel(retfalse);
            cg.IlGenerator.Emit(OpCodes.Ldc_I4_0);

            cg.IlGenerator.MarkLabel(end);


        }
    }
}
