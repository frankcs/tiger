using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Operations.Logical;

namespace TigerCompiler.AST
{
    class EqualNode : EqualityNode
    {
        public EqualNode(IToken payload) : base(payload)
        {
        }
    }
}
