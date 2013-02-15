using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerCompiler.AST
{
    /// <summary>
    /// Quitar ASCII_ESC, ESC_SEQ, WS, COMMENT y PRINTABLE_CHARACTER
    /// TODO: Fix the enum values.
    /// </summary>
    internal enum TigerTokenTypes
    {
        ALIAS_DECL = 4
        ,
        AND = 5
        ,
        ARRAY_CREATION = 6
        ,
        ARRAY_TYPE_DECL = 7
        ,
        ASCII_ESC = 8
        ,
        ASSIGN = 9
        ,
        AT = 10
        ,
        BREAK = 11
        ,
        COMMENT = 12
        ,
        DECL_LIST = 13
        ,
        DIV = 14
        ,
        DOT = 15
        ,
        EQUAL = 16
        ,
        ESC_SEQ = 17
        ,
        EXPRESSION_LIST = 18
        ,
        EXPRESSION_SEQ = 19
        ,
        FOR = 20
        ,
        FUNCTION_CALL = 21
        ,
        FUNCTION_DECL = 22
        ,
        FUNC_DECL_BLOCK = 23
        ,
        GT = 24
        ,
        GT_EQUAL = 25
        ,
        ID = 26
        ,
        IF_THEN = 27
        ,
        IF_THEN_ELSE = 28
        ,
        INT = 29
        ,
        LET = 30
        ,
        LT = 31
        ,
        LT_EQUAL = 32
        ,
        LVALUE = 33
        ,
        MINUS = 34
        ,
        MUL = 35
        ,
        NIL = 36
        ,
        NON_EQUAL = 37
        ,
        OR = 38
        ,
        PLUS = 39
        ,
        PRINTABLE_CHARACTER = 40
        ,
        PROGRAM = 41
        ,
        RECORD_CREATION = 42
        ,
        RECORD_DECL = 43
        ,
        STRING = 44
        ,
        TYPE_DECL_BLOCK = 45
        ,
        TYPE_FIELDS = 46
        ,
        TYPE_ID = 47
        ,
        UMINUS = 48
        ,
        VAR_DECL = 49
        ,
        VAR_DECL_BLOCK = 50
        ,
        WHILE = 51
        ,
        WS = 52
    }
}
