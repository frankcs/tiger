using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class EqualNode : TigerCompiler.RelationalOperationNode
    {
        public EqualNode(IToken payload) : base(payload)
        {
        }
    }
}
