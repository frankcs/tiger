using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class NotEqualNode : TigerCompiler.RelationalOperationNode
    {
        public NotEqualNode(IToken payload) : base(payload)
        {
        }
    }
}
