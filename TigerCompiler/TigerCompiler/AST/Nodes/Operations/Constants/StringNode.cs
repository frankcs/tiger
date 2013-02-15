using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.CodeGeneration;

namespace TigerCompiler.AST.Nodes.Operations.Constants
{
    class StringNode : ConstantNode
    {
        public StringNode(IToken payload) : base(payload)
        {
        }

        public override void GenerateCode(CodeGenerator cg)
        {
            base.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Ldstr, Text);
        }
    }
}
