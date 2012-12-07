using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class WhileNode : TigerCompiler.FlowControlNode
    {
        public WhileNode(IToken payload) : base(payload)
        {
        }
    }
}
