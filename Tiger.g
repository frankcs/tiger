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
	DOT = '.';
	
	//Imaginary nodes
	PROGRAM;
	TYPE_ID;
	
	ARRAY_CREATION;
	RECORD_CREATION;
	
	FUNCTION_CALL;
	
	LVALUE;

	AT;
	EXPRESSION_SEQ;
	EXPRESSION_LIST;
	
	IF_THEN_ELSE;
	IF_THEN;
	
	ARRAY_TYPE_DECL;
	RECORD_DECL;
	ALIAS_DECL;
	
	FUNCTION_DECL;
	VAR_DECL;
	
	TYPE_DECL_BLOCK;
	FUNC_DECL_BLOCK;
	VAR_DECL_BLOCK;
	
	DECL_LIST;
	TYPE_FIELDS;
	
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
        )+ {$channel = Hidden;}
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

expr_seq: 	expr (';'! expr)* ;

expr_list:	expr (','! expr)*;
		
field_list:
		(ID EQUAL! expr) (','! ID EQUAL! expr)*;
	
lvalue	:	ID array_or_member_access? -> ^(LVALUE ID array_or_member_access?)
	;
	
array_or_member_access
	:	(member_access | array_access) array_or_member_access?
	;
	
member_access
	:	DOT^ ID
	;
	
array_access
	:	'[' expr ']' -> ^(AT expr)
	;
	
declaration_list:
		(declaration)+ -> ^(DECL_LIST declaration+);
		
// ALEX: Comprobar que esto agrupa correctamente los bloques de declaraciones.
declaration:
		type_declaration+ -> ^(TYPE_DECL_BLOCK type_declaration+)
	|	variable_declaration+ -> ^(VAR_DECL_BLOCK variable_declaration+)
	|	function_declaration+ -> ^(FUNC_DECL_BLOCK function_declaration+);

//ALEX: Desfactorizé esto para que reescribir las reglas fuera más fácil.
type_declaration:
			'type' type_id EQUAL type_id -> ^(ALIAS_DECL type_id type_id)
		|	'type' type_id EQUAL '{' type_fields '}' -> ^(RECORD_DECL type_id type_fields)
		|	'type' type_id EQUAL 'array' 'of' type_id -> ^(ARRAY_TYPE_DECL type_id type_id)
		;

variable_declaration:
		'var' ID (':' type_id)? ASSIGN expr -> ^(VAR_DECL ID type_id? expr);

function_declaration:
		'function' ID '(' type_fields? ')' (':' type_id)? EQUAL expr -> ^(FUNCTION_DECL ID ^(TYPE_FIELDS type_fields?) type_id? expr);

type_id	:  ID -> ^(TYPE_ID ID);
	
// ALEX: Tuve que cambiar esto para poder recorrer bien la declaracion de una funcion.
type_fields:	type_field (','! type_field)*;
	
type_field:	ID ':' type_id -> ^(ID type_id);
