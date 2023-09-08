// Read, Eval, Print Loop (REPL) for the TT2 language

using System.Text;

namespace TeraTerm2;

/// <summary>
/// If this looks like a ripoff from Crafting Interpreters, it is.
///
/// One cannot argue with elegance.
/// </summary>
public static class TeraTerm
{
    public static void Main(string[] args)
    {
        if (args.Length > 1)
        {
            Console.WriteLine("Usage: tt [script]");
            Environment.Exit(64);
        }
        else if (args.Length == 1)
        {
            RunFile(args[0]);
        }
        else
        {
            RunPrompt();
        }
    }
    
    private static bool _hadError = false;

    private static void RunFile(string path)
    {
        var bytes = File.ReadAllBytes(path);
        Run(Encoding.UTF8.GetString(bytes));
        if (_hadError) Environment.Exit(65);
    }

    private static void RunPrompt()
    {
        for (;;)
        {
            Console.Write("> ");
            var line = Console.ReadLine();
            if (line == null) break;
            Run(line);
            _hadError = false;
        }
    }

    private static void Run(string source)
    {
        var scanner = new Scanner(source);
        var tokens = scanner.ScanTokens();

        // For now, just print the tokens.
        foreach (var token in tokens)
        {
            Console.WriteLine(token);
        }
    }

    public static void Error(int line, string message)
    {
        report(line, "", message);
    }

    private static void report(int line, string where, string message)
    {
        Console.Error.WriteLine("[line " + line + "] Error" + where + ": " + message);
        _hadError = true;
    }
}