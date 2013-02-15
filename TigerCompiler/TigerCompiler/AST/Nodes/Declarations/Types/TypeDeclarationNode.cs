using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Helpers;
using TigerCompiler.Semantic;

namespace TigerCompiler.AST.Nodes.Declarations.Types
{
    abstract class TypeDeclarationNode : DeclarationNode
    {
        protected TypeDeclarationNode(IToken payload) : base(payload)
        {
        }

        protected TypeIDNode NewTypeNode
        {
            get { return Children[0] as TypeIDNode; }
        }

        public void ResolveReferencedTypes(Scope scope)
        {
            Scope.ResolveType(NewTypeNode.TypeName).ResolveReferencedTypes(scope);
        }
    }
}
