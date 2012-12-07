using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler
{
    public class HelperNode : ASTNode
    {
        public HelperNode(IToken payload) : base(payload)
        {
        }
    }
}
