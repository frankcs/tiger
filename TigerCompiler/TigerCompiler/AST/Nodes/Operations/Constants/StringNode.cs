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
            ParsedText= Text.Substring(1, Text.Length - 2);
            ParsedText = Regex.Replace(ParsedText, @"(\\\d\d\d)", new MatchEvaluator(ToAscii));
            ParsedText = Regex.Unescape(ParsedText);
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            ReturnType = TypeInfo.String;
        }

        private string ParsedText { get; set; }

        public override void GenerateCode(CodeGenerator cg)
        {
            
            cg.IlGenerator.Emit(OpCodes.Ldstr, ParsedText);
            
        }

        private string ToAscii(Match m)
        {
            return Convert.ToChar(int.Parse(m.Groups[0].Value.Substring(1))).ToString();
        }
    }
}
