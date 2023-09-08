# Lexical Grammar

# Syntactic Grammar

```ebnf
macro (encoding:
    Usually (ANSI CodePage)
    UTF-8 (with BOM)
    UTF-8 (without BOM)
    UTF-16 (with BE BOM)
    UTF-16 (with LE BOM)
) -> line* EOF
```

We ignore comments in the lexical parser.

```ebnf
line -> command | assignment | label

command -> commandName (argument (argument)*)?

commandName -> identifier
```
