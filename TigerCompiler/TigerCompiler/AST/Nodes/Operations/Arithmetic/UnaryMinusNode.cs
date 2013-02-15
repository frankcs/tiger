using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.Semantic;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Operations.Arithmetic
{
    class UnaryMinusNode : UnaryOperationNode
    {
        public UnaryMinusNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            report.Assert(this,Operand.ReturnType == TypeInfo.Int, "Operand must be an integer.");

            ReturnType = TypeInfo.Int;
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            base.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Neg);
        }
    }
}
