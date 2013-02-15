using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Helpers;
using TigerCompiler.Semantic;
using TigerCompiler.Semantic.Types;

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

        //protected new TypeInfo Type
        //{
        //    get
        //    {
        //        var type = Scope.ResolveType(NewTypeNode.TypeName);
        //        if (type is AliasTypeInfo)
        //            return (type as AliasTypeInfo).TargetType;
        //        return type;
        //    }
        //}

        public void ResolveReferencedTypes(Scope scope,ErrorReporter reporter)
        {
            scope.ResolveType(NewTypeNode.TypeName).ResolveReferencedTypes(this,scope,reporter);
        }
    }
}
