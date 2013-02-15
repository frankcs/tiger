using System;

namespace TigerCompiler.Semantic.Types
{
    public class AliasTypeInfo : TypeInfo
    {
        private readonly string _targetTypeName;

        public AliasTypeInfo(string targetTypeName)
        {
            _targetTypeName = targetTypeName;
        }

        public TypeInfo TargetType { get; private set; }
    
        public override Type GetILType()
        {
            return TargetType.GetILType();
        }

        public override void ResolveReferencedTypes(Scope scope)
        {
            if (ResolutionStatus != TypeResolutionStatus.NotResolved) return;

            ResolutionStatus = TypeResolutionStatus.Resolving;

            TargetType = scope.ResolveType(_targetTypeName);
            switch (TargetType.ResolutionStatus)
            {
                case TypeResolutionStatus.NotResolved:
                case TypeResolutionStatus.Resolving:
                case TypeResolutionStatus.HasCycles:
                    ResolutionStatus = TypeResolutionStatus.HasCycles;
                    scope.Reporter.AddError("Cycle detected.");
                    return;
                case TypeResolutionStatus.OK:
                    TargetType = (TargetType is AliasTypeInfo) ? ((AliasTypeInfo)TargetType).TargetType : TargetType;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
                
            ResolutionStatus = TypeResolutionStatus.OK;
        }
    }
}
