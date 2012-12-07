using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class OrNode : TigerCompiler.LogicalOperationNode
    {
        public OrNode(IToken payload) : base(payload)
        {
        }
    }
}
