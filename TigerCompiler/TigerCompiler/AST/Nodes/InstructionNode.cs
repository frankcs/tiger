using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler
{
    public abstract class InstructionNode : ASTNode
    {
        public InstructionNode(IToken payload) : base(payload)
        {
        }
    }
}
