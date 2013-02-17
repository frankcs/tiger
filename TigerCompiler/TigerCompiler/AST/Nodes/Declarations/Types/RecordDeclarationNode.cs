using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Antlr.Runtime;
using TigerCompiler.AST.Nodes.Helpers;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Declarations.Types
{
    /// <summary>
    /// ^(RECORD_DECL type_id type_fields)
    /// type_fields:	type_field (','! type_field)*;
    /// type_field:	ID ':' type_id -> ^(ID type_id);
    /// </summary>
    class RecordDeclarationNode : TypeDeclarationNode
    {
        public RecordDeclarationNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            base.CheckSemantics(scope, report);

            if (report.Assert(this, !scope.IsDefinedInCurrentScopeAsType(NewTypeNode.TypeName),
                              "Type {0} is already defined in the current scope.", NewTypeNode.TypeName))
            {
                var record = scope.DefineRecord(NewTypeNode.TypeName);

                for (int i = 1; i < Children.Count; i++)
                {
                    var currentMember = Children[i];
                    var currentMemberTypeName = ((currentMember as ASTNode).Children[0] as TypeIDNode).TypeName;
                    var couldAddMember = record.AddMember(currentMember.Text, currentMemberTypeName);
                    report.Assert(this, couldAddMember, "Record members must have different names.");
                }
            }
        }

        public override void GenerateCode(CodeGeneration.CodeGenerator cg)
        {
            //get the type info
            var typeinfo = (RecordTypeInfo)Scope.ResolveType(NewTypeNode.TypeName);
            //generate the fields
            var fields = new List<FieldBuilder>();
            var paramlist = new List<Type>();
            foreach (var field in typeinfo.Fields)
            {
                FieldBuilder fieldb = typeinfo.ILTypeBuilder.DefineField(field.Key, field.Value.GetILType(),
                                                                        FieldAttributes.Public);
                fields.Add(fieldb);
                typeinfo.FieldBuilders.Add(field.Key,fieldb);
                paramlist.Add(field.Value.GetILType());
            }

            //creating and getting the constructor
            typeinfo.Constructor = typeinfo.ILTypeBuilder.DefineConstructor(MethodAttributes.Public,
                                                                            CallingConventions.Standard,
                                                                            paramlist.ToArray());
            var cilgen = typeinfo.Constructor.GetILGenerator();

            //generating code for the constructor 
            for (int i = 1; i <= fields.Count; i++)
            {
                //cargar siempre el objeto
                cilgen.Emit(OpCodes.Ldarg_0);
                cilgen.Emit(OpCodes.Ldarg,i);
                cilgen.Emit(OpCodes.Stfld,fields[i-1]);
            }
            cilgen.Emit(OpCodes.Ret);
            //closing the type
            typeinfo.ILTypeBuilder.CreateType();
        }
    }

}

