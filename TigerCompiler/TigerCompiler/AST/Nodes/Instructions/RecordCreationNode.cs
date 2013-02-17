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
        public RecordCreationNode(IToken payload) : base(payload)
        {

        }

        public string TypeId {
            get { return ((IdNode)Children[0]).Text; }
        }

        public IEnumerable<ASTNode> InitializationExpressions
        {
            get
            {
                for (int i = 2; i < Children.Count; i=i+2)
                {
                    if(i<Children.Count)
                        yield return (ASTNode)Children[i];
                }
            }
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            var recordinfo = (RecordTypeInfo)Scope.ResolveVarOrFunction(TypeId);
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
