using System;
using System.Reflection;
using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Operations.Equality
{
    public abstract class EqualityNode : BinaryOperationNode
    {
        protected EqualityNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            report.Assert(this,LeftOperand.ReturnType == RightOperand.ReturnType, "Equality comparisons are only allowed between elements of the same type.");

            ReturnType = TypeInfo.Int;
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            base.GenerateCode(cg);
            Label return0 = cg.IlGenerator.DefineLabel();
            Label end = cg.IlGenerator.DefineLabel();

            if (LeftOperand.ReturnType == TypeInfo.Int)
            {
                //code gen for integer eq
                cg.IlGenerator.Emit(OpCodes.Ceq);
                return;
            }
            if (LeftOperand.ReturnType == TypeInfo.String)
            {
                //code gen for string eq
                MethodInfo stringeq = typeof(String).GetMethod("Equals", new[] { typeof(string), typeof(string) });
                cg.IlGenerator.Emit(OpCodes.Call, stringeq);
            }
            else
            {
                //code gen for ref eq
                MethodInfo refeq = typeof(object).GetMethod("ReferenceEquals", new[] { typeof(object), typeof(object) });
                cg.IlGenerator.Emit(OpCodes.Call, refeq);
            }
            cg.IlGenerator.Emit(OpCodes.Brfalse,return0);
            cg.IlGenerator.Emit(OpCodes.Ldc_I4_1);
            cg.IlGenerator.Emit(OpCodes.Br,end);
            cg.IlGenerator.MarkLabel(return0);
            cg.IlGenerator.Emit(OpCodes.Ldc_I4_0);
            cg.IlGenerator.MarkLabel(end);
        }
    }
}
