using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Helpers;
using TigerCompiler.Semantic;

namespace TigerCompiler.AST.Nodes.Instructions
{
    /// <summary>
    /// (ID '(')=>(ID '(' expr_list? ')') -> ^(FUNCTION_CALL ID ^(EXPRESSION_LIST expr_list?))
    /// </summary>
    internal class FunctionCallNode : InstructionNode
    {
        public FunctionCallNode(IToken payload)
            : base(payload)
        {
        }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            var func = scope.ResolveVarOrFunction(FunctionName);

            if (func == null)
                report.AddError("Undefined function {0}.", FunctionName);
            else if (func is VariableInfo)
                report.AddError("Cannot invoke a variable.");
            else ReturnType = ((FunctionInfo) func).ReturnType; // Procedures will hold void as a return type.
        }

        public string FunctionName
        {
            get { return Children[0].Text; }
        }

        public ExpressionListNode ExpressionList
        {
            get { return (ExpressionListNode)Children[1]; }
        }
    }
}
