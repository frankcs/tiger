using System.Collections.Generic;
using TigerCompiler.AST.Nodes;
using TigerCompiler.Semantic.Symbols;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.Semantic
{
    public class Scope
    {
        public static readonly Scope DefaultGlobalScope = new Scope(new ErrorReporter());

        static Scope()
        {
            // Built-in types
            DefaultGlobalScope.TypeTable.Add("int", TypeInfo.Int);
            DefaultGlobalScope.TypeTable.Add("string", TypeInfo.String);

            // Built-in functions
            FunctionInfo.DefineBuiltInFunctions(DefaultGlobalScope);
        }

        private Scope(ErrorReporter reporter)
        {
            Reporter = reporter;
        }

        public Scope Parent { get; private set; }
        public ErrorReporter Reporter { get; private set; }

        private readonly List<Scope> _childScopes = new List<Scope>();
        private readonly Dictionary<string, Symbol> SymbolTable = new Dictionary<string, Symbol>();
        private readonly Dictionary<string, TypeInfo> TypeTable = new Dictionary<string, TypeInfo>();

        public Scope CreateChildScope()
        {
            var newScope = new Scope(Reporter);
            _childScopes.Add(newScope);
            newScope.Parent = this;
            return newScope;
        }

        /// <summary>
        /// Defines a variable in the current scope.
        /// </summary>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="type">The type of the variable. Must exist in the current scope.</param>
        /// <param name="readOnly">True if the variable cannot be overwritten. Only used for the indexer in the for loop.</param>
        /// <returns>TODO</returns>
        public VariableInfo DefineVariable(string variableName, TypeInfo type, bool readOnly = false)
        {
            var vinfo = new VariableInfo() { IsReadOnly = readOnly, VariableType = type};
            SymbolTable[variableName] =vinfo;
            return vinfo;
        }

        /// <summary>
        /// Returns true if a variable or function is defined in the current scope with the same name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsDefinedInCurrentScopeAsVarOrFunc(string name)
        {
            return SymbolTable.ContainsKey(name);
        }

        /// <summary>
        /// Returns true if there is a type declared with the same name in the current scope.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsDefinedInCurrentScopeAsType(string name)
        {
            return TypeTable.ContainsKey(name);
        }

        /// <summary>
        /// Defines a function in the current scope.
        /// </summary>
        /// <param name="functionName">The name of the function.</param>
        /// <param name="returnType">The return type of the function.</param>
        /// <param name="readOnly">True if the function cannot be overwritten. Used for built-in functions.</param>
        /// <returns>TODO</returns>
        public FunctionInfo DefineFunction(string functionName, TypeInfo returnType, bool readOnly = false)
        {
            var finfo = new FunctionInfo(this) {IsReadOnly = readOnly, ReturnType = returnType};
            SymbolTable[functionName] = finfo;
            return finfo;
        }

        /// <summary>
        /// Defines a procedure in the current scope.
        /// </summary>
        /// <param name="procedureName">The name of the procedure.</param>
        /// <param name="readOnly">True if the procedure cannot be overwritten. Used for built-in functions.</param>
        /// <returns>TODO</returns>
        public FunctionInfo DefineFunction(string procedureName, bool readOnly = false)
        {
            var finfo = new FunctionInfo(this) { IsReadOnly = readOnly, ReturnType = TypeInfo.Void };
            SymbolTable.Add(procedureName,finfo);
            return finfo;
        }

        /// <summary>
        /// Defines an array type in the current scope.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <param name="targetArrayTypeName">The name of the target type for the array.</param>
        /// <returns>TODO</returns>
        public ArrayTypeInfo DefineArray(string typeName, string targetArrayTypeName)
        {
            var arInfo = new ArrayTypeInfo(targetArrayTypeName);
            TypeTable.Add(typeName,arInfo);
            return arInfo;
        }

        /// <summary>
        /// Defines a record type in the current scope.
        /// The members of the array must be added to the record type by using AddMember.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <returns>TODO</returns>
        public RecordTypeInfo DefineRecord(string typeName)
        {
            var rinfo = new RecordTypeInfo();
            TypeTable.Add(typeName, rinfo);
            return rinfo;
        }

        /// <summary>
        /// Defines an alias for a type.
        /// </summary>
        /// <param name="alias">The alias to define.</param>
        /// <param name="typeName">The name of the type to create an alias for.</param>
        /// <returns></returns>
        public void DefineAlias(string alias, string typeName)
        {
            var alInfo = new AliasTypeInfo(typeName);
            TypeTable.Add(alias,alInfo);
        }

        /// <summary>
        /// Finds a variable or function defined in this scope or any parent scope.
        /// </summary>
        /// <param name="key">The variable or function name to look for.</param>
        /// <returns></returns>
        public Symbol ResolveVarOrFunction(string key)
        {
            return SymbolTable.ContainsKey(key) ? SymbolTable[key] : Parent != null ? Parent.ResolveVarOrFunction(key) : null;
        }

        /// <summary>
        /// Finds a type defined in this scope or any parent scope.
        /// </summary>
        /// <param name="key">The name of the type to look for.</param>
        /// <returns></returns>
        public TypeInfo ResolveType(string key)
        {
            if (!TypeTable.ContainsKey(key))
                return Parent != null ? Parent.ResolveType(key) : null;
            //var typeInfo = 
            return TypeTable[key];
            //var aliasTypeInfo = typeInfo as AliasTypeInfo;
            //return aliasTypeInfo != null ? aliasTypeInfo.TargetType : typeInfo;
        }

        public bool TypeIsVisibleInSomeParentScope(TypeInfo type)
        {
            if (ReferenceEquals(this,Scope.DefaultGlobalScope))
            {
                return true;
            }
            return Parent != null &&
                   (Parent.TypeTable.ContainsValue(type) || Parent.TypeIsVisibleInSomeParentScope(type));
        }
    }

}
