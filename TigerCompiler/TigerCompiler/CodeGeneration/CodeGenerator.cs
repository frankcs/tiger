using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using System.IO;
using System.Threading;
using TigerCompiler.AST.Nodes;

namespace TigerCompiler.CodeGeneration
{
    /// <summary>
    /// Takes care of the overall process of code generation
    /// Generates the Assembly, Module and an entrypoint fuction for 
    /// the Il Generated Code.
    /// Generate code for every built-in tiger function used in the program.  
    /// Grants the utilities to Save the current Assembly into an executable File (.exe)
    /// </summary>
    public class CodeGenerator
    {
        #region Constructors
        public CodeGenerator(string fileName, IEnumerable<string> builtIn)
        {

            string clearname = Path.GetFileNameWithoutExtension(fileName);
            EXEFileName = clearname + ".exe";
            ScopedGenerators= new Stack<ILGenerator>();
            AssemblyName assemName = new AssemblyName("TigerProgram");
            ILAssembly = AppDomain.CurrentDomain.DefineDynamicAssembly(assemName, AssemblyBuilderAccess.Save,
                                                                       Path.GetDirectoryName(fileName));
            ILModule = ILAssembly.DefineDynamicModule(clearname, EXEFileName);
            EntryPoint = ILModule.DefineGlobalMethod("Main", MethodAttributes.Public | MethodAttributes.Static,
                                                     null, null);
            UsedBuiltInFunctionNames = builtIn;
            BuiltInFunctiontoBuilder = new Dictionary<string, MethodBuilder>();

            //Creating the Standard Library
            StandardLibrary = ILModule.DefineType("StandardLibrary", TypeAttributes.Public | TypeAttributes.Class);
            GenerateCodeForBuiltInFunctions();
            StandardLibrary.CreateType();

            EnterGenerationScope(EntryPoint.GetILGenerator()); 
        }
        #endregion

        #region Fields
        /// <summary>
        /// The AssemblyBuilder for our CodeGenerator 
        /// </summary>
        public AssemblyBuilder ILAssembly { get; private set; }

        public ModuleBuilder ILModule { get; private set; }

        /// <summary>
        /// Our "Main" function where ilasm begins its work
        /// </summary>
        public MethodBuilder EntryPoint { get; private set; }

        /// <summary>
        /// The current il generator 
        /// </summary>
        public ILGenerator IlGenerator { get { return ScopedGenerators.Peek(); } }

        /// <summary>
        /// The stacks of generators being used
        /// </summary>
        private Stack<ILGenerator> ScopedGenerators { get; set; }

        /// <summary>
        /// A Type for organizing the StandardLibrary Functions
        /// </summary>
        public TypeBuilder StandardLibrary { get; private set; }

        /// <summary>
        /// Output executable file name
        /// </summary>
        string EXEFileName { get; set; }

        /// <summary>
        /// To know which function built in function should be generated
        /// </summary>
        private IEnumerable<string> UsedBuiltInFunctionNames { get; set; }

        /// <summary>
        /// To acces builders of built in functions by its name
        /// </summary>
        private Dictionary<string, MethodBuilder> BuiltInFunctiontoBuilder { get; set; }

        #endregion

        #region Methods

        #region Utilities
        public void EnterGenerationScope(ILGenerator ilcg)
        {
            ScopedGenerators.Push(ilcg);
        }

        public void LeaveGenerationScope()
        {
            if(EntryPoint.GetILGenerator()==ScopedGenerators.Peek())
                throw new InvalidOperationException("Can't Leave the Scope of the Main function");
            ScopedGenerators.Pop();
        }

        /// <summary>
        /// Genera código para la función de entrada del programa
        /// </summary>
        public void GenerateCode(ASTNode node)
        {
            MethodInfo stringtoint = typeof(Convert).GetMethod("ToInt32", new[] { typeof(string) });
            ILGenerator generator = EntryPoint.GetILGenerator();

            #region Testing built in functions

            #region testing print

            //generator.Emit(OpCodes.Ldstr, "Hello World");
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["print"]);

            #endregion

            #region testing printi

            //generator.Emit(OpCodes.Ldc_I4, 255);
            //generator.Emit(OpCodes.Call,BuiltInFunctiontoBuilder["printi"]);

            #endregion

            #region testing printline

            //generator.Emit(OpCodes.Ldstr, "Hello World");
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["printline"]);

            #endregion

            #region testing flush

            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["flush"]);

            #endregion

            #region testing getchar

            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["getchar"]);
            //generator.DeclareLocal(typeof (string));
            //generator.Emit(OpCodes.Stloc_0);
            //generator.Emit(OpCodes.Ldloc_0);
            //generator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[]{typeof(string)},null));

            #endregion

            #region testing getline

            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["getline"]);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["printline"]);

            #endregion

            #region testing ord

            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["getline"]);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["ord"]);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["printi"]);

            #endregion

            #region testing chr

            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["getline"]);
            //generator.Emit(OpCodes.Call, stringtoint);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["chr"]);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["printline"]);

            #endregion

            #region testing size

            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["getline"]);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["size"]);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["printi"]);

            #endregion

            #region testing substring

            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["getline"]);
            //generator.Emit(OpCodes.Ldc_I4_0);
            //generator.Emit(OpCodes.Ldc_I4_2);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["substring"]);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["printline"]);

            #endregion

            #region testing concat

            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["getline"]);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["getline"]);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["concat"]);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["printline"]);

            #endregion

            #region testing not

            //generator.Emit(OpCodes.Ldc_I4_0);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["not"]);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["printi"]);
            //generator.Emit(OpCodes.Ldc_I4_2);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["not"]);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["printi"]);

            #endregion

            #region testing exit

            //generator.Emit(OpCodes.Ldc_I4_0);
            //generator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["exit"]);

            #endregion

            #endregion

            #region Testing Type and Array Creation

            //TypeBuilder builder = ILModule.DefineType("Person");
            //FieldBuilder fieldBuilder0 = builder.DefineField("Name", typeof(string), FieldAttributes.Public);
            //FieldBuilder fieldBuilder1 = builder.DefineField("Age", typeof(int), FieldAttributes.Public);
            //ConstructorBuilder ctor = builder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard,
            //                          new Type[] { typeof(string), typeof(int) });
            //ILGenerator ctorIlgen = ctor.GetILGenerator();

            //ctorIlgen.Emit(OpCodes.Ldarg_0);
            //ctorIlgen.Emit(OpCodes.Ldarg_1);
            //ctorIlgen.Emit(OpCodes.Stfld, fieldBuilder0);
            //ctorIlgen.Emit(OpCodes.Ldarg_0);
            //ctorIlgen.Emit(OpCodes.Ldarg_2);
            //ctorIlgen.Emit(OpCodes.Stfld, fieldBuilder1);
            //ctorIlgen.Emit(OpCodes.Ret);
            //builder.CreateType();

            //LocalBuilder person = IlGenerator.DeclareLocal(builder);
            //LocalBuilder arrayofperson = IlGenerator.DeclareLocal(builder.MakeArrayType());
            //IlGenerator.Emit(OpCodes.Ldstr, "Frank");
            //IlGenerator.Emit(OpCodes.Ldc_I4, 22);
            //IlGenerator.Emit(OpCodes.Newobj, ctor);
            //IlGenerator.Emit(OpCodes.Stloc_0);
            //IlGenerator.Emit(OpCodes.Ldloc_0);
            //IlGenerator.Emit(OpCodes.Ldfld, fieldBuilder0);
            //IlGenerator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["printline"]);

            //IlGenerator.Emit(OpCodes.Ldc_I4_1);
            //IlGenerator.Emit(OpCodes.Newarr, builder);
            //IlGenerator.Emit(OpCodes.Stloc_1);
            //IlGenerator.Emit(OpCodes.Ldloc_1);
            //IlGenerator.Emit(OpCodes.Ldc_I4_0);
            //IlGenerator.Emit(OpCodes.Ldloc_0);
            //IlGenerator.Emit(OpCodes.Stelem, builder);
            //IlGenerator.Emit(OpCodes.Ldloc_1);
            //IlGenerator.Emit(OpCodes.Ldc_I4_0);
            //IlGenerator.Emit(OpCodes.Ldelem, builder);
            //IlGenerator.Emit(OpCodes.Ldfld, fieldBuilder0);
            //IlGenerator.Emit(OpCodes.Call, BuiltInFunctiontoBuilder["printline"]);

            #endregion

            #region ArrayTypeDecl
            
            #endregion
            node.GenerateCode(this);

            generator.Emit(OpCodes.Ldc_I4, 2000);
            generator.Emit(OpCodes.Call, typeof(Thread).GetMethod("Sleep", new[] { typeof(int) }, null));
            generator.Emit(OpCodes.Ret);
        }

        public void SaveBin()
        {
            ILAssembly.SetEntryPoint(EntryPoint);
            ILModule.CreateGlobalFunctions();
            ILAssembly.Save(EXEFileName);
        }

        #endregion

        #region Generation
      
        /// <summary>
        /// Decides wich bulit-in function is gonna be generated
        /// The idea is to have all the needed built in functions inside the 
        /// same module (ILModule) as the main function i.e. the EntryPoint
        /// </summary>
        private void GenerateCodeForBuiltInFunctions()
        {
            foreach (var usedBuiltInFunctionName in UsedBuiltInFunctionNames)
            {
                MethodBuilder result = null;
                switch (usedBuiltInFunctionName)
                {
                    case "print":
                        result = GeneratePrint();
                        break;
                    case "printline":
                        result = GeneratePrintline();
                        break;
                    case "printi":
                        result = GeneratePrintI();
                        break;
                    case "flush":
                        result = GenerateFlush();
                        break;
                    case "getchar":
                        result = GenerateGetChar();
                        break;
                    case "getline":
                        result = GenerateGetLine();
                        break;
                    case "ord":
                        result = GenerateOrd();
                        break;
                    case "chr":
                        result = GenerateChr();
                        break;
                    case "size":
                        result = GenerateSize();
                        break;
                    case "substring":
                        result = GenerateSubString();
                        break;
                    case "concat":
                        result = GenerateConcat();
                        break;
                    case "not":
                        result = GenerateNot();
                        break;
                    case "exit":
                        result = GenerateExit();
                        break;
                    default:
                        throw new Exception("Built-in Function Not Found");
                }
                if (result != null)
                    BuiltInFunctiontoBuilder.Add(usedBuiltInFunctionName, result);
            }
        }

        private MethodBuilder GeneratePrint()
        {
            //Initialization for method builder and ILgenerator 
            MethodBuilder builder = StandardLibrary.DefineMethod("print",
                                                                MethodAttributes.Public | MethodAttributes.Static, null,
                                                                new Type[] { typeof(string) });
            ILGenerator generator = builder.GetILGenerator();

            //Code Generation
            generator.Emit(OpCodes.Ldarg_0);                                                                        //load param0 to stack
            generator.Emit(OpCodes.Call, typeof(Console).GetMethod("Write", new Type[] { typeof(string) }, null));  //call Method Write of console
            generator.Emit(OpCodes.Ret);

            return builder;

        }

        private MethodBuilder GeneratePrintI()
        {
            //Initialization for method builder and ILgenerator 
            MethodBuilder builder = StandardLibrary.DefineMethod("printi",
                                                                MethodAttributes.Public | MethodAttributes.Static, null,
                                                                new Type[] { typeof(int) });
            ILGenerator generator = builder.GetILGenerator();

            //Code Generation
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Call, typeof(Console).GetMethod("Write", new Type[] { typeof(int) }, null));  //call Method Write of console
            generator.Emit(OpCodes.Ret);

            return builder;
        }

        private MethodBuilder GeneratePrintline()
        {
            //Initialization for method builder and ILgenerator 
            MethodBuilder builder = StandardLibrary.DefineMethod("printline",
                                                                MethodAttributes.Public | MethodAttributes.Static, null,
                                                                new Type[] { typeof(string) });
            ILGenerator generator = builder.GetILGenerator();

            //Code Generation
            generator.Emit(OpCodes.Ldarg_0);                                                                        //load param0 to stack
            generator.Emit(OpCodes.Call, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }, null));  //call Method Write of console
            generator.Emit(OpCodes.Ret);

            return builder;
        }

        private MethodBuilder GenerateFlush()
        {
            //Initialization for method builder and ILgenerator 
            MethodBuilder builder = StandardLibrary.DefineMethod("flush",
                                                                MethodAttributes.Public | MethodAttributes.Static, null,
                                                                null);
            ILGenerator generator = builder.GetILGenerator();

            //Code Generation
            //the idea is : Console.Out.Flush()
            MethodInfo consoledotout = typeof(Console).GetProperty("Out").GetGetMethod();                          //Save a method whose return type is the instance i need
            generator.Emit(OpCodes.Call, consoledotout);                                                            //this call pushhes into the stack the required instance
            MethodInfo flushontw = typeof(TextWriter).GetMethod("Flush");                                          //Get the method flush from TexWritter
            generator.Emit(OpCodes.Call, flushontw);                                                                 //With the instance on the stack call the required method
            generator.Emit(OpCodes.Ret);

            return builder;
        }

        private MethodBuilder GenerateGetChar()
        {
            //Initialization for method builder and ILgenerator 
            MethodBuilder builder = StandardLibrary.DefineMethod("getchar",
                                                                MethodAttributes.Public | MethodAttributes.Static, typeof(string),
                                                                null);
            ILGenerator generator = builder.GetILGenerator();

            //Code Generation
            //the idea is : Char.ToString(Convert.ToChar(Console.Read()))

            generator.Emit(OpCodes.Call, typeof(Console).GetMethod("Read"));                                //read th int
            generator.Emit(OpCodes.Call, typeof(Convert).GetMethod("ToChar", new Type[] { typeof(int) }));     //convert it to Char
            generator.Emit(OpCodes.Call, typeof(Char).GetMethod("ToString", new Type[] { typeof(char) }));  //convert it to string
            generator.Emit(OpCodes.Ret);

            return builder;
        }

        private MethodBuilder GenerateGetLine()
        {
            //Initialization for method builder and ILgenerator 
            MethodBuilder builder = StandardLibrary.DefineMethod("getline",
                                                                MethodAttributes.Public | MethodAttributes.Static, typeof(string),
                                                                null);
            ILGenerator generator = builder.GetILGenerator();

            //Code Generation
            //the idea is : Console.ReadLine()
            generator.Emit(OpCodes.Call, typeof(Console).GetMethod("ReadLine"));                           //read the string
            generator.Emit(OpCodes.Ret);

            return builder;
        }

        private MethodBuilder GenerateOrd()
        {
            //Initialization for method builder and ILgenerator 
            MethodBuilder builder = StandardLibrary.DefineMethod("ord",
                                                                MethodAttributes.Public | MethodAttributes.Static, typeof(int),
                                                                new Type[] { typeof(string) });
            ILGenerator generator = builder.GetILGenerator();

            //Code Generation
            //The idea is: Ask if the empty string or null to return -1
            //if not at lest has one char
            //get the first char with the char property
            //Use Convert to get th ASCII code
            Label nulloremptytargetsite = generator.DefineLabel();
            MethodInfo nullorempymethod = typeof(string).GetMethod("IsNullOrEmpty", new Type[] { typeof(string) });
            MethodInfo convertChartoint = typeof(Convert).GetMethod("ToInt32", new Type[] { typeof(char) });
            PropertyInfo chars = typeof(string).GetProperty("Chars");

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Call, nullorempymethod);
            generator.Emit(OpCodes.Brtrue_S, nulloremptytargetsite);

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldc_I4_0);
            generator.Emit(OpCodes.Call, chars.GetGetMethod());
            generator.Emit(OpCodes.Call, convertChartoint);
            generator.Emit(OpCodes.Ret);

            //on failure set -1 and ret
            generator.MarkLabel(nulloremptytargetsite);
            generator.Emit(OpCodes.Ldc_I4_M1);
            generator.Emit(OpCodes.Ret);

            return builder;
        }

        private MethodBuilder GenerateChr()
        {
            //Initialization for method builder and ILgenerator 
            MethodBuilder builder = StandardLibrary.DefineMethod("chr",
                                                                MethodAttributes.Public | MethodAttributes.Static, typeof(string),
                                                                new Type[] { typeof(int) });
            ILGenerator generator = builder.GetILGenerator();

            //The idea is: chek for the argument to see if it's in range
            //if not jump to code that throws an IndexOutOfRange Exception
            //else return the required string
            Label outofrangetargetsite = generator.DefineLabel();
            ConstructorInfo excep = typeof(ArgumentOutOfRangeException).GetConstructor(new Type[] { typeof(string), typeof(string) });
            MethodInfo inttochar = typeof(Convert).GetMethod("ToChar", new[] { typeof(int) });
            MethodInfo chartostring = typeof(Convert).GetMethod("ToString", new[] { typeof(char) });
            const string paramname = "i";
            const string error = "The argument is not ASCII compliant (0-127)";
            
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldc_I4_0);
            generator.Emit(OpCodes.Clt);        //compare if arg<0, if it's 1 is on the stack else 0

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldc_I4, 127);
            generator.Emit(OpCodes.Cgt);        //compare if arg>127, same as prev

            generator.Emit(OpCodes.Add);        //if some of previous checks was succesfull this operation will 
            //push on the stack 1

            generator.Emit(OpCodes.Brtrue_S, outofrangetargetsite);

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Call, inttochar);         //to char
            generator.Emit(OpCodes.Call, chartostring);      //to string
            generator.Emit(OpCodes.Ret);


            generator.MarkLabel(outofrangetargetsite);

            generator.Emit(OpCodes.Ldstr, paramname);
            generator.Emit(OpCodes.Ldstr, error);
            generator.Emit(OpCodes.Newobj, excep);
            generator.Emit(OpCodes.Throw);
            generator.Emit(OpCodes.Ret);

            return builder;
        }

        private MethodBuilder GenerateSize()
        {
            //Initialization for method builder and ILgenerator 
            MethodBuilder builder = StandardLibrary.DefineMethod("size",
                                                                MethodAttributes.Public | MethodAttributes.Static, typeof(int),
                                                                new Type[] { typeof(string) });
            ILGenerator generator = builder.GetILGenerator();
            MethodInfo getlenght = typeof(string).GetProperty("Length").GetGetMethod();

            //the idea is : string.length
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Call, getlenght);
            generator.Emit(OpCodes.Ret);

            return builder;

        }

        /// <summary>
        /// Throws the exceptions that Substring at String throws
        /// I'm just calling it
        /// </summary>
        /// <returns></returns>
        private MethodBuilder GenerateSubString()
        {
            //Initialization for method builder and ILgenerator 
            MethodBuilder builder = StandardLibrary.DefineMethod("substring",
                                                                MethodAttributes.Public | MethodAttributes.Static, typeof(string),
                                                                new Type[] { typeof(string), typeof(int), typeof(int) });
            ILGenerator generator = builder.GetILGenerator();
            MethodInfo substring = typeof(string).GetMethod("Substring", new Type[] { typeof(int), typeof(int) });

            //idea: string.Substring(i,n)

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Ldarg_2);
            generator.Emit(OpCodes.Call, substring);
            generator.Emit(OpCodes.Ret);

            return builder;
        }

        private MethodBuilder GenerateConcat()
        {
            //Initialization for method builder and ILgenerator 
            MethodBuilder builder = StandardLibrary.DefineMethod("concat",
                                                                MethodAttributes.Public | MethodAttributes.Static, typeof(string),
                                                                new Type[] { typeof(string), typeof(string) });
            ILGenerator generator = builder.GetILGenerator();
            MethodInfo concat = typeof(string).GetMethod("Concat", new Type[] { typeof(string), typeof(string) });

            //idea: string.Concat(str1,str2)

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Call, concat);
            generator.Emit(OpCodes.Ret);

            return builder;
        }

        private MethodBuilder GenerateNot()
        {
            //Initialization for method builder and ILgenerator 
            MethodBuilder builder = StandardLibrary.DefineMethod("not",
                                                                MethodAttributes.Public | MethodAttributes.Static, typeof(int),
                                                                new Type[] { typeof(int) });
            ILGenerator generator = builder.GetILGenerator();
            Label argiszero = generator.DefineLabel();

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Brfalse, argiszero);
            generator.Emit(OpCodes.Ldc_I4_0);
            generator.Emit(OpCodes.Ret);

            generator.MarkLabel(argiszero);
            generator.Emit(OpCodes.Ldc_I4_1);
            generator.Emit(OpCodes.Ret);

            return builder;
        }

        private MethodBuilder GenerateExit()
        {
            //Initialization for method builder and ILgenerator 
            MethodBuilder builder = StandardLibrary.DefineMethod("exit",
                                                                MethodAttributes.Public | MethodAttributes.Static, null,
                                                                new Type[] { typeof(int) });
            ILGenerator generator = builder.GetILGenerator();
            MethodInfo exit = typeof (Environment).GetMethod("Exit", new Type[] {typeof (int)});

            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Call,exit);
            generator.Emit(OpCodes.Ret);
            return builder;
        }
        #endregion

        #endregion
    }
}
