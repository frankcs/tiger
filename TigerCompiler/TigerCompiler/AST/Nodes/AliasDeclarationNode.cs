using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class AliasDeclarationNode : TypeDeclarationNode
    {
        public AliasDeclarationNode(IToken payload) : base(payload)
        {
        }
    }
}
