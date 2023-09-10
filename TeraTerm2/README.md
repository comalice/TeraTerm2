# Lexical Grammar

# Syntactic Grammar

A TeraTerm macro is a sequence of lines. Each line is either a command, an assignment, a comment, or a label. In a
special case, a line can contain c-style multiline comments. We ignore comments in the lexical parser.

```ebnf
macro (encoding:
    Usually (ANSI CodePage)
    UTF-8 (with BOM)
    UTF-8 (without BOM)
    UTF-16 (with BE BOM)
    UTF-16 (with LE BOM)
) -> line* EOF

line -> command | assignment | label

command -> commandName (argument (argument)*)?

;; I am unsure if this should just be an 'identifier' node or
;; if it should be individual nodes for each reserved word. My
;; gut tells me we should just have an 'identifier' node and
;; then perform lookups for the identifier we want to use.
commandName -> identifier

argument -> constant | identifier

assignment -> identifier '=' constant | identifier | expression

label -> ':' labelName 

constant -> string | integer

string -> lexer picks these up ;; TODO: add support for character literals
integer -> lexer picks these up ;; TODO: add support for hex

expression -> literal | unary | binary | grouping

literal -> INTEGER | STRING
grouping -> '(' expression ')'
unary -> ('not' | '~' | '!' | '+' | '-') expression
binary -> expression operator expression
operator -> '+' | '-' | '*' | '/' | '%' | '==' | '!=' | '<' | '<=' | '>' | '>=' | 'and' | 'or' | 'xor' | '<<' | '>>' | '&' | '|' | '^'
```
