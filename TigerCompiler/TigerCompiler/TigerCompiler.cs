﻿using System;
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
            Console.WriteLine("Tiger Compiler version 0.6");
            Console.WriteLine("Copyright (C) 2012-2013 Frank E. Perez & Alex R. Coto\n");
            if (args.Length > 0 && File.Exists(args[0]))
                Environment.Exit(Compile(args[0])); 
            else
                Console.WriteLine("(0,0) File {0} cannot be found.",args[0]);
        }

        private static int Compile(string filename)
        {
            ICharStream characters = new ANTLRFileStream(filename);
            var lexer = new TigerLexer(characters);
            ITokenStream tokens = new CommonTokenStream(lexer);
            var parser = new TigerParser(tokens);

            //parser.TraceDestination = Console.Out;
            var result = parser.program();

            if (parser.NumberOfSyntaxErrors > 0 || lexer.Error)
                return 1;

            ((ProgramNode) result.Tree).CheckSemantics(Scope.DefaultGlobalScope,Scope.DefaultGlobalScope.Reporter);

            if (Scope.DefaultGlobalScope.Reporter.Errors.Count >0)
            {
                Scope.DefaultGlobalScope.Reporter.PrintErrors(Console.Out);
                return 1;
            }

            var generator = new CodeGenerator(Environment.CurrentDirectory+ Path.DirectorySeparatorChar + filename,
                                              new[] {"print","printi", "printline", "flush", 
                                                     "getchar", "getline", "ord", "chr", "size",
                                                     "substring","concat", "not", "exit"
                                                    });
            generator.GenerateCode((ASTNode)result.Tree);
            generator.SaveBin();

            Console.WriteLine("The program has compiled successfully.");
            return 0;

        }
    }
}