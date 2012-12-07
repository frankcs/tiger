using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class LessThanOrEqualNode : TigerCompiler.RelationalOperationNode
    {
        public LessThanOrEqualNode(IToken payload) : base(payload)
        {
        }
    }
}
