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
	OPEN_PARENTHESIS = '(';
	CLOSE_PARENTHESIS = ')';
	OPEN_BRACKET = '[';
	CLOSE_BRACKET = ']';
	OPEN_BRACE = '{';
	CLOSE_BRACE = '}';	
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
    :   '/*' ( options {greedy=false;} :COMMENT|. )* '*/'
    ;
    
STRING
    :  '"' ( ESC_SEQ | PRINTABLE_CHARACTER )* '"'
    ;

fragment
ESC_SEQ
    :   '\\' ('n'|'r'|'t'|'\"'|ASCII_ESC|WS?'\\')
    ;

// '\' es el 92. Importa?
fragment
PRINTABLE_CHARACTER	: ((' '.. '[')|(']'..'~')) ;// printable characters [32-126]

fragment
ASCII_ESC
    :   '1' (('2' ('0'..'7')) | (('0'|'1') ('0'..'9')))
    |   '0' ('0'..'9') ('0'..'9')
    ;

program	:	ID EOF;
