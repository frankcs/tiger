using System;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using Antlr.Runtime;
using TigerCompiler.CodeGeneration;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.AST.Nodes.Operations.Constants
{
    class StringNode : ConstantNode
    {
        public StringNode(IToken payload) : base(payload)
        {
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            ReturnType = TypeInfo.String;
        }

        public override void GenerateCode(CodeGenerator cg)
        {
            string result = Text;
            result = result.Substring(1, result.Length - 2);
            result = Regex.Replace(result, @"(\\\d\d\d)", new MatchEvaluator(ToAscii));
            result = Regex.Unescape(result);
            cg.IlGenerator.Emit(OpCodes.Ldstr, result);
            
        }

        private string ToAscii(Match m)
        {
            return Convert.ToChar(int.Parse(m.Groups[0].Value.Substring(1))).ToString();
        }
    }
}
