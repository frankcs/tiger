using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Helpers;

namespace TigerCompiler.AST.Nodes.Declarations.Types
{
    /// <summary>
    /// ^(RECORD_DECL type_id type_fields)
    /// type_fields:	type_field (','! type_field)*;
    /// type_field:	ID ':' type_id -> ^(ID type_id);
    /// </summary>
    class RecordDeclarationNode : TypeDeclarationNode
    {
        public RecordDeclarationNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            var record = scope.DefineRecord(NewTypeNode.TypeName);

            for (int i = 1; i < Children.Count; i++)
            {
                var currentMember = Children[i];
                var currentMemberTypeName = ((currentMember as ASTNode).Children[0] as TypeIDNode).TypeName;
                record.AddMember(currentMember.Text, currentMemberTypeName);
            }
        }
    }
}
