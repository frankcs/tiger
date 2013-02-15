using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TigerCompiler.AST.Nodes;

namespace TigerCompiler.Semantic
{
    public class ErrorReporter
    {
        public List<Exception> errors = new List<Exception>();

        public void Assert(ASTNode node, bool condition, string errorMsg, params object[] args)
        {
            if (!condition)
                errors.Add(new Exception(String.Format(errorMsg,args)));
        }

        public void AddError(string error, params object[] args)
        {
            errors.Add(new Exception(String.Format(error,args)));
        }
    }
}
