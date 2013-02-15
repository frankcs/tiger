using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;
using TigerCompiler.AST.Nodes;
using TigerCompiler.AST.Nodes.Operations;

namespace TigerCompiler
{
    public abstract class UnaryOperationNode : OperationNode
    {
        public UnaryOperationNode(IToken payload) : base(payload)
        {
        }

        public ASTNode Operand
        {
            get { return (ASTNode)Children[0]; }
        }
    }
}
