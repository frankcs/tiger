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
            if (payload == null)
            {
                return base.Create(payload);
            }
            
            switch ((TigerTokenTypes)payload.Type)
            {
                #region Old
                //case TigerParser.PROGRAM:
                //    return new

                //#region Declarations

                //case TigerParser.TYPE_DECL:
                //    return new
                //case TigerParser.VAR_DECL:
                //    return new
                //case TigerParser.ARRAY_TYPE_DECL:
                //    return new
                //case TigerParser.ALIAS_DECL:
                //    return new
                //case TigerParser.FUNCTION_DECL:
                //    return new

                //#endregion

                //#region Operators
                //case TigerParser.OR:
                //    return new
                //case TigerParser.AND:
                //    return new
                //case TigerParser.ASSIGN:
                //    return new
                //case TigerParser.GT:
                //    return new
                //case TigerParser.GT_EQUAL:
                //    return new
                //case TigerParser.LT:
                //    return new
                //case TigerParser.LT_EQUAL:
                //    return new
                //case TigerParser.EQUAL:
                //    return new
                //case TigerParser.NON_EQUAL:
                //    return new
                //case TigerParser.PLUS:
                //    return new
                //case TigerParser.MINUS:
                //    return new
                //case TigerParser.MUL:
                //    return new
                //case TigerParser.DIV:
                //    return new
                //case TigerParser.UMINUS:
                //    return new 
                //#endregion

                //#region Creation
                //case TigerParser.ARRAY_CREATION:
                //    return new
                //case TigerParser.RECORD_CREATION:
                //    return new




                //#endregion




                //default:
                //    throw new ArgumentOutOfRangeException("Unrecognized token.");

                #endregion
                    
                case TigerTokenTypes.ALIAS_DECL:
                    return new AliasDeclarationNode(payload);
                case TigerTokenTypes.AND:
                    return new AndNode(payload);
                case TigerTokenTypes.ARRAY_CREATION:
                    return new ArrayCreationNode(payload);
                case TigerTokenTypes.ARRAY_TYPE_DECL:
                    return new ArrayTypeDeclarationNode(payload);
                case TigerTokenTypes.ASSIGN:
                    return new AssignNode(payload);
                case TigerTokenTypes.AT:
                    return new IndexingNode(payload);
                case TigerTokenTypes.BREAK:
                    return new BreakNode(payload);
                case TigerTokenTypes.DIV:
                    return new DivisionNode(payload);
                case TigerTokenTypes.EQUAL:
                    return new EqualNode(payload);
                case TigerTokenTypes.EXPRESSION_LIST:
                    return new ExpressionListNode(payload);
                case TigerTokenTypes.EXPRESSION_SEQ:
                    return new ExpressionSequenceNode(payload);
                case TigerTokenTypes.FOR:
                    return new ForNode(payload);
                case TigerTokenTypes.FUNCTION_CALL:
                    return new FunctionCallNode(payload);
                case TigerTokenTypes.FUNCTION_DECL:
                    return new FunctionDeclarationNode(payload);
                case TigerTokenTypes.GT:
                    return new GreaterThanNode(payload);
                case TigerTokenTypes.GT_EQUAL:
                    return new GreaterThanOrEqualNode(payload);
                case TigerTokenTypes.ID:
                    return new IdNode(payload);
                case TigerTokenTypes.IF_THEN:
                    return new IfThenNode(payload);
                case TigerTokenTypes.IF_THEN_ELSE:
                    return new IfThenElseNode(payload);
                case TigerTokenTypes.INT:
                    return new IntNode(payload);
                case TigerTokenTypes.LET:
                    return new LetNode(payload);
                case TigerTokenTypes.LT:
                    return new LessThanNode(payload);
                case TigerTokenTypes.LT_EQUAL:
                    return new LessThanOrEqualNode(payload);
                case TigerTokenTypes.MINUS:
                    return new MinusNode(payload);
                case TigerTokenTypes.MUL:
                    return new MultiplyNode(payload);
                case TigerTokenTypes.NIL:
                    return new NilNode(payload);
                case TigerTokenTypes.NON_EQUAL:
                    return new NotEqualNode(payload);
                case TigerTokenTypes.OR:
                    return new OrNode(payload);
                case TigerTokenTypes.PLUS:
                    return new PlusNode(payload);
                case TigerTokenTypes.PROGRAM:
                    return new ProgramNode(payload);
                case TigerTokenTypes.RECORD_CREATION:
                    return new RecordCreationNode(payload);
                case TigerTokenTypes.RECORD_DECL:
                    return new RecordDeclarationNode(payload);
                case TigerTokenTypes.STRING:
                    return new StringNode(payload);
                case TigerTokenTypes.TYPE_DECL:
                    return new TypeDeclarationNode(payload);
                case TigerTokenTypes.TYPE_ID:
                    return new TypeIDNode(payload);
                case TigerTokenTypes.UMINUS:
                    return new UnaryMinusNode(payload);
                case TigerTokenTypes.VAR_DECL:
                    return new VariableDeclarationNode(payload);
                case TigerTokenTypes.WHILE:
                    return new WhileNode(payload);
                case TigerTokenTypes.LVALUE:
                    return new LValueNode(payload);
                default:
                    return base.Create(payload);
            }
        }
    }

    internal class DummyNode : CommonTree
    {
    }
}
