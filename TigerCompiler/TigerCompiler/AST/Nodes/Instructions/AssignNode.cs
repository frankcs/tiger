﻿using System;
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
            var varinfo = (VariableInfo) Scope.ResolveVarOrFunction(LValue.MainIDNode.Text);
            //si es una var sencilla genero código para Expression y asigno al ILLocal
           
            if(LValue.Children.Count==1){
                Expression.GenerateCode(cg);
                cg.IlGenerator.Emit(OpCodes.Stloc, varinfo.ILLocalVariable);
            }
            else
            {
                //se carga el obj o arr
                cg.IlGenerator.Emit(OpCodes.Ldloc, varinfo.ILLocalVariable);
                Type itertype;
                if (varinfo.VariableType is ArrayTypeInfo)
                    itertype = ((ArrayTypeInfo)varinfo.VariableType).TargetType.GetILType();
                else
                    itertype = varinfo.VariableType.GetILType();
                // Se va accediendo a los miembros
                for (int i = 1; i < LValue.Children.Count-1; i++)
                {
                    if(LValue.Children[1] is IndexingNode)
                    {
                        //si es arr se accede
                        var node = (IndexingNode) LValue.Children[i];
                        cg.IlGenerator.Emit(OpCodes.Ldc_I4, int.Parse(node.IndexNode.Text));
                        cg.IlGenerator.Emit(OpCodes.Ldelem,itertype);
                    }
                    if(LValue.Children[1] is DotNode)
                    {
                        var node = (DotNode)LValue.Children[i];
                        FieldInfo member = itertype.GetField(node.MemberName);
                        cg.IlGenerator.Emit(OpCodes.Ldfld);
                    }
                }
            }
        }
    }
}
