using System;
using System.Reflection;
using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Operations.Comparison
{
    class GreaterThanNode : ComparisonNode
    {
        public GreaterThanNode(IToken payload) : base(payload)
        {
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            base.GenerateCode(cg);
            if(LeftOperand.ReturnType== TypeInfo.Int)
            {
                cg.IlGenerator.Emit(OpCodes.Cgt);
            }
            else
            {
                Label ret1 = cg.IlGenerator.DefineLabel();
                Label end = cg.IlGenerator.DefineLabel();

                MethodInfo strcmp = typeof (string).GetMethod("CompareOrdinal", new[] {typeof (string), typeof (string)});
                //cmp with zero, if greater return 1 
                cg.IlGenerator.Emit(OpCodes.Call, strcmp);
                cg.IlGenerator.Emit(OpCodes.Ldc_I4_0);
                cg.IlGenerator.Emit(OpCodes.Bgt,ret1);
                cg.IlGenerator.Emit(OpCodes.Ldc_I4_0);
                cg.IlGenerator.Emit(OpCodes.Br,end);
                cg.IlGenerator.MarkLabel(ret1);
                cg.IlGenerator.Emit(OpCodes.Ldc_I4_1);
                cg.IlGenerator.MarkLabel(end);
            }
        }
    }
}
