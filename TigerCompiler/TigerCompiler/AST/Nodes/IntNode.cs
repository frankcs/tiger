using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class IntNode : TigerCompiler.ConstantNode
    {
        public IntNode(IToken payload) : base(payload)
        {
        }
    }
}
