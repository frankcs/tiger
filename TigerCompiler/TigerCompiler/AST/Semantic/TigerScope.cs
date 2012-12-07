using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Code_Generation;

namespace TigerCompiler.AST.Semantic
{
    public class Scope
    {
        public Scope Parent { get; set; }

        public Scope[] Children { get; set; }

        //int ParentIndex { get; set; }

        public Dictionary<string, TigerInfo> Table { get; set; }

        Scope CreateChildScope()
        {
            throw new NotImplementedException();
        }

        public void AddVariable(VariableInfo varinfo)
        {
            throw new NotImplementedException();
        }

        public VariableInfo FindVariableInfo(string name)
        {
            throw new NotImplementedException();
        }

        public bool CanFindVariable(string name)
        {
            throw new NotImplementedException();
        }

        public void AddFunction(FunctionInfo varinfo)
        {
            throw new NotImplementedException();
        }

        public FunctionInfo FindFunctionInfo(string name)
        {
            throw new NotImplementedException();
        }

        public bool CanFindFunction(string name)
        {
            throw new NotImplementedException();
        }

        public void AddType(TypeInfo varinfo)
        {
            throw new NotImplementedException();
        }

        public TypeInfo FindTypeInfo(string name)
        {
            throw new NotImplementedException();
        }

        public bool CanFindType(string name)
        {
            throw new NotImplementedException();
        }
    }
}
