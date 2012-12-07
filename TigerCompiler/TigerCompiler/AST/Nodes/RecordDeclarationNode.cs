using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class RecordDeclarationNode : TypeDeclarationNode
    {
        public RecordDeclarationNode(IToken payload) : base(payload)
        {
        }
    }
}
