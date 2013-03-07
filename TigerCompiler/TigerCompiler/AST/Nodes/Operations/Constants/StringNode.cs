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
        private bool error;

        public StringNode(IToken payload) : base(payload)
        {
            ParsedText = Text.Substring(1, Text.Length - 2);
            ParsedText = Regex.Replace(ParsedText, @"(\\\d\d\d)", ToAscii);
            ParsedText = Regex.Unescape(ParsedText);
        }

        public override void CheckSemantics(Semantic.Scope scope, Semantic.ErrorReporter report)
        {
            // Esto hace falta hacerlo a pesar de que esta expresado en la gramatica de ANTLR,
            // porque al parecer la generacion de codigo en c# tiene algun bateo.
            // El codigo generado en Java da error correctamente. En C#, los errores se los traga.
            if (error)
                report.AddError(this,"Invalid escape sequence detected.");
            ReturnType = TypeInfo.String;
        }

        private string ParsedText { get; set; }

        public override void GenerateCode(CodeGenerator cg)
        {
            cg.IlGenerator.Emit(OpCodes.Ldstr, ParsedText);
        }

        private string ToAscii(Match m)
        {
            var a = int.Parse(m.Groups[0].Value.Substring(1));
            if (a >= 128)
            {
                error = true;
            }
            return Convert.ToChar(a).ToString();
        }
    }
}
