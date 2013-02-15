using System;
using TigerCompiler.Semantic.Symbols;

namespace TigerCompiler.Semantic.Types
{
    public abstract class TypeInfo : Symbol
    {
        static TypeInfo()
        {
            Int = new PrimitiveTypeInfo(typeof(int)){IsReadOnly = true};
            String = new PrimitiveTypeInfo(typeof(string)) { IsReadOnly = true };
            Void = new PrimitiveTypeInfo(typeof(void)) { IsReadOnly = true };
            Unknown = new PrimitiveTypeInfo(null) { IsReadOnly = true};
        }

        public static readonly PrimitiveTypeInfo Int;
        public static readonly PrimitiveTypeInfo String;
        public static readonly PrimitiveTypeInfo Void;

        /// <summary>
        /// An expression returns an unknown type when type inference does not work.
        /// TODO: Does this propagate? What happens then?
        /// </summary>
        public static readonly PrimitiveTypeInfo Unknown;
        
        /// <summary>
        /// Holds the generated ReturnType object for the current type.
        /// </summary>
        protected Type ILType;

        /// <summary>
        /// Singleton. Creates the ReturnType, if it is not already made.
        /// </summary>
        public abstract Type GetILType();

        /// <summary>
        /// Allows or not overwriting this symbol's meaning.
        /// </summary>
        public bool IsReadOnly { get; set; }

        public TypeResolutionStatus ResolutionStatus { get; set; }

        /// <summary>
        /// Resolves types.
        /// </summary>
        /// <param name="scope">The scope to resolve in.</param>
        public abstract void ResolveReferencedTypes(Scope scope);
    }
}
