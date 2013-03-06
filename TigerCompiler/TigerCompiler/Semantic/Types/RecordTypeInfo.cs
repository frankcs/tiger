using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using TigerCompiler.AST.Nodes;

namespace TigerCompiler.Semantic.Types
{
    public class RecordTypeInfo : TypeInfo
    {
        public RecordTypeInfo()
        {
            FieldBuilders= new Dictionary<string, FieldBuilder>();
        }

        private readonly Dictionary<string, string> _preFields = new Dictionary<string, string>();
        public readonly Dictionary<string, TypeInfo> Fields = new Dictionary<string, TypeInfo>();

        /// <summary>
        /// If AddMember returns false, a duplicate member name was detected.
        /// </summary>
        /// <param name="memberName"></param>
        /// <param name="memberTypeName"></param>
        /// <returns></returns>
        public bool AddMember(string memberName, string memberTypeName)
        {
            if (!_preFields.ContainsKey(memberName))
            {
                _preFields.Add(memberName, memberTypeName);
                return true;
            }
            return false;
        }

        public override Type GetILType()
        {
            return ILTypeBuilder; // FRANK
        }

        public override bool IsNullable { get { return true; } set{throw new InvalidOperationException();} }

        public override bool ResolveReferencedTypes(ASTNode node, Scope scope, ErrorReporter reporter)
        {
            if (ResolutionStatus != TypeResolutionStatus.NotResolved)
                return true;

            ResolutionStatus = TypeResolutionStatus.OK;

            //ResolutionStatus = TypeResolutionStatus.Resolving;

            foreach (var nameTypeTuple in _preFields)
            {
                var tmp = scope.ResolveType(nameTypeTuple.Value);

                if (!reporter.Assert(node, !IsNull(tmp), "Cannot find type {0} in current context.", nameTypeTuple.Value))
                    continue;
                if (tmp.ResolutionStatus == TypeResolutionStatus.NotResolved)
                    tmp.ResolveReferencedTypes(node, scope, reporter);
                Fields.Add(nameTypeTuple.Key, tmp);
            }

            return true;
        }

        public ConstructorBuilder Constructor { get; set; }

        public Dictionary<string, FieldBuilder> FieldBuilders { get; set; }
    }
}
