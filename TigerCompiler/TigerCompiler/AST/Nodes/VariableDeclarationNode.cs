using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class VariableDeclarationNode : TigerCompiler.DeclarationNode
    {
        public VariableDeclarationNode(IToken payload) : base(payload)
        {
        }
    }
}
