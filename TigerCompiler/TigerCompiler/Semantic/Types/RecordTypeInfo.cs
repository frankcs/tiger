using System;
using System.Collections.Generic;
using TigerCompiler.AST.Nodes;

namespace TigerCompiler.Semantic.Types
{
    public class RecordTypeInfo : TypeInfo
    {
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
            throw new NotImplementedException(); // FRANK
        }

        public override bool ResolveReferencedTypes(ASTNode node, Scope scope, ErrorReporter reporter)
        {
            bool ok = true;

            ResolutionStatus = TypeResolutionStatus.Resolving;

            foreach (var nameTypeTuple in _preFields)
            {
                var tmp = scope.ResolveType(nameTypeTuple.Value);
                if (tmp != null)
                {
                    if (tmp.ResolutionStatus == TypeResolutionStatus.NotResolved)
                        tmp.ResolveReferencedTypes(node, scope, reporter);
                    Fields.Add(nameTypeTuple.Key, tmp);
                }else ok = false;
            }

            ResolutionStatus = ok? TypeResolutionStatus.OK : TypeResolutionStatus.Error;
            return ok;
        }

    }
}
