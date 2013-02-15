using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.CodeGeneration;
using TigerCompiler.Semantic;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Operations.Constants
{
    class NilNode : ConstantNode
    {
        public NilNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            ReturnType = TypeInfo.Unknown;
        }

        public override void GenerateCode(CodeGenerator cg)
        {
            base.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Ldnull);
        }
    }
}
