grammar Tiger;

options
{
	language=Java;
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
    :  '"' ( ESC_SEQ | PRINTABLE_CHARACTER )* '"'
    ;

fragment
ESC_SEQ
    :   '\\' ('n'|'r'|'t'|'\"'|ASCII_ESC|WS?'\\')
    ;

fragment
PRINTABLE_CHARACTER	: ((' '.. '[')|(']'..'~')) ;// printable characters [32-126]

fragment
ASCII_ESC
    :   '1' (('2' ('0'..'7')) | (('0'|'1') ('0'..'9')))
    |   '0' ('0'..'9') ('0'..'9')
    ;
    
program	:	expr EOF;

expr: 	  	(STRING 
		| INT 
		| NIL 
		| MINUS expr 
		| (ID LBRACKET expr RBRACKET OF) => ID LBRACKET expr RBRACKET OF expr
		| ID 		( LPAR expr_list? RPAR
			| 	( LBRACE field_list? RBRACE)
			|	((DOT ID | LBRACKET expr RBRACKET)*) (ASSIGN expr)?)
		| LPAR expr_seq RPAR 
		| (IF expr THEN expr ELSE) => (IF expr THEN expr ELSE expr)
		| IF expr THEN expr
		| WHILE expr DO expr 
		| FOR ID ASSIGN expr TO expr DO expr 
		| BREAK 
		| LET declaration_list IN expr_seq? END) 
		;

type_id	:  ID
	;
	
type_declaration:
		TYPE type_id EQUAL type;

type:		type_id
	|	LBRACE type_fields RBRACE
	|	ARRAY OF type_id;
	
type_fields:
		type_field (COMMA type_field)*;
	
type_field:
		ID COLON type_id;

	
expr_seq: 	expr (SEMICOLON expr)*;

expr_list:
		(expr) (COMMA expr)*;
		
field_list:
		(ID EQUAL expr) (COMMA ID EQUAL expr)*;

lvalue:
	;
	
declaration_list:
		(declaration) (declaration)*;
declaration:
		type_declaration
	|	variable_declaration
	|	function_declaration;
	
variable_declaration:
		VAR ID ASSIGN expr
	|	VAR ID COLON type_id ASSIGN expr;

function_declaration:
		FUNCTION ID LPAR type_fields? RPAR EQUAL expr
	|	FUNCTION ID LPAR type_fields? RPAR COLON type_id EQUAL expr;

