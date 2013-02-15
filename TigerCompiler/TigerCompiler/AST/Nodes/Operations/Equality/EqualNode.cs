using System;
using System.Reflection;
using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Operations.Equality
{
    class EqualNode : EqualityNode
    {
        public EqualNode(IToken payload) : base(payload)
        {
            
        }
    }
}
