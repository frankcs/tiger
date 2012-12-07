using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr.Runtime;
using Antlr.Runtime.Tree;

namespace TigerCompiler.AST
{
    class OurTreeAdaptor : CommonTreeAdaptor
    {
        public override object Create(IToken payload)
        {
            switch (payload.Type)
            {
                case TigerParser.PROGRAM:
                    break;
                case TigerParser.TYPE_DECL:
                    break;
                case TigerParser.VAR_DECL:
                    break;
                case TigerParser.ARRAY_TYPE_DECL:
                    break;
                case TigerParser.ALIAS_DECL:
                    break;
                case TigerParser.OR:
                    break;
                case TigerParser.AND:
                    break;
                case TigerParser.ASSIGN:
                    break;
                case TigerParser.GT:
                    break;
                case TigerParser.GT_EQUAL:
                    break;
                case TigerParser.LT:
                    break;
                case TigerParser.LT_EQUAL:
                    break;
                case TigerParser.EQUAL:
                    break;
                case TigerParser.NON_EQUAL:
                    break;
                case TigerParser.PLUS:
                    break;
                case TigerParser.MINUS:
                    break;
                case TigerParser.MUL:
                    break;
                case TigerParser.DIV:
                    break;
                    
                
                    

                default:
                    throw new ArgumentOutOfRangeException("Unrecognized token.");
            }

        }
    }

    internal class ProgramNode : IToken
    {

    }
}
