using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class LValueNode : TigerCompiler.InstructionNode
    {
        public LValueNode(IToken payload) : base(payload)
        {
        }
    }
}
