using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.Semantic;

namespace TigerCompiler.AST.Nodes.Operations.Equality
{
    class NotEqualNode : EqualityNode
    {
        public NotEqualNode(IToken payload) : base(payload)
        {
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            base.GenerateCode(cg);
            //not whatever the equals gives
            MethodBuilder not = ((FunctionInfo) Semantic.Scope.DefaultGlobalScope.ResolveVarOrFunctionOnCodeGen("not")).ILMethod;
            cg.IlGenerator.Emit(OpCodes.Call, not);

            #region A mano
            //Label return0 = cg.IlGenerator.DefineLabel();
            //Label end = cg.IlGenerator.DefineLabel();
            ////if there was 1 ret 0 else ret 1
            //cg.IlGenerator.Emit(OpCodes.Brtrue,return0);
            //cg.IlGenerator.Emit(OpCodes.Ldc_I4_1);
            //cg.IlGenerator.Emit(OpCodes.Br, end);
            //cg.IlGenerator.MarkLabel(return0);
            //cg.IlGenerator.Emit(OpCodes.Ldc_I4_0);
            //cg.IlGenerator.MarkLabel(end);
            #endregion
        }
    }
}
