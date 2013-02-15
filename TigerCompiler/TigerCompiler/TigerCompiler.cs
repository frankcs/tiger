using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Antlr.Runtime;
using TigerCompiler.AST.Nodes;
using TigerCompiler.AST.Nodes.Helpers;
using TigerCompiler.CodeGeneration;
using TigerCompiler.Semantic;

namespace TigerCompiler
{
    class TigerCompiler
    {
        public static void Main(string[] args)
        {
            if (args.Length > 0 && File.Exists(args[0]))
                Environment.Exit(Compile(args[0])); 
            else
                Console.WriteLine("La ruta no es valida o el archivo no existe");
        }

        private static int Compile(string filename)
        {
            ICharStream characters = new ANTLRFileStream(filename);
            TigerLexer lexer = new TigerLexer(characters);
            ITokenStream tokens = new CommonTokenStream(lexer);
            TigerParser parser = new TigerParser(tokens);

            parser.TraceDestination = Console.Out;
            var result = parser.program();

            if (parser.NumberOfSyntaxErrors > 0)
            {
                Console.WriteLine("Entrada incorrecta. {0} error(es) cometido(s)", parser.NumberOfSyntaxErrors);
                return 1;
            }

            ((ProgramNode) result.Tree).CheckSemantics(Scope.DefaultGlobalScope,Scope.DefaultGlobalScope.Reporter);

            if (Scope.DefaultGlobalScope.Reporter.errors.Count >0)
            {
                return 1;
            }
            var generator = new CodeGenerator(Environment.CurrentDirectory+ Path.DirectorySeparatorChar + filename,
                                              new[] {"print","printi", "printline", "flush", 
                                                     "getchar", "getline", "ord", "chr", "size",
                                                     "substring","concat", "not", "exit"
                                                    });
            generator.GenerateCode((ASTNode)result.Tree);
            generator.SaveBin();

            return 0;

        }
    }
}
