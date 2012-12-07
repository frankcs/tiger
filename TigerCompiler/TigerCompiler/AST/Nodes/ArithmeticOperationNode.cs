using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler
{
    public abstract class ArithmeticOperationNode : BinaryOperationNode
    {
        public ArithmeticOperationNode(IToken payload) : base(payload)
        {
        }
    }
}
