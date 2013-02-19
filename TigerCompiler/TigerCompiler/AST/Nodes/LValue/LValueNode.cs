using System;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Text;
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
            if (report.Assert(this, variableInfo != null, "Unknown variable for the current scope ({0}).", MainIDNode.Text))
            {
                Debug.Assert(variableInfo != null, "variableInfo != null");
                ReturnType = variableInfo.VariableType;

                for (int i = 1; i < Children.Count; i++)
                {
                    var tmp = Children[i];
                    if (tmp is DotNode)
                    {
                        var recordTypeInfo = (ReturnType as RecordTypeInfo);
                        if (recordTypeInfo != null)
                        {
                            TypeInfo value;
                            bool memberExists = recordTypeInfo.Fields.TryGetValue(((DotNode) tmp).MemberName,out value);
                            if (memberExists)
                                ReturnType = value;
                            else
                            {
                                report.AddError(this,"Unknown member.");
                                ReturnType = null;
                                break;
                            }
                        }
                        else
                        {
                            report.AddError(this,"Dot operator applied to an invalid type.");
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
                            report.AddError(this,"Trying to index on a non-array type.");
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

        public IdNode MainIDNode
        {
            get { return Children[0] as IdNode; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Children[0].Text);
            for (int i = 1; i < Children.Count; i++)
            {
                var current = Children[i];
                if (current is DotNode)
                {
                    sb.Append("." + (current as DotNode).MemberName);
                } else if(current is IndexingNode)
                {
                    sb.Append("[" + (current as IndexingNode) + "]");
                }
            }
            return sb.ToString();
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            var varinfo = (VariableInfo)(MainIDNode.ReferencedThing);
            //si es una var sencilla genero código para Expression y asigno al ILLocal

            if (Children.Count == 1){
                cg.IlGenerator.Emit(OpCodes.Ldloc, varinfo.ILLocalVariable);
            }
            else
            {
                //se carga el obj o arr
                cg.IlGenerator.Emit(OpCodes.Ldloc, varinfo.ILLocalVariable);
                TypeInfo resolutedtype = varinfo.VariableType;
                for (int i = 1; i < Children.Count; i++)
                {
                    if (Children[i] is IndexingNode)
                    {
                        var node = (IndexingNode)Children[i];
                        Type targettype = ((ArrayTypeInfo)resolutedtype).TargetType.GetILType();
                        node.IndexNode.GenerateCode(cg);
                        //si es arr se accede
                        cg.IlGenerator.Emit(OpCodes.Ldelem, targettype);
                        resolutedtype = ((ArrayTypeInfo) resolutedtype).TargetType;
                    }
                    else
                    {
                        var membername = ((DotNode)Children[i]).MemberName;
                        //resolver el field builder asoc al member
                        var fieldbuilder = ((RecordTypeInfo)resolutedtype).FieldBuilders[membername];
                        //actualizar el resoluted type info
                        resolutedtype = ((RecordTypeInfo)resolutedtype).Fields[membername];
                        cg.IlGenerator.Emit(OpCodes.Ldfld, fieldbuilder);
                    }
                }
            }
        }
    }
}
