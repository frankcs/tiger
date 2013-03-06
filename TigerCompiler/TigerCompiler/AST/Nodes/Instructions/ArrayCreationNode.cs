using System.Reflection.Emit;
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
        public ArrayCreationNode(IToken payload)
            : base(payload)
        {
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            report.Assert(this, TypeNode.ReturnType != TypeInfo.Void, "Invalid type for array.");
            report.Assert(this, LengthExpression.ReturnType == TypeInfo.Int, "Array length must be an int.");

            var type = scope.ResolveType(TypeNode.TypeName);
            var arrayType = (type is AliasTypeInfo) ? ((AliasTypeInfo)type).TargetType : type;

            if (TypeInfo.IsNull(arrayType))
                report.AddError(this, "Unknown type: {0}", TypeNode.TypeName);
            else if (!((arrayType is ArrayTypeInfo || (arrayType is AliasTypeInfo && ((AliasTypeInfo)arrayType).TargetType is ArrayTypeInfo))))
                report.AddError(this, "The type {0} is not an array.", TypeNode.TypeName);
            else
                report.Assert(this, ((ArrayTypeInfo)arrayType).TargetType == InitExpression.ReturnType,
                              "Initialization expression and array types do not match ({0},{1})",
                              InitExpression.ReturnType, TypeNode.ReturnType);

            ReturnType = arrayType;
        }

        private TypeIDNode TypeNode
        {
            get { return (TypeIDNode)Children[0]; }
        }

        private ASTNode LengthExpression
        {
            get { return (ASTNode)Children[1]; }
        }

        private ASTNode InitExpression
        {
            get { return (ASTNode)Children[2]; }
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            var arrayinfo = TypeInfo.ArrayFromTypeInfo(Scope.ResolveType(TypeNode.TypeName));
            //to get the length
            LocalBuilder length = cg.IlGenerator.DeclareLocal(typeof(int));
            LocalBuilder count = cg.IlGenerator.DeclareLocal(typeof(int));
            LocalBuilder arr = cg.IlGenerator.DeclareLocal(arrayinfo.GetILType());

            Label initloopbegin = cg.IlGenerator.DefineLabel();
            Label initloopend = cg.IlGenerator.DefineLabel();

            //set the count to zero
            cg.IlGenerator.Emit(OpCodes.Ldc_I4_0);
            cg.IlGenerator.Emit(OpCodes.Stloc, count);
            LengthExpression.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Dup);
            //get the length
            cg.IlGenerator.Emit(OpCodes.Stloc, length);
            cg.IlGenerator.Emit(OpCodes.Newarr, arrayinfo.TargetType.GetILType());
            cg.IlGenerator.Emit(OpCodes.Stloc, arr);

            //make the initialization loop
            cg.IlGenerator.MarkLabel(initloopbegin);

            //make the comparison and jmp to end if does not hold
            cg.IlGenerator.Emit(OpCodes.Ldloc, count);
            cg.IlGenerator.Emit(OpCodes.Ldloc, length);
            cg.IlGenerator.Emit(OpCodes.Bge,initloopend);
            
            //Evaluate the expr and store it
            cg.IlGenerator.Emit(OpCodes.Ldloc, arr);
            cg.IlGenerator.Emit(OpCodes.Ldloc, count);
            InitExpression.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Stelem, arrayinfo.TargetType.GetILType());

            //increase count and jmp to begin
            cg.IlGenerator.Emit(OpCodes.Ldloc, count);
            cg.IlGenerator.Emit(OpCodes.Ldc_I4_1);
            cg.IlGenerator.Emit(OpCodes.Add);
            cg.IlGenerator.Emit(OpCodes.Stloc,count);
            cg.IlGenerator.Emit(OpCodes.Br,initloopbegin);
            cg.IlGenerator.MarkLabel(initloopend);
            
            //leave the arr reference in the top of the stack
            cg.IlGenerator.Emit(OpCodes.Ldloc,arr);
            }
    }
}
