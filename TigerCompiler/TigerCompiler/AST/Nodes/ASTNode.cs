using Antlr.Runtime;
using TigerCompiler.CodeGeneration;
using TigerCompiler.Semantic;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes
{
    public abstract class ASTNode : Antlr.Runtime.Tree.CommonTree
    {
        public TypeInfo ReturnType;

        protected ASTNode(IToken payload):base(payload)
        {
        }

        /// <summary>
        /// Semantic check. Not much more to say.
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="report"> </param>
        public virtual void CheckSemantics(Scope scope, ErrorReporter report)
        {
            Scope = scope;

            if (Children != null)
                foreach (ASTNode child in Children)
                    child.CheckSemantics(scope, report);
        }

        protected Scope Scope { get; set; }

        public virtual void GenerateCode(CodeGenerator cg)
        {
            if (Children != null)
                foreach (ASTNode child in Children)
                    child.GenerateCode(cg);
        }
    }

}
