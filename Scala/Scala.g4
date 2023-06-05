grammar Scala;

compilationUnit: classDefinition* EOF;

classDefinition:
    'class' IDENTIFIER '{' classBody '}'
;

classBody:
    (methodDefinition | fieldDefinition)*
;

fieldDefinition:
    IDENTIFIER ':' dataType
;

methodDefinition:
    'def' IDENTIFIER '(' parameters? ')' ':' dataType '{' block '}'
  | 'def' IDENTIFIER ':' dataType '{' block '}'
;

parameters:
    parameter (',' parameter)*
;

parameter:
    IDENTIFIER ':' dataType
;

dataType:
    'Int' | 'Double' | 'Boolean' | 'String' | IDENTIFIER
;

block:
    statement* expression
;

statement:
    'val' IDENTIFIER '=' expression
  | printMethodCall
;

printMethodCall:
    'print' '(' expression ')'
;

expression:
    addExpr
;

addExpr:
    multExpr (('+' | '-') multExpr)*
;

multExpr:
    unaryExpr (('*' | '/') unaryExpr)*
;

unaryExpr:
    '-' unaryExpr
    | primary
;

primary:
    IDENTIFIER
    | INT_LITERAL
    | DOUBLE_LITERAL
    | BOOLEAN_LITERAL
    | STRING_LITERAL
    | '(' expression ')'
;

IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;
INT_LITERAL: [0-9]+;
DOUBLE_LITERAL: [0-9]+ '.' [0-9]+;
BOOLEAN_LITERAL: 'true' | 'false';
STRING_LITERAL: '"' (~["\r\n])* '"';
LINE_COMMENT: '//' ~[\r\n]* -> skip;
BLOCK_COMMENT: '/*' .*? '*/' -> skip;
WHITESPACE: [ \t\r\n]+ -> skip;