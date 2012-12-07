using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class ForNode : TigerCompiler.FlowControlNode
    {
        public ForNode(IToken payload) : base(payload)
        {
        }
    }
}
