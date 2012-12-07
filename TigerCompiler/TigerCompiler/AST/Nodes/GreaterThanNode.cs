using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class GreaterThanNode : TigerCompiler.RelationalOperationNode
    {
        public GreaterThanNode(IToken payload) : base(payload)
        {
        }
    }
}
