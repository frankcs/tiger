using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class TypeDeclarationNode : TigerCompiler.DeclarationNode
    {
        public TypeDeclarationNode(IToken payload) : base(payload)
        {
        }
    }
}
