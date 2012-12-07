using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class NilNode : TigerCompiler.ConstantNode
    {
        public NilNode(IToken payload) : base(payload)
        {
        }

        public NilNode() : base(Tokens.Skip)
        {
            
        }
    }
}
