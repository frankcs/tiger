using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using TigerCompiler.Semantic.Types;

namespace TigerCompiler.Semantic
{
    public class FunctionInfo : Symbols.Symbol
    {
        public bool IsReadOnly { get; set; }

        public FunctionInfo(Scope parentScope)
        {
            FunctionScope = parentScope.CreateChildScope();
        }

        public TypeInfo ReturnType { get; set; }

        public Scope FunctionScope { get; private set; }

        public readonly Dictionary<string, TypeInfo> Parameters = new Dictionary<string, TypeInfo>();

        public bool AddParameter(string paramName, TypeInfo type)
        {
            if (Parameters.ContainsKey(paramName))
                return false;
            FunctionScope.DefineVariable(paramName, type);
            Parameters.Add(paramName,type);
            return true;
        }

        public static void DefineBuiltInFunctions(Scope scope)
        {
            //function print(s : string)
            var print = scope.DefineFunction("print", true);
            print.AddParameter("s", TypeInfo.String);
            Scope.BuiltInFunctions.Add(new KeyValuePair<string, FunctionInfo>("print",print));

            //function printi(i : int)
            var printi = scope.DefineFunction("printi",true);
            printi.AddParameter("i", TypeInfo.Int);
            Scope.BuiltInFunctions.Add(new KeyValuePair<string, FunctionInfo>("printi", printi));
            //function flush()

            var flush = scope.DefineFunction("flush",true);
            Scope.BuiltInFunctions.Add(new KeyValuePair<string, FunctionInfo>("flush", flush));

            //function getchar() : string
            var getchar = scope.DefineFunction("getchar", TypeInfo.String, true);
            Scope.BuiltInFunctions.Add(new KeyValuePair<string, FunctionInfo>("getchar", getchar));

            //function ord(s:string) : int
            var ord = scope.DefineFunction("ord", TypeInfo.Int,true);
            ord.AddParameter("s", TypeInfo.String);
            Scope.BuiltInFunctions.Add(new KeyValuePair<string, FunctionInfo>("ord", ord));

            //function chr(i : int) : string
            var chr = scope.DefineFunction("chr", TypeInfo.String, true);
            chr.AddParameter("i", TypeInfo.Int);
            Scope.BuiltInFunctions.Add(new KeyValuePair<string, FunctionInfo>("chr", chr));

            //function size(s : string) : int
            var size = scope.DefineFunction("size", TypeInfo.Int, true);
            size.AddParameter("s", TypeInfo.String);
            Scope.BuiltInFunctions.Add(new KeyValuePair<string, FunctionInfo>("size", size));

            //function substring(s:string,f:int,n:int):string
            var substring = scope.DefineFunction("substring", TypeInfo.String, true);
            substring.AddParameter("s", TypeInfo.String);
            substring.AddParameter("f", TypeInfo.Int);
            substring.AddParameter("n", TypeInfo.Int);
            Scope.BuiltInFunctions.Add(new KeyValuePair<string, FunctionInfo>("substring", substring));

            //function concat (s1:string, s2:string):string
            var concat = scope.DefineFunction("concat", TypeInfo.String, true);
            concat.AddParameter("s1", TypeInfo.String);
            concat.AddParameter("s2", TypeInfo.String);
            Scope.BuiltInFunctions.Add(new KeyValuePair<string, FunctionInfo>("concat", concat));

            //function not(i : int) : int
            var not = scope.DefineFunction("not", TypeInfo.Int, true);
            not.AddParameter("i", TypeInfo.Int);
            Scope.BuiltInFunctions.Add(new KeyValuePair<string, FunctionInfo>("not", not));

            //function exit(i : int)
            var exit = scope.DefineFunction("exit", true);
            exit.AddParameter("i", TypeInfo.Int);
            Scope.BuiltInFunctions.Add(new KeyValuePair<string, FunctionInfo>("exit", exit));

            //function getline():string
            var getline=scope.DefineFunction("getline", TypeInfo.String);
            Scope.BuiltInFunctions.Add(new KeyValuePair<string, FunctionInfo>("getline", getline));

            //function printline(s:string)
            var printline = scope.DefineFunction("printline");
            printline.AddParameter("s", TypeInfo.String);
            Scope.BuiltInFunctions.Add(new KeyValuePair<string, FunctionInfo>("printline", printline));
        }

        public MethodBuilder ILMethod { get; set; }

        public List<KeyValuePair<string, VariableInfo>> Locals { get; set; }
    }
}
