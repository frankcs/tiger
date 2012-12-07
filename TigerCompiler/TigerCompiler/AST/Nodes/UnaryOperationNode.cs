using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler
{
    public abstract class UnaryOperationNode : ASTNode
    {
        public UnaryOperationNode(IToken payload) : base(payload)
        {
        }
    }
}
