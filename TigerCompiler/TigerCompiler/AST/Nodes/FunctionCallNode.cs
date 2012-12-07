using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class FunctionCallNode : TigerCompiler.InstructionNode
    {
        public FunctionCallNode(IToken payload) : base(payload)
        {
        }
    }
}
