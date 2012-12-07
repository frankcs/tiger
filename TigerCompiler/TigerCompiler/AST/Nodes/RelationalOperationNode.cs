using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler
{
    public abstract class RelationalOperationNode : BinaryOperationNode
    {
        public RelationalOperationNode(IToken payload) : base(payload)
        {
        }
    }
}
