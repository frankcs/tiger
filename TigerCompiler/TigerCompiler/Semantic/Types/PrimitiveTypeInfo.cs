using System;
using TigerCompiler.AST.Nodes;

namespace TigerCompiler.Semantic.Types
{
    public class PrimitiveTypeInfo : TypeInfo
    {
        public PrimitiveTypeInfo(Type type)
        {
            ILType = type;
            ResolutionStatus = TypeResolutionStatus.OK;
        }

        public override Type GetILType()
        {
            return ILType;
        }

        public override bool ResolveReferencedTypes(ASTNode node, Scope scope, ErrorReporter reporter)
        {
            return true;
        }

        public override string ToString()
        {
            return ILType.ToString();
        }
    }
}
