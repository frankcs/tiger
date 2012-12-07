using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class MultiplyNode : TigerCompiler.ArithmeticOperationNode
    {
        public MultiplyNode(IToken payload) : base(payload)
        {
        }
    }
}
