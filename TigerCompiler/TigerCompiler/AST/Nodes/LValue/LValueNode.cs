using System;
using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Helpers;
using TigerCompiler.Semantic;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.LValue
{
    class LValueNode : HelperNode
    {
        public LValueNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            base.CheckSemantics(scope,report);

            var variableInfo = scope.ResolveVarOrFunction(MainIDNode.Text) as VariableInfo;
            if (variableInfo == null)
                report.AddError("Unknown variable for the current scope ({0}).", MainIDNode.Text);
            else
            {
                ReturnType = variableInfo.VariableType;

                for (int i = 1; i < Children.Count; i++)
                {
                    var tmp = Children[i];
                    if (tmp is DotNode)
                    {
                        var recordTypeInfo = (ReturnType as RecordTypeInfo);
                        if (recordTypeInfo != null)
                        {
                            var memberInfo = recordTypeInfo.Fields[((DotNode) tmp).MemberName];
                            if (memberInfo != null)
                                ReturnType = memberInfo;
                            else
                            {
                                report.AddError("Unknown member.");
                                break;
                            }
                        }
                        else
                        {
                            report.AddError("Dot operator applied to an invalid type.");
                            break;
                        }
                    }
                    else if (tmp is IndexingNode)
                    {
                        if (ReturnType is ArrayTypeInfo)
                        {
                            ReturnType = ((ArrayTypeInfo) ReturnType).TargetType;
                        }
                        else
                        {
                            report.AddError("Trying to index on a non-array type.");
                            break;
                        }
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("Invalid child node of LValue detected.");
                    }
                }
            }
        }

        private IdNode MainIDNode
        {
            get { return Children[0] as IdNode; }
        }
    }
}
