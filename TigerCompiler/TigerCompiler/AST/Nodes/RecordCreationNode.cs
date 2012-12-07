using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;

namespace TigerCompiler.AST
{
    class RecordCreationNode : TigerCompiler.InstructionNode
    {
        public RecordCreationNode(IToken payload) : base(payload)
        {
        }
    }
}
