using Antlr.Runtime;
using Antlr.Runtime.Tree;
using TigerCompiler.AST.Nodes.Declarations;
using TigerCompiler.AST.Nodes.Declarations.Blocks;
using TigerCompiler.AST.Nodes.Declarations.Types;
using TigerCompiler.AST.Nodes.Flow;
using TigerCompiler.AST.Nodes.Helpers;
using TigerCompiler.AST.Nodes.Instructions;
using TigerCompiler.AST.Nodes.LValue;
using TigerCompiler.AST.Nodes.Operations.Arithmetic;
using TigerCompiler.AST.Nodes.Operations.Comparison;
using TigerCompiler.AST.Nodes.Operations.Constants;
using TigerCompiler.AST.Nodes.Operations.Equality;
using TigerCompiler.AST.Nodes.Operations.Logical;

namespace TigerCompiler.AST
{
    class OurTreeAdaptor : CommonTreeAdaptor
    {
        public override object Create(IToken payload)
        {
            if (payload == null)
            {
                return base.Create(null);
            }

            switch ((TigerTokenTypes)payload.Type)
            {
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
                case TigerTokenTypes.DOT:
                    return new DotNode(payload);
                case TigerTokenTypes.FUNC_DECL_BLOCK:
                    return new FunctionDeclarationBlockNode(payload);
                case TigerTokenTypes.VAR_DECL_BLOCK:
                    return new VariableDeclarationBlockNode(payload);
                case TigerTokenTypes.TYPE_DECL_BLOCK:
                    return new TypeDeclarationBlockNode(payload);
                case TigerTokenTypes.TYPE_FIELDS:
                    return new TypeFieldsNode(payload);
                case TigerTokenTypes.DECL_LIST:
                    return new DeclarationListNode(payload);

                default:
                    return base.Create(payload);
            }
        }
    }
}
