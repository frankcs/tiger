using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler
{
    public abstract class DeclarationNode : ASTNode
    {
        public DeclarationNode(IToken payload) : base(payload)
        {
        }
    }
}
