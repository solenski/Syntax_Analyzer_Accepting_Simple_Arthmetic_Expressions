using System.Collections.Generic;

namespace AnalizatorSkladniowy
{
    public interface ISyntaxAnalyser
    {
        Dictionary<string, IEnumerable<char>> terminalSymbols { get; }
        bool IsValidExpression(string s);
    }
}