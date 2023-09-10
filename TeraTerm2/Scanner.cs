using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace TeraTerm2;

using static TokenType;

public class Scanner
{
    private readonly string _source;
    private readonly List<Token> _tokens = new List<Token>();
    private int _start = 0;
    private int _current = 0;
    private int _line = 1;

    public Scanner(string source)
    {
        this._source = source;
    }

    public List<Token> ScanTokens()
    {
        while (!IsAtEnd())
        {
            _start = _current;
            scanToken();
        }

        _tokens.Add(new Token(EOF, "", null, _line));
        return _tokens;
    }

    private void scanToken()
    {
        char c = Advance();
        switch (c)
        {
            case '(':
                AddToken(LEFT_PAREN);
                break;
            case ')':
                AddToken(RIGHT_PAREN);
                break;
            case '{':
                AddToken(LEFT_BRACE);
                break;
            case '}':
                AddToken(RIGHT_BRACE);
                break;
            case '~':
                AddToken(TILDE);
                break;
            case '!':
                AddToken(Match('=') ? BANG_EQUAL : BANG);
                break;
            case '+':
                AddToken(PLUS);
                break;
            case '-':
                AddToken(MINUS);
                break;
            case '<':
                if (Match('<'))
                {
                    // <<
                    AddToken(LTLT);
                }
                else if (Match('='))
                {
                    // <=
                    AddToken(LTE);
                }
                else if (Match('>'))
                {
                    // <>
                    AddToken(LTGT);
                }
                else
                {
                    // <
                    AddToken(LT);
                }

                break;
            case '>':
                if (Match('>'))
                {
                    if (Match('>'))
                    {
                        // >>>
                        AddToken(GTGTGT);
                    }
                    else
                    {
                        // >>
                        AddToken(GTGT);
                    }
                }
                else if (Match('='))
                {
                    // >=
                    AddToken(GTE);
                }
                else
                {
                    // >
                    AddToken(GT);
                }

                break;
            case '*':
                AddToken(STAR);
                break;
            case '%':
                AddToken(PERCENT);
                break;
            case '&':
                AddToken(Match('&') ? ANDAND : AMPERSAND);
                break;
            case '^':
                AddToken(CARET);
                break;
            case '|':
                AddToken(Match('|') ? PIPEPIPE : PIPE);
                break;
            case '=':
                AddToken(Match('=') ? EQEQ : EQUAL);
                break;
            // Comments
            case ';':
                // We'll ignore comments for now.
                while (Peek() != '\n' && !IsAtEnd()) Advance();
                break;
            // Match C style comments
            case '/':
                if (Match('*'))
                {
                    while (!IsAtEnd())
                    {
                        // Increment line number.
                        if (Peek() == '\n') _line++;
                        // if we don't find continue until we do.
                        if (Peek() != '*' || PeekNext() != '/') continue;
                        // Consume * and /
                        Advance();
                        Advance();
                        break;
                    }
                }
                else
                {
                    AddToken(SLASH);
                }

                break;
            case ' ':
            case '\r':
            case '\t':
                // Ignore whitespace.
                break;
            case '\n':
                _line++;
                break;
            case '"':
                StringLiteral();
                break;
            case ':':
                TTLabel();
                break;
            default:
                // Handle integers.
                // TODO Sort out how TT treats negatives.
                if (IsDigit(c))
                {
                    Integer();
                    break;
                }
                
                // Handle variable identifiers.
                if (IsAlphaOrUnderscore(c))
                {
                    Identifier();
                    break;
                }
                else
                {
                    // TODO This code reports individual errors. We should probably collect them and report
                    // them all at once.
                    TeraTerm.Error(_line, "Unexpected character.");
                }

                break;
        }
    }

    /// <summary>
    /// Parse only single line string literals.
    /// </summary>
    private void StringLiteral()
    {
        while (Peek() != '"' && !IsAtEnd())
        {
            // Throw if the string does not terminate before a newline.
            // Or throw if we reach the end of the file.
            if (Peek() == '\n' || IsAtEnd())
            {
                TeraTerm.Error(_line, "Unterminated string.");
            }

            Advance();
        }

        // Consume closing ".
        Advance();

        // Trim the quotes, return the string.
        string value = _source.Substring(_start + 1, _current - 2);
        AddToken(STRING, value);
    }

    private void Identifier()
    {
        while (IsAlphaOrUnderscore(Peek()) || IsDigit(Peek()))
        {
            Advance();
        }

        var value = _source.Substring(_start, _current - _start);
        Keywords.TryGetValue(value, out var type);
        if (type == null)
        {
            type = VARIABLE;
        }
        AddToken(type, value);
    }

    private void TTLabel()
    {
        while (IsAlphaOrUnderscore(Peek()) || IsDigit(Peek()))
        {
            Advance();
        }

        var value = _source.Substring(_start, _current);
        AddToken(LABEL, value);
    }

    private void Integer()
    {
        while (IsDigit(Peek()))
        {
            Advance();
        }
        // We use Int32 because the built-in type is 32-bit.
        try
        {
            AddToken(INTEGER, Int32.Parse(_source.Substring(_start, _current - _start)));
        }
        catch (OverflowException)
        {
            TeraTerm.Error(_line, "Integer literal is too large.");
        }
    }

    private static bool IsAlphaOrUnderscore(char c)
    {
        return c is '_' or >= 'a' and <= 'z' or >= 'A' and <= 'Z';    
    }
    
    private static bool IsDigit(char c)
    {
        return c is >= '0' and <= '9';
    }

    private bool Match(char expected)
    {
        if (IsAtEnd()) return false;
        if (_source[_current] != expected) return false;
        
        _current++;
        return true;
    }
    
    private char Peek()
    {
        if (IsAtEnd()) return '\0';
        return _source[_current];
    }

    private char PeekNext()
    {
        if (IsAtEnd()) return '\0';
        return _source[_current + 1];
    }
    
    private bool IsAtEnd()
    {
        return _current >= _source.Length;
    }
    
    private char Advance()
    {
        _current++;
        return _source[_current - 1];
    }

    private void AddToken(TokenType type, object? literal = null)
    {
        string text = _source.Substring(_start, _current - _start);
        _tokens.Add(new Token(type, text, literal, _line));
    }

    private static readonly StringComparer Comparer = StringComparer.OrdinalIgnoreCase;
    private static readonly Dictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>(Comparer)
    {
        // TODO Commands
        
        // Operators
        {"and", AND},
        {"not", NOT},
        {"or", OR},
        {"xor", XOR},
        
        // TODO System variables
    };
}