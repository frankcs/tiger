using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class AndNode : TigerCompiler.LogicalOperationNode
    {
        public AndNode(IToken payload) : base(payload)
        {
        }
    }
}
