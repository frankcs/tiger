using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TigerCompiler.AST;

namespace Code_Generation
{
    public abstract class TigerInfo
    {
        public string Name { get; set; }

        public TypeIDNode TypeNode { get; set; }
    }
}
