using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Antlr.Runtime;
using Antlr.Runtime.Tree;
using TigerCompiler.AST;

namespace TigerCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0 && File.Exists(args[0]))
            {
                ICharStream characters = new ANTLRFileStream(args[0]);
                TigerLexer lexer = new TigerLexer(characters);
                ITokenStream tokens = new CommonTokenStream(lexer);
                TigerParser parser = new TigerParser(tokens);
                parser.TraceDestination = Console.Out;
                var result = parser.program();
                if (parser.NumberOfSyntaxErrors > 0)
                    Console.WriteLine("Entrada incorrecta. {0} error(es) cometido(s)", parser.NumberOfSyntaxErrors);
            }
            else
                Console.WriteLine("La ruta no es valida o el archivo no existe");
        }
    }
}

public partial class TigerParser
{
    partial void CreateTreeAdaptor(ref ITreeAdaptor adaptor)
    {
        adaptor = new OurTreeAdaptor();
    }
}
