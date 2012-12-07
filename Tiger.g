grammar Tiger;

options
{
	language=Java;
	output=AST;
}

tokens
{
	//Reserved words
	ARRAY = 'array';
	BREAK = 'break';
	DO = 'do';
	ELSE = 'else';
	END = 'end';
	FOR = 'for';
	FUNCTION = 'function';
	IF = 'if';
	IN = 'in';
	LET = 'let';
	NIL = 'nil';
	OF = 'of';
	THEN = 'then';
	TO = 'to';
	TYPE = 'type';
	VAR = 'var';
	WHILE = 'while';
	
	//punctuation marks and symbols	
	COMMA = ',';
	COLON = ':';
	SEMICOLON = ';';
	LPAR = '(';
	RPAR = ')';
	LBRACKET = '[';
	RBRACKET = ']';
	LBRACE = '{';
	RBRACE = '}';	
	DOT = '.';
	PLUS = '+';
	MINUS = '-';
	MUL = '*';
	DIV = '/';
	EQUAL = '=';
	NON_EQUAL = '<>';
	LT = '<';
	LT_EQUAL = '<=';
	GT = '>'; 
	GT_EQUAL = '>=';	
	AND = '&';
	OR = '|';
	ASSIGN = ':=';		
	
	//Imaginary nodes
	PROGRAM;
	ARRAY_CREATION;
	FUNCTION_CALL;
	RECORD_CREATION;
	AT;
	EXPRESSION_SEQ;
	EXPRESSION_LIST;
	IF_THEN_ELSE;
	IF_THEN;
	TYPE_ID;
	TYPE_DECL;
	FUNCTION_DECL;
	VAR_DECL;
	
}

@header
{
//using System;
//using System.IO;
}

@members{
/*
	static void Main(string[] args)
	{
	    if (args.Length > 0 && File.Exists(args[0]))
	    {
	        ICharStream characters = new ANTLRFileStream(args[0]);
	        TigerLexer lexer = new TigerLexer(characters);
	        ITokenStream tokens = new CommonTokenStream(lexer);
	        TigerParser parser = new TigerParser(tokens);
	        parser.TraceDestination = Console.Out;
	        parser.program();
	        if (parser.NumberOfSyntaxErrors > 0)
	            Console.WriteLine("Entrada incorrecta. {0} error(es) cometido(s)", parser.NumberOfSyntaxErrors);
	    }
	    else
	        Console.WriteLine("La ruta no es valida o el archivo no existe");
	}
	*/
}

ID  :	('a'..'z'|'A'..'Z') ('a'..'z'|'A'..'Z'|'0'..'9'|'_')*
    ;


// TODO: Comprobar validez de 010, por ejemplo.
//INT :	 '0' | '1'..'9' ('0'..'9')*
INT :	'0'..'9'+
    ;

WS  :   ( ' '
        | '\t'
        | '\n'
        | '\r'
        )+ {$channel=HIDDEN;}
    ;

// Probar los comentarios anidados.
COMMENT
    :   '/*' ( options {greedy=false;} :COMMENT|. )* '*/' {$channel = HIDDEN;}
    ;
    
STRING
    : '"'( ESC_SEQ | PRINTABLE_CHARACTER )* '"'
    ;

fragment
ESC_SEQ
    :   '\\' ('n'|'r'|'t'|'\"'|ASCII_ESC|WS?'\\')
    ;

fragment
PRINTABLE_CHARACTER	: ((' '..'!')|('#'.. '[')|(']'..'~')) ;// printable characters [32-126]

fragment
ASCII_ESC
    :   '1' (('2' ('0'..'7')) | (('0'|'1') ('0'..'9')))
    |   '0' ('0'..'9') ('0'..'9')
    ;
    
program	:	expr EOF -> ^(PROGRAM expr);

expr	:	or_expr;

or_expr	:	and_exp (OR^ and_exp)*
	;

and_exp :	comp_expr (AND^ comp_expr)*
	;

comp_expr
	:	arith_expr((EQUAL^ | NON_EQUAL^ | LT^ | GT^ | LT_EQUAL^ | GT_EQUAL^) arith_expr)*
	;

arith_expr
	:	term((PLUS^|MINUS^) term)*
	;

term	:	texpr((MUL^|DIV^) texpr)*
	;

texpr: 	  	STRING
		| INT
		| NIL
		| (ID LBRACKET expr RBRACKET OF) => type_id LBRACKET e1=expr RBRACKET OF e2=expr -> ^(ARRAY_CREATION type_id $e1 $e2) 
		| (ID LPAR)=>(ID LPAR expr_list? RPAR) -> ^(FUNCTION_CALL ID ^(EXPRESSION_LIST expr_list?))
		| (ID LBRACE) => (ID LBRACE field_list? RBRACE) -> ^(RECORD_CREATION ID field_list?)
		| (lvalue ASSIGN) => (lvalue ASSIGN expr) -> ^(ASSIGN lvalue expr)
		| lvalue
		| LPAR expr_seq? RPAR -> ^(EXPRESSION_SEQ expr_seq?)
		| (IF expr THEN expr ELSE) => (IF ifx=expr THEN thenx=expr ELSE elsex=expr) -> ^(IF_THEN_ELSE $ifx $thenx $elsex)
		| IF ifx=expr THEN elsex=expr -> ^(IF_THEN $ifx $elsex)
		| WHILE condition=expr DO something=expr -> ^(WHILE $condition $something)
		| FOR var=ID ASSIGN init=expr TO limit=expr DO something=expr -> ^(FOR $var $init $limit $something)
		| BREAK
		| LET declaration_list IN expr_seq? END -> ^(LET declaration_list expr_seq?)
		| MINUS texpr -> ^(MINUS texpr)
		;


type_id	:  ID -> ^(TYPE_ID ID)
	;
	
type_declaration:
		TYPE type_id EQUAL type -> ^(TYPE_DECL type_id type);

type:		type_id
	|	LBRACE type_fields RBRACE
	|	ARRAY OF type_id -> ^(ARRAY type_id);
	
type_fields:
		type_field (COMMA type_field)*;
	
type_field:
		ID COLON type_id;

	
expr_seq: 	expr (SEMICOLON! expr)*;

expr_list:	expr (COMMA! expr)*;
		
field_list:
		(ID EQUAL! expr) (COMMA! ID EQUAL! expr)*;
	
lvalue	:	ID array_or_member_access? -> ^(ID array_or_member_access?)
	;
	
array_or_member_access
	:	(member_access | array_access) array_or_member_access?
	;
	
member_access
	:	DOT ID -> ^(DOT ID)
	;
	
array_access
	:	'[' expr ']' -> ^(AT expr)
	;
	
declaration_list:
		(declaration)+;
declaration:
		type_declaration
	|	variable_declaration
	|	function_declaration;
	
variable_declaration:
		VAR ID (COLON type_id)? ASSIGN expr -> ^(VAR_DECL ID type_id? expr);

function_declaration:
		FUNCTION ID LPAR type_fields? RPAR (COLON type_id)? EQUAL expr -> ^(FUNCTION_DECL ID type_fields? type_id? expr);

