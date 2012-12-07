using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler
{
    public abstract class ASTNode : Antlr.Runtime.Tree.CommonTree
    {
        protected ASTNode(IToken payload):base(payload)
        {
            
        }
    }
}
