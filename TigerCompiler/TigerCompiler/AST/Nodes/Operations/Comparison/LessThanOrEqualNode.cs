using System.Reflection;
using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Operations.Comparison
{
    class LessThanOrEqualNode : ComparisonNode
    {
        public LessThanOrEqualNode(IToken payload) : base(payload)
        {
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            base.GenerateCode(cg);
            if (LeftOperand.ReturnType == TypeInfo.Int)
            {
                Label equal = cg.IlGenerator.DefineLabel();
                Label end = cg.IlGenerator.DefineLabel();

                LocalBuilder local0 = cg.IlGenerator.DeclareLocal(typeof(int));
                LocalBuilder local1 = cg.IlGenerator.DeclareLocal(typeof(int));

                //store the values
                cg.IlGenerator.Emit(OpCodes.Stloc, local0);
                cg.IlGenerator.Emit(OpCodes.Stloc, local1);

                //reload
                cg.IlGenerator.Emit(OpCodes.Ldloc, local0);
                cg.IlGenerator.Emit(OpCodes.Ldloc, local1);

                //if equal return 1
                cg.IlGenerator.Emit(OpCodes.Beq, equal);

                //reload
                cg.IlGenerator.Emit(OpCodes.Ldloc, local0);
                cg.IlGenerator.Emit(OpCodes.Ldloc, local1);
                //compare and end
                cg.IlGenerator.Emit(OpCodes.Clt);
                cg.IlGenerator.Emit(OpCodes.Br, end);

                cg.IlGenerator.MarkLabel(equal);
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
                cg.IlGenerator.Emit(OpCodes.Ble, ret1);
                cg.IlGenerator.Emit(OpCodes.Ldc_I4_0);
                cg.IlGenerator.Emit(OpCodes.Br, end);
                cg.IlGenerator.MarkLabel(ret1);
                cg.IlGenerator.Emit(OpCodes.Ldc_I4_1);
                cg.IlGenerator.MarkLabel(end);
            }
        }
    }
}
