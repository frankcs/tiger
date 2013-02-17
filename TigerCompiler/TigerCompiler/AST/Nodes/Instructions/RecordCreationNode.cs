using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Helpers;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Instructions
{
    /// <summary>
    /// ^(RECORD_CREATION ID field_list?)
    /// (ID EQUAL! expr) (','! ID EQUAL! expr)*;
    /// </summary>
    class RecordCreationNode : InstructionNode
    {
        public RecordCreationNode(IToken payload) : base(payload) {}

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            var type = scope.ResolveType(TypeNode.Text);

            if (report.Assert(TypeNode, !TypeInfo.IsNull(type), "Type {0} does not exist in the current scope.",TypeNode.Text) &&
                report.Assert(TypeNode, type is RecordTypeInfo, "Type {0} is not a record.", TypeNode.Text) &&
                report.Assert(this,(Children.Count - 1)/2 == ((RecordTypeInfo)type).Fields.Count, "Invalid amount of fields."))
            {

                var rtype = (RecordTypeInfo) type;
                for (int i = 1; i < Children.Count; i+=2)
                {
                    string fieldName = Children[i].Text;
                    var initExpr = (ASTNode)Children[i + 1];

                    report.Assert((ASTNode)Children[i], rtype.Fields[fieldName] == initExpr.ReturnType, "Field init expression type and field type do not match.");
                }
            }

            ReturnType = type;
        }

        IdNode TypeNode
        {
            get { return (IdNode) Children[0];}
        }

        public IEnumerable<ASTNode> InitializationExpressions
        {
            get
            {
                for (int i = 2; i < Children.Count; i = i + 2)
                {
                    if (i < Children.Count)
                        yield return (ASTNode)Children[i];
                }
            }
        }
		
        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            var recordinfo = (RecordTypeInfo)Scope.ResolveType(TypeNode.Text);
            //Generate code for the initialization expr
            foreach (ASTNode initializationExpression in InitializationExpressions)
            {
                initializationExpression.GenerateCode(cg);
            }
            //Creates the new obj
            cg.IlGenerator.Emit(OpCodes.Newobj,recordinfo.Constructor);
        }
    }
}
