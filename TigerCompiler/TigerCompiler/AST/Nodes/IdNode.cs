using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class IdNode : TigerCompiler.HelperNode
    {
        public IdNode(IToken payload) : base(payload)
        {
        }
    }
}
