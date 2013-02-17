using System;
using TigerCompiler.AST.Nodes;

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
            return ILType ?? (ILType = TargetType.GetILType().MakeArrayType());
        }

        public override bool ResolveReferencedTypes(ASTNode node,Scope scope, ErrorReporter reporter)
        {
            bool ok = false;
            ResolutionStatus = TypeResolutionStatus.Resolving;
            if (TargetType == null)
                TargetType = scope.ResolveType(_targetTypeName);
            if (reporter.Assert(node,!IsNull(TargetType),"Unknown type: {0}.",_targetTypeName))
            {
                if (TargetType.ResolutionStatus == TypeResolutionStatus.NotResolved)
                    ok = TargetType.ResolveReferencedTypes(node, scope, reporter);
                ResolutionStatus = TypeResolutionStatus.OK;
                return ok;
            }
            return false;
        }
    }
}
