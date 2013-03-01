using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.Semantic;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Operations.Constants
{
    class IntNode : ConstantNode
    {
        public IntNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            ReturnType = TypeInfo.Int;
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            base.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Ldc_I4, int.Parse(Text));
        }
    }
}
