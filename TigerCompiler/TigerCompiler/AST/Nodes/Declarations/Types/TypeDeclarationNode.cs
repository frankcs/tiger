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

        public TypeIDNode NewTypeNode
        {
            get { return Children[0] as TypeIDNode; }
        }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            var type = scope.ResolveType(NewTypeNode.TypeName);
            report.Assert(this, TypeInfo.IsNull(type) || !type.IsReadOnly, "Cannot overwrite type {0}. It is read-only.", NewTypeNode.TypeName);
            
            NewTypeNode.CheckSemantics(scope,report);
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
            var type = scope.ResolveType(NewTypeNode.TypeName);
            var noCycles = type.ResolveReferencedTypes(this, scope, reporter);
            reporter.Assert(this, noCycles, "Invalid type detected. Check for invalid type references, or cyclic type definitions.");
        }
    }
}
