using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class LetNode : TigerCompiler.InstructionNode
    {
        public LetNode(IToken payload) : base(payload)
        {
        }
    }
}
