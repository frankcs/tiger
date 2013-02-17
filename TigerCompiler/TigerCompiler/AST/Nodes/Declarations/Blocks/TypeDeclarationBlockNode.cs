using System.Linq;
using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Declarations.Types;

namespace TigerCompiler.AST.Nodes.Declarations.Blocks
{
    internal class TypeDeclarationBlockNode : ASTNode
    {
        public TypeDeclarationBlockNode(IToken payload): base(payload) {}

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            foreach (TypeDeclarationNode node in Children)
                node.ResolveReferencedTypes(scope,report);
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            //definir los tipos y enlazarlos con su builder menos los alias, a esos le daremos un trato especial
            var typenames = from typedec in Children where typedec is RecordDeclarationNode select ((TypeDeclarationNode) typedec).NewTypeNode.TypeName;
            var typesinfo = from name in typenames select Scope.ResolveType(name);
            
            foreach (var typeInfo in typesinfo)
            {
                typeInfo.ILTypeBuilder = cg.CreateType();
            }
            base.GenerateCode(cg);
        }
    }
}