using System.Collections.Generic;
using System.Reflection.Emit;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.Semantic
{
    public class VariableInfo : Symbols.Symbol
    {
        public bool IsReadOnly { get; set; }

        public TypeInfo VariableType { get; set; }

        public LocalBuilder ILLocalVariable { get; set; }

        public Dictionary<string,TypeInfo> Fields
        {
            get{
                var recordTypeInfo = VariableType as RecordTypeInfo;
                return recordTypeInfo != null ? recordTypeInfo.Fields : null;
            }
        }

        //public bool IsHidingAnother { get; set; }
    }
}
