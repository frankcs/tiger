using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler
{
    public abstract class BinaryOperationNode : ASTNode
    {
        public BinaryOperationNode(IToken payload) : base(payload)
        {
        }
    }
}
