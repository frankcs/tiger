using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler
{
    public abstract class FlowControlNode : InstructionNode
    {
        public FlowControlNode(IToken payload) : base(payload)
        {
        }

        public Label EndofCicle { get; protected set; }
    }
}
