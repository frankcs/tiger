using System.Reflection;
using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Operations.Comparison
{
    class GreaterThanOrEqualNode : ComparisonNode
    {
        public GreaterThanOrEqualNode(IToken payload) : base(payload)
        {
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            base.GenerateCode(cg);
            if (LeftOperand.ReturnType == TypeInfo.Int)
            {
                Label return1 = cg.IlGenerator.DefineLabel();
                Label end = cg.IlGenerator.DefineLabel();

                //if greater or equal return 1 if not 0
                cg.IlGenerator.Emit(OpCodes.Bge, return1);
                //compare and end
                cg.IlGenerator.Emit(OpCodes.Ldc_I4_0);
                cg.IlGenerator.Emit(OpCodes.Br,end);

                cg.IlGenerator.MarkLabel(return1);
                cg.IlGenerator.Emit(OpCodes.Ldc_I4_1);
                cg.IlGenerator.MarkLabel(end);

            }
            else
            {
                Label ret1 = cg.IlGenerator.DefineLabel();
                Label end = cg.IlGenerator.DefineLabel();

                MethodInfo strcmp = typeof(string).GetMethod("CompareOrdinal", new[] { typeof(string), typeof(string) });
                //cmp with zero, if greater return 1 
                cg.IlGenerator.Emit(OpCodes.Call, strcmp);
                cg.IlGenerator.Emit(OpCodes.Ldc_I4_0);
                cg.IlGenerator.Emit(OpCodes.Bge, ret1);
                cg.IlGenerator.Emit(OpCodes.Ldc_I4_0);
                cg.IlGenerator.Emit(OpCodes.Br, end);
                cg.IlGenerator.MarkLabel(ret1);
                cg.IlGenerator.Emit(OpCodes.Ldc_I4_1);
                cg.IlGenerator.MarkLabel(end);
            }
        }
    }
}
