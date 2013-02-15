using System;

namespace TigerCompiler.Semantic.Types
{
    public class PrimitiveTypeInfo : TypeInfo
    {
        public PrimitiveTypeInfo(Type type)
        {
            ILType = type;
        }

        public override Type GetILType()
        {
            return ILType;
        }

        public override void ResolveReferencedTypes(Scope scope)
        {
            ResolutionStatus = TypeResolutionStatus.OK;
        }
    }
}
