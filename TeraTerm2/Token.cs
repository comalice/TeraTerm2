namespace TeraTerm2;

public class Token
{
    private readonly int? _charNum;
    private readonly int _line;
    private readonly object? _literal;
    public readonly string Lexeme;
    public readonly TokenType Type;

    /// <summary>
    ///     A parser token object with source location references.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="lexeme"></param>
    /// <param name="literal"></param>
    /// <param name="line"></param>
    /// <param name="charNum"></param>
    public Token(TokenType type, string lexeme, object? literal, int line, int? charNum = null)
    {
        Type = type;
        Lexeme = lexeme;
        _literal = literal;
        _line = line;
        _charNum = charNum;
    }

    public override string ToString()
    {
        return Type + " " + Lexeme + " " + _literal;
    }
}