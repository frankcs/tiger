using System.Reflection.Emit;
using System.Diagnostics;
using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Helpers;
using TigerCompiler.Semantic;
using TigerCompiler.Semantic.Types;

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

            if (!report.Assert(this, func != null, "Undefined function {0}.", FunctionName) ||
                !report.Assert(this, func is FunctionInfo, "Cannot invoke a variable.") ||
                !report.Assert(this, ((FunctionInfo) func).Parameters.Count == ExpressionList.ChildCount,
                               "Calling function with wrong amount of parameters.")) return;

            var functionInfo = (FunctionInfo) func;

            int i = 0;
            foreach (var parameter in functionInfo.Parameters)
            {
                var paramExpression = (ASTNode) ExpressionList.Children[i++];
                report.Assert(paramExpression, paramExpression.ReturnType == parameter.Value,
                              "Function parameter and expression types do not match.");
            }
            ReturnType = functionInfo.ReturnType; // Procedures will hold void as a return type.
        }

        public string FunctionName
        {
            get { return Children[0].Text; }
        }

        public ExpressionListNode ExpressionList
        {
            get { return (ExpressionListNode)Children[1]; }
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            var func = (FunctionInfo)Scope.ResolveVarOrFunction(FunctionName);
            ExpressionList.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Call,func.ILMethod);
        }
    }
}
