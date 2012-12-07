using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class IfThenNode : TigerCompiler.FlowControlNode
    {
        public IfThenNode(IToken payload) : base(payload)
        {
        }
    }
}
