using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class MinusNode : TigerCompiler.ArithmeticOperationNode
    {
        public MinusNode(IToken payload) : base(payload)
        {
        }
    }
}
