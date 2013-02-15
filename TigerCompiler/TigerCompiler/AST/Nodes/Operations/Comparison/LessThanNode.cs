using System.Reflection;
using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Operations.Comparison
{
    class LessThanNode : ComparisonNode
    {
        public LessThanNode(IToken payload) : base(payload)
        {
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            base.GenerateCode(cg);
            if (LeftOperand.ReturnType == TypeInfo.Int)
            {
                cg.IlGenerator.Emit(OpCodes.Clt);
            }
            else
            {
                Label ret1 = cg.IlGenerator.DefineLabel();
                Label end = cg.IlGenerator.DefineLabel();

                MethodInfo strcmp = typeof(string).GetMethod("CompareOrdinal", new[] { typeof(string), typeof(string) });
                //cmp with zero, if lesser return 1 
                cg.IlGenerator.Emit(OpCodes.Call, strcmp);
                cg.IlGenerator.Emit(OpCodes.Ldc_I4_0);
                cg.IlGenerator.Emit(OpCodes.Blt, ret1);
                cg.IlGenerator.Emit(OpCodes.Ldc_I4_0);
                cg.IlGenerator.Emit(OpCodes.Br, end);
                cg.IlGenerator.MarkLabel(ret1);
                cg.IlGenerator.Emit(OpCodes.Ldc_I4_1);
                cg.IlGenerator.MarkLabel(end);
            }
        }
    }
}
