using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class AssignNode : TigerCompiler.InstructionNode
    {
        public AssignNode(IToken payload) : base(payload)
        {
        }
    }
}
