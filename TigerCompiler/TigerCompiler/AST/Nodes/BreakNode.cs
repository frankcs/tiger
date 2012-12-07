using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class BreakNode : TigerCompiler.FlowControlNode
    {
        public BreakNode(IToken payload) : base(payload)
        {
        }
    }
}
