using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.LValue
{
    internal class DotNode : Helpers.HelperNode
    {
        public DotNode(IToken payload):base(payload)
        {
        }

        public string MemberName
        {
            get { return Children[0].Text; }
        }
    }
}