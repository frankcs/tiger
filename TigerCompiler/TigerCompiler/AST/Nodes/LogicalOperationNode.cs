using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler
{
    public abstract class LogicalOperationNode : BinaryOperationNode
    {
        public LogicalOperationNode(IToken payload) : base(payload)
        {
        }
    }
}
