using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class TypeIDNode : TigerCompiler.HelperNode
    {
        public TypeIDNode(IToken payload) : base(payload)
        {
        }
    }
}
