using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Helpers
{
    // TODO: Is this useful?
    /// <summary>
    /// 'type' type_id EQUAL type -> ^(TYPE_DECL type_id type);
    /// </summary>
    public class TypeIDNode : HelperNode
    {
        public TypeIDNode(IToken payload) : base(payload)
        {
        }

        public string TypeName
        {
            get { return Children[0].Text; }
        }
    }
}
