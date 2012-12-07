using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class ExpressionListNode : TigerCompiler.HelperNode
    {
        public ExpressionListNode(IToken payload) : base(payload)
        {
        }
    }
}
