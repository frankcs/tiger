using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Declarations;
using TigerCompiler.Semantic;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Instructions
{
    /// <summary>
    /// LET declaration_list 'in' expr_seq? 'end' -> ^(LET declaration_list expr_seq?)
    /// expr (';' expr)* -> ^(EXPR_SEQ expr+);
    /// declaration_list: (declaration)+ -> ^(DECL_LIST declaration+);
    /// TODO: Finish the let scope thing.
    /// </summary>
    class LetNode : InstructionNode
    {
        private Scope _letScope;

        public LetNode(IToken payload) : base(payload)
        {
        }

        DeclarationNode DeclarationsNode
        {
            get { return (DeclarationNode) Children[0]; }
        }

        ExpressionSequenceNode ExprSeqNode
        {
            get { return (ExpressionSequenceNode) (Children.Count > 1 ? Children[1] : null); }
        }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            _letScope = scope.CreateChildScope();

            base.CheckSemantics(_letScope, report);

            //The return type is that of the last expression, or none if there aren't any.
            ReturnType = Children.Count > 1 ? ((ASTNode)ExprSeqNode.Children[ExprSeqNode.Children.Count - 1]).ReturnType : TypeInfo.Void;
        }
    }
}
