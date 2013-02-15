using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Helpers;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Instructions
{
    /// <summary>
    /// (ID '[' expr ']' 'of') => type_id '[' e1=expr ']' 'of' e2=expr -> ^(ARRAY_CREATION type_id $e1 $e2) 
    /// </summary>
    class ArrayCreationNode : InstructionNode
    {
        public ArrayCreationNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            report.Assert(this, TypeNode.ReturnType != TypeInfo.Void, "Invalid type for array.");
            report.Assert(this, LengthExpression.ReturnType == TypeInfo.Int, "Array length must be an int.");

            var arrayType = scope.ResolveType(TypeNode.TypeName);
            if (arrayType == null)
                report.AddError("Unknown type: {0}", TypeNode.TypeName);
            else if (!(arrayType is ArrayTypeInfo))
                report.AddError("The type {0} is not an array.", TypeNode.TypeName);
            else
                report.Assert(this, ((ArrayTypeInfo) arrayType).TargetType == InitExpression.ReturnType,
                              "Initialization expression and array types do not match ({0},{1})",
                              InitExpression.ReturnType, TypeNode.ReturnType);

            ReturnType = arrayType;
        }

        private TypeIDNode TypeNode
        {
            get { return (TypeIDNode) Children[0]; }
        }

        private ASTNode LengthExpression
        {
            get { return (ASTNode)Children[1]; }
        }

        private ASTNode InitExpression
        {
            get { return (ASTNode)Children[2]; }
        }
    }
}
