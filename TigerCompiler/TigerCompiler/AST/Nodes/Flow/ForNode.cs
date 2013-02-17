using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Helpers;
using TigerCompiler.Semantic;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Flow
{
    /// <summary>
    ///  FOR var=ID ASSIGN init=expr 'to' limit=expr 'do' something=expr -> ^(FOR $var $init $limit $something)
    /// </summary>
    class ForNode : FlowControlNode
    {
        private Scope forScope;
        private VariableInfo loopVariable;

        public ForNode(IToken payload) : base(payload){}

        IdNode LoopVariableIDNode
        {
            get { return (IdNode) Children[0]; }
        }
        ASTNode LoopVariableInitExpression
        {
            get { return (ASTNode)Children[1]; }
        }
        ASTNode LoopVariableUpperLimitExpression
        {
            get { return (ASTNode)Children[2]; }
        }
        ASTNode DoExpression
        {
            get { return (ASTNode)Children[3]; }
        }

        public bool BodyReturnsValue
        {
            get { return DoExpression.ReturnType != TypeInfo.Void && DoExpression.ReturnType != null; }
        }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            forScope =scope.CreateChildScope();
            loopVariable = forScope.DefineVariable(LoopVariableIDNode.Text, TypeInfo.Int, true);
            base.CheckSemantics(forScope, report);

            report.Assert(LoopVariableInitExpression, LoopVariableInitExpression.ReturnType == TypeInfo.Int,"The initialization expression of a for loop must return an integer value.");
            report.Assert(this, LoopVariableUpperLimitExpression.ReturnType == TypeInfo.Int, "The upper bound expression of a for loop must return an integer value.");
            //report.Assert(DoExpression, DoExpression.ReturnType == TypeInfo.Void, "A for expression cannot return a value.");

			ReturnType = TypeInfo.Void;
		}

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            Label beginoffor = cg.IlGenerator.DefineLabel();
            EndofCicle = cg.IlGenerator.DefineLabel();
            //define the iteration variable and hook it with it's varinfo
            loopVariable.ILLocalVariable = cg.IlGenerator.DeclareLocal(typeof(int));
            LocalBuilder upperbound = cg.IlGenerator.DeclareLocal(typeof(int));

            //starting th generation
            LoopVariableInitExpression.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Stloc, loopVariable.ILLocalVariable);
            LoopVariableUpperLimitExpression.GenerateCode(cg);
            cg.IlGenerator.Emit(OpCodes.Stloc, upperbound);
            //mark the begin of the cicle
            cg.IlGenerator.MarkLabel(beginoffor);
            //do the comparison
            cg.IlGenerator.Emit(OpCodes.Ldloc, loopVariable.ILLocalVariable);
            cg.IlGenerator.Emit(OpCodes.Ldloc, upperbound);
            cg.IlGenerator.Emit(OpCodes.Bgt,EndofCicle);
            //Generate the body
            DoExpression.GenerateCode(cg);
            if(BodyReturnsValue)
                cg.IlGenerator.Emit(OpCodes.Pop);
            //Incresing the counter
            cg.IlGenerator.Emit(OpCodes.Ldloc, loopVariable.ILLocalVariable);
            cg.IlGenerator.Emit(OpCodes.Ldc_I4_1);
            cg.IlGenerator.Emit(OpCodes.Add);
            cg.IlGenerator.Emit(OpCodes.Stloc, loopVariable.ILLocalVariable);
            cg.IlGenerator.Emit(OpCodes.Br, beginoffor);
            cg.IlGenerator.MarkLabel(EndofCicle);
        }
    }
}
