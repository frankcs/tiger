﻿using System.Collections.Generic;
using System.Reflection.Emit;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.Semantic
{
    public class VariableInfo : Symbols.Symbol
    {
        public bool IsReadOnly { get; set; }

        public TypeInfo VariableType { get; set; }

        public FieldBuilder ILLocalVariable { get; set; }

        public Dictionary<string,TypeInfo> Fields
        {
            get{
                var recordTypeInfo = TypeInfo.RecordFromTypeInfo(VariableType);
                return recordTypeInfo != null ? recordTypeInfo.Fields : null;
            }
        }

        //public bool IsHidingAnother { get; set; }
    }
}
