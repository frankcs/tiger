using System;
using TigerCompiler.AST.Nodes;

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

        public override bool ResolveReferencedTypes(ASTNode node, Scope scope, ErrorReporter reporter)
        {
            if (ResolutionStatus != TypeResolutionStatus.NotResolved) return ResolutionStatus == TypeResolutionStatus.OK;

            ResolutionStatus = TypeResolutionStatus.Resolving;

            TargetType = scope.ResolveType(_targetTypeName);

            if (reporter.Assert(node,!ReferenceEquals(TargetType,null),"Unknown type in the current context: {0}",_targetTypeName))
            { 
                if (TargetType.ResolutionStatus == TypeResolutionStatus.NotResolved)
                    TargetType.ResolveReferencedTypes(node, scope, reporter);
                switch (TargetType.ResolutionStatus)
                {
                    case TypeResolutionStatus.Resolving:
                    case TypeResolutionStatus.Error:
                        ResolutionStatus = TypeResolutionStatus.Error;
                        return false;
                    case TypeResolutionStatus.OK:
                        TargetType = (TargetType is AliasTypeInfo) ? ((AliasTypeInfo)TargetType).TargetType : TargetType;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                ResolutionStatus = TypeResolutionStatus.OK;
                return true;
            }
            return false;
        }
    }
}
