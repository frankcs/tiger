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
            base.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.And);
        }
    }
}
