using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TigerCompiler.AST
{
    /// <summary>
    /// Quitar ASCII_ESC, ESC_SEQ, WS, COMMENT y PRINTABLE_CHARACTER
    /// </summary>
    enum TigerTokenTypes
    {
     ALIAS_DECL=4,
	 AND=5,
	 ARRAY_CREATION=6,
	 ARRAY_TYPE_DECL=7,
	 ASSIGN=9,
	 AT=10,
	 BREAK=11,
	 DIV=13,
	 DOT=14,
	 EQUAL=15,
	 EXPRESSION_LIST=17,
	 EXPRESSION_SEQ=18,
	 FOR=19,
	 FUNCTION_CALL=20,
	 FUNCTION_DECL=21,
	 GT=22,
	 GT_EQUAL=23,
	 ID=24,
	 IF_THEN=25,
	 IF_THEN_ELSE=26,
	 INT=27,
	 LET=28,
	 LT=29,
	 LT_EQUAL=30,
	 LVALUE=31,
	 MINUS=32,
	 MUL=33,
	 NIL=34,
	 NON_EQUAL=35,
	 OR=36,
	 PLUS=37,
     PROGRAM = 39,
	 RECORD_CREATION=40,
	 RECORD_DECL=41,
	 STRING=42,
	 TYPE_DECL=43,
	 TYPE_ID=44,
	 UMINUS=45,
	 VAR_DECL=46,
	 WHILE=47,
    }
}
