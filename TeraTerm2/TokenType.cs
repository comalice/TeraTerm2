namespace TeraTerm2;

public enum TokenType
{
    // Single character tokens
    LEFT_PAREN,
    RIGHT_PAREN,
    LEFT_BRACE,
    RIGHT_BRACE,

    // 1. Math expression tokens. Highest precedence.
    // Bitwise negation, logical negation, unary plus, unary minus
    // 'not', '~', '!', '+', '-'
    NOT,
    TILDE,
    BANG,
    PLUS,
    MINUS,

    // 2. Multiplication, division, and remainder (modulo)
    // '*', '/', '%'
    STAR,
    SLASH,
    PERCENT,

    // 3. Addition and subtraction, '+' and '-'
    // These appear twice in the hierarchy.
    // PLUS, MINUS,

    // 4. Arithmetic shift left and right, logical shift right
    // '<<', '>>', '>>>'
    LTLT,
    GTGT,
    GTGTGT,

    // 5. Bitwise conjunction, 'and', '&'
    AND,
    AMPERSAND,

    // 6. Bitwise exclusive disjunction, 'xor', '^'
    XOR,
    CARET,

    // 7. Bitwise disjunction, 'or', '|'
    OR,
    PIPE,

    // 8. Less than, less than or equal to, greater than, greater than or equal to
    LT,
    GT,
    LTE,
    GTE,

    // 9. Equality and inequality
    EQUAL,
    EQEQ,
    LTGT,
    BANG_EQUAL,

    // 10. Logical conjunction
    ANDAND,

    // 11. Logical disjunction
    PIPEPIPE,

    // Literals
    INTEGER,
    STRING,

    // Identifiers, :label and variables/commands
    LABEL,
    IDENTIFIER,

    // Keywords
    FOR,
    NEXT,
    IF,
    THEN,
    ELSE,
    ELSEIF,
    ENDIF,
    WHILE,
    ENDWHILE,
    UNTIL,
    ENDUNTIL,
    DO,
    LOOP,
    BREAK,
    CONTINUE,
    GOTO,
    RETURN,
    END,
    CALL,
    EXIT,
    // TODO Command List, we might not do this here. 
    // We might do this with an IDENTIFIER token and leave it to the parser to decide what needs to happen?
    // System Variables
    // GROUPMATCHSTR1, GROUPMATCHSTR2, GROUPMATCHSTR3, GROUPMATCHSTR4, GROUPMATCHSTR5, GROUPMATCHSTR6, GROUPMATCHSTR7, GROUPMATCHSTR8, GROUPMATCHSTR9,
    // PARAM1, PARAM2, PARAM3, PARAM4, PARAM5, PARAM6, PARAM7, PARAM8, PARAM9, PARAMS, PARAMCNT,
    // INPUTSTR, MATCHSTR, RESULT, TIMEOUT, MTIMEOUT,

    EOF
}