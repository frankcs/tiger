using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler
{
    public abstract class ConstantNode : InstructionNode
    {
        public ConstantNode(IToken payload) : base(payload)
        {
        }
    }
}
