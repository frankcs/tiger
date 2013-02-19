using Antlr.Runtime;
using TigerCompiler.AST.Nodes.LValue;
using TigerCompiler.Semantic.Symbols;

namespace TigerCompiler.AST.Nodes.Helpers
{
    class IdNode : HelperNode
    {
        public IdNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            if (!(Parent is DotNode))
                ReferencedThing = scope.ResolveVarOrFunction(Text);
        }

        public Symbol ReferencedThing { get; set; }
    }
}
