using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Helpers;
using TigerCompiler.Semantic;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.LValue
{
    class IndexingNode : HelperNode
    {
        public IndexingNode(IToken payload) : base(payload)
        {
        }

        public ASTNode IndexNode
        {
            get { return (ASTNode) Children[0]; }
        }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            report.Assert(this, IndexNode.ReturnType == TypeInfo.Int, "Index is not an integer.");
        }
    }
}
