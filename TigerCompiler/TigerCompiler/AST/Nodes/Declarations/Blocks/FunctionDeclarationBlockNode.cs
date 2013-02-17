using System.Collections.Generic;
using Antlr.Runtime;
using TigerCompiler.Semantic;

namespace TigerCompiler.AST.Nodes.Declarations.Blocks
{
    internal class FunctionDeclarationBlockNode : ASTNode
    {
        public FunctionDeclarationBlockNode(IToken payload)
            : base(payload)
        {

        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            // Delegated to the FunctionDeclarationNodes:
                // - Add every function declaration inside this block to the current scope.
                // - Create a child scope for every function.
            base.CheckSemantics(scope, report);

            var names = new HashSet<string>();
            foreach (FunctionDeclarationNode declarationNode in Children)
            {
                if (!report.Assert(declarationNode,!names.Contains(declarationNode.FunctionName),"Duplicate function names in the same function block."))
                  return; // TODO: return?
                names.Add(declarationNode.FunctionName);
            }

            // - Check the semantics of the bodies of the functions.
            // - Verify that the function bodies return the right type.
            foreach (FunctionDeclarationNode declarationNode in Children)
            {
                var varOrFunction = scope.ResolveVarOrFunction(declarationNode.FunctionName);
                if (!(varOrFunction is FunctionInfo)) continue;
                declarationNode.FunctionBody.CheckSemantics(declarationNode.FunctionScope, report);
                report.Assert(declarationNode, declarationNode.FunctionBody.ReturnType == declarationNode.FunctionReturnType,
                              "Function return type does not match declared type.");
            }
        }
    }
}