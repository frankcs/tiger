using Antlr.Runtime;
using TigerCompiler.AST.Nodes.LValue;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Instructions
{
    /// <summary>
    /// ^(ASSIGN lvalue expr)
    /// </summary>
    class AssignNode : InstructionNode
    {
        public AssignNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            report.Assert(this, !LValue.ReturnType.IsReadOnly, "Cannot assign a value to {0}. It is read-only.", LValue.Children[0].Text);

            ReturnType = TypeInfo.Void;
        }

        LValueNode LValue
        {
            get{return Children[0] as LValueNode;}
        }

        ASTNode Expression
        {
            get { return Children[1] as ASTNode; }
        }
    }
}
