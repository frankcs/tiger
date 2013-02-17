using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Helpers;

namespace TigerCompiler.AST.Nodes.Declarations.Types
{
    /// <summary>
    /// ^(ARRAY_TYPE_DECL type_id type_id)
    /// </summary>
    class ArrayTypeDeclarationNode : TypeDeclarationNode
    {
        public ArrayTypeDeclarationNode(IToken payload) : base(payload){}

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);
            if (report.Assert(this, !scope.IsDefinedInCurrentScopeAsType(NewTypeNode.TypeName), "Type {0} is already defined in the current scope.", NewTypeNode.TypeName))

            scope.DefineArray(NewTypeNode.TypeName, TargetTypeNode.TypeName);
        }

        TypeIDNode TargetTypeNode
        {
            get { return Children[1] as TypeIDNode; }
        }
    }
}
