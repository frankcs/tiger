using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class IndexingNode : TigerCompiler.HelperNode
    {
        public IndexingNode(IToken payload) : base(payload)
        {
        }
    }
}
