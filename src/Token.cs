namespace TeraTerm2;

public class Token
{
    public readonly TokenType Type;
    private readonly string _lexeme;
    private readonly object? _literal;
    private readonly int _line;
    private readonly int? _charNum;
    
    /// <summary>
    /// A parser token object with source location references.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="lexeme"></param>
    /// <param name="literal"></param>
    /// <param name="line"></param>
    /// <param name="charNum"></param>
    public Token(TokenType type, string lexeme, object? literal, int line, int? charNum = null)
    {
        this.Type = type;
        this._lexeme = lexeme;
        this._literal = literal;
        this._line = line;
        this._charNum = charNum;
    }

    public override string ToString()
    {
        return Type + " " + _lexeme + " " + _literal;
    }
}