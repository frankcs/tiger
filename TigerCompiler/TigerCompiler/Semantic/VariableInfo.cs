using System.Reflection.Emit;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.Semantic
{
    public class VariableInfo : Symbols.Symbol
    {
        public bool IsReadOnly { get; set; }

        public TypeInfo VariableType { get; set; }

        public LocalBuilder ILLocalVariable { get; set; }
    }
}
