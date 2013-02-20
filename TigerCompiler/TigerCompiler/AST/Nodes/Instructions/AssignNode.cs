using System;
using System.Reflection;
using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.AST.Nodes.LValue;
using TigerCompiler.Semantic;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Instructions
{
    /// <summary>
    /// ^(ASSIGN lvalue expr)
    /// </summary>
    class AssignNode : InstructionNode
    {
        public AssignNode(IToken payload) : base(payload) { }

        public override void CheckSemantics(Scope scope, ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            if (report.Assert(this, CanBeAssignedTo, "Cannot assign a value to {0}. It is a read-only variable or a function.", LValue.Children[0].Text))
            {
                report.Assert(Expression, TypeInfo.IsNull(LValue.ReturnType) || Expression.ReturnType == LValue.ReturnType,
                              "Expression and variable types do not match.");
            }

            ReturnType = TypeInfo.Void;
        }

        LValueNode LValue
        {
            get { return Children[0] as LValueNode; }
        }

        ASTNode Expression
        {
            get { return Children[1] as ASTNode; }
        }
		
		bool CanBeAssignedTo
        {
            get
            {
                var variable = Scope.ResolveVarOrFunction(LValue.Children[0].Text) as VariableInfo;

                return variable != null && !variable.IsReadOnly;
            }
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            var varinfo = (VariableInfo) (LValue.MainIDNode.ReferencedThing);
            //si es una var sencilla genero código para Expression y asigno al ILLocal
            
            if(LValue.Children.Count==1){
                Expression.GenerateCode(cg);
                cg.IlGenerator.Emit(OpCodes.Stsfld, varinfo.ILLocalVariable);
            }
            else
            {
                //se carga el obj o arr
                cg.IlGenerator.Emit(OpCodes.Ldsfld, varinfo.ILLocalVariable);
                TypeInfo resolutedtype = varinfo.VariableType;
                for (int i = 1; i < LValue.Children.Count; i++)
                {
                    if(LValue.Children[i] is IndexingNode)
                    {
                        var node = (IndexingNode)LValue.Children[i];
                        Type targettype = ((ArrayTypeInfo)resolutedtype).TargetType.GetILType();
                        node.IndexNode.GenerateCode(cg);

                        if (i == LValue.Children.Count - 1)
                        {
                            //almacenar
                            Expression.GenerateCode(cg);
                            cg.IlGenerator.Emit(OpCodes.Stelem, targettype);
                        }
                        else //si es arr se accede
                        {
                            cg.IlGenerator.Emit(OpCodes.Ldelem, targettype);
                            resolutedtype = ((ArrayTypeInfo)resolutedtype).TargetType;
                        }


                    }
                    else
                    {
                        var membername = ((DotNode)LValue.Children[i]).MemberName;
                        //resolver el field builder asoc al member
                        var fieldbuilder = ((RecordTypeInfo)resolutedtype).FieldBuilders[membername];
                        if (i == LValue.Children.Count - 1)
                        {
                            //almacenar
                            Expression.GenerateCode(cg);
                            cg.IlGenerator.Emit(OpCodes.Stfld, fieldbuilder);

                        }
                        else
                        {
                            //actualizar el resoluted type info
                            resolutedtype = ((RecordTypeInfo) resolutedtype).Fields[membername];
                            cg.IlGenerator.Emit(OpCodes.Ldfld, fieldbuilder);
                        }
                    }
                }
            }
        }
    }
}
