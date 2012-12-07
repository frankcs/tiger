grammar Tiger;

options
{
	language=CSharp3;
	output=AST;
}

tokens
{
	//Reserved words
	BREAK = 'break';
	FOR = 'for';
	LET = 'let';
	NIL = 'nil';
	WHILE = 'while';
	
	// Operators
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
	TYPE_ID;
	
	ARRAY_CREATION;
	RECORD_CREATION;
	
	FUNCTION_CALL;

	AT;
	EXPRESSION_SEQ;
	EXPRESSION_LIST;
	
	IF_THEN_ELSE;
	IF_THEN;
		
	TYPE_DECL;
		ARRAY_TYPE_DECL;
		RECORD_DECL;
		ALIAS_DECL;
	FUNCTION_DECL;
	VAR_DECL;
	
	UMINUS;
	
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
        )+ {$channel=Hidden;}
    ;

// Probar los comentarios anidados.
COMMENT
    :   '/*' ( options {greedy=false;} :COMMENT|. )* '*/' {$channel = Hidden;}
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
		| (ID '[' expr ']' 'of') => type_id '[' e1=expr ']' 'of' e2=expr -> ^(ARRAY_CREATION type_id $e1 $e2) 
		| (ID '(')=>(ID '(' expr_list? ')') -> ^(FUNCTION_CALL ID ^(EXPRESSION_LIST expr_list?))
		| (ID '{') => (ID '{' field_list? '}') -> ^(RECORD_CREATION ID field_list?)
		| (lvalue ASSIGN) => (lvalue ASSIGN expr) -> ^(ASSIGN lvalue expr)
		| lvalue
		| '(' expr_seq? ')' -> ^(EXPRESSION_SEQ expr_seq?)
		| ('if' expr 'then' expr 'else') => ('if' ifx=expr 'then' thenx=expr 'else' elsex=expr) -> ^(IF_THEN_ELSE $ifx $thenx $elsex)
		| 'if' ifx=expr 'then' elsex=expr -> ^(IF_THEN $ifx $elsex)
		| WHILE condition=expr 'do' something=expr -> ^(WHILE $condition $something)
		| FOR var=ID ASSIGN init=expr 'to' limit=expr 'do' something=expr -> ^(FOR $var $init $limit $something)
		| BREAK
		| LET declaration_list 'in' expr_seq? 'end' -> ^(LET declaration_list expr_seq?)
		| MINUS texpr -> ^(UMINUS texpr)
		;


type_id	:  ID -> ^(TYPE_ID ID)
	;
	
type_declaration:
		'type' type_id EQUAL type -> ^(TYPE_DECL type_id type);

type:		type_id -> ALIAS_DECL
	|	'{' type_fields '}' -> ^(RECORD_DECL type_fields)
	|	'array' 'of' type_id -> ^(ARRAY_TYPE_DECL type_id);
	
type_fields:
		type_field (',' type_field)*;
	
type_field:
		ID ':' type_id;

	
expr_seq: 	expr (';'! expr)*;

expr_list:	expr (','! expr)*;
		
field_list:
		(ID EQUAL! expr) (','! ID EQUAL! expr)*;
	
lvalue	:	ID array_or_member_access? -> ^(ID array_or_member_access?)
	;
	
array_or_member_access
	:	(member_access | array_access) array_or_member_access?
	;
	
member_access
	:	'.' ID -> ^('.' ID)
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
		'var' ID (':' type_id)? ASSIGN expr -> ^(VAR_DECL ID type_id? expr);

function_declaration:
		'function' ID '(' type_fields? ')' (':' type_id)? EQUAL expr -> ^(FUNCTION_DECL ID type_fields? type_id? expr);

