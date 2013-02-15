using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TigerCompiler.AST.Nodes;

namespace TigerCompiler.Semantic
{
    public class ErrorReporter
    {
        public readonly List<string> Errors = new List<string>();

        public bool Assert(ASTNode node, bool condition, string errorMsg, params object[] args)
        {
            if (!condition)
                Errors.Add((String.Format("({0},{1}): {2}", node.Line, node.CharPositionInLine, String.Format(errorMsg, args))));
            return condition;
        }

        public void AddError(ASTNode node, string errorMsg, params object[] args)
        {
            Errors.Add((String.Format("({0},{1}): {2}", node.Line, node.CharPositionInLine, String.Format(errorMsg, args))));
        }

        public void PrintErrors(TextWriter tw)
        {
            foreach (var exception in Errors)
            {
                tw.WriteLine(exception);
            }
        }
    }
}
