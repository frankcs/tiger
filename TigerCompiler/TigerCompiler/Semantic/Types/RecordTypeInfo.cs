using System;
using System.Collections.Generic;

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
                // TODO: Pasar un ErrorReporter por si hay dos miembros con el mismo nombre?
            }
            return false;
        }

        public override Type GetILType()
        {
            throw new NotImplementedException(); // FRANK
        }

        public override void ResolveReferencedTypes(Scope scope)
        {
            ResolutionStatus = TypeResolutionStatus.Resolving;

            foreach (var nameTypeTuple in _preFields)
            {
                var tmp = scope.ResolveType(nameTypeTuple.Value);
                if (tmp.ResolutionStatus == TypeResolutionStatus.NotResolved)
                    tmp.ResolveReferencedTypes(scope);
                
                Fields.Add(nameTypeTuple.Key, tmp);
            }

            ResolutionStatus = TypeResolutionStatus.OK;
        }
    }
}
