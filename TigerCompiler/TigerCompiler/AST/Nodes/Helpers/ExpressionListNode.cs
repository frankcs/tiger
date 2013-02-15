using Antlr.Runtime;

namespace TigerCompiler.AST.Nodes.Helpers
{
    /// <summary>
    /// (ID '(')=>(ID '(' expr_list? ')') -> ^(FUNCTION_CALL ID ^(EXPRESSION_LIST expr_list?))
    /// expr_list: expr (','! expr)*;
    /// </summary>
    class ExpressionListNode : HelperNode
    {
        public ExpressionListNode(IToken payload) : base(payload)
        {
        }
    }
}
