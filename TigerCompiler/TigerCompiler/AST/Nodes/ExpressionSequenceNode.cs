using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class ExpressionSequenceNode : TigerCompiler.InstructionNode
    {
        public ExpressionSequenceNode(IToken payload) : base(payload)
        {
        }
    }
}
