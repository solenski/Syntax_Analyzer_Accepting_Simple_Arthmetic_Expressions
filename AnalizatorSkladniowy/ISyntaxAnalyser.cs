using System.Collections.Generic;

namespace AnalizatorSkladniowy
{
    public interface ISyntaxAnalyser
    {
        Dictionary<string, IEnumerable<char>> terminalSymbolsGroups { get; }
        bool IsValidExpression(string s);
    }
}