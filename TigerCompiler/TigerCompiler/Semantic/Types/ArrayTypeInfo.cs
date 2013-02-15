using System;

namespace TigerCompiler.Semantic.Types
{
    public class ArrayTypeInfo : TypeInfo
    {
        public ArrayTypeInfo(string targetTypeName)
        {
            _targetTypeName = targetTypeName;
        }

        private readonly string _targetTypeName;
        public TypeInfo TargetType { get;  private set; }

        public override Type GetILType()
        {
            // No funciona asi. Al parecer, lo siguiente es valido:
            //  type a = array of b
            //  type b = array of a
            // ...por lo cual, como no es posible tener el tipo del primero
            // antes de crear el 2do, entonces no se puede usar MakeArrayType.
            return ILType ?? (ILType = TargetType.GetILType().MakeArrayType());
        }

        public override void ResolveReferencedTypes(Scope scope)
        {
            ResolutionStatus = TypeResolutionStatus.Resolving;
            if (TargetType == null)
                TargetType = scope.ResolveType(_targetTypeName);
            if (TargetType.ResolutionStatus == TypeResolutionStatus.NotResolved)
                TargetType.ResolveReferencedTypes(scope);
            ResolutionStatus = TypeResolutionStatus.OK;
        }
    }
}
