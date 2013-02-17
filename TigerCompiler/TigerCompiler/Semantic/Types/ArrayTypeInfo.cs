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
            if (ResolutionStatus != TypeResolutionStatus.NotResolved)
                return ResolutionStatus == TypeResolutionStatus.OK;
            ResolutionStatus = TypeResolutionStatus.Resolving;
            if (TargetType == null)
                TargetType = scope.ResolveType(_targetTypeName);

            if (reporter.Assert(node,!IsNull(TargetType),"Unknown type: {0}.",_targetTypeName))
            {
                bool ok = TargetType.ResolveReferencedTypes(node, scope, reporter);
                ResolutionStatus = ok? TypeResolutionStatus.OK : TypeResolutionStatus.Error;
                return ok;
            }
            return false;
        }

        public override bool IsNullable
        {
            get { return true; }
            set
            {
                throw new InvalidOperationException();
            }
        }
    }
}
