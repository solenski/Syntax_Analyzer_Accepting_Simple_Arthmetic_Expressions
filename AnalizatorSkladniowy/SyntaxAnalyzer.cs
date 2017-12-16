using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizatorSkladniowy
{

    public class SyntaxAnalyzer : ISyntaxAnalyser
    {
        enum SubExpression
        {
            Invalid,
            Number,
            OperatorNumberPair,
            Minus,
        };

        enum NumberSubExpression
        {
            Invalid,
            Digit1,
            Digit2,
        }

        enum OperationNumberPairSubExpression
        {
            Invalid,
            Operation,
            Number
        }



        private Dictionary<string, IEnumerable<char>> _terminalSymbols = new Dictionary<string, IEnumerable<char>>
        {
            { "<oper>", new[] {'+', '-','*', '/' } },
            { "<digit>", new[] {'1', '2','3', '4', '5','6','7','8','9' } },
            { "<digit2>",  new[] {'0','1', '2','3', '4', '5','6','7','8','9' } },

        };
        HashSet<char> allTerminalSymbols => new HashSet<char>(_terminalSymbols.Values.SelectMany(x => x));
        public Dictionary<string, IEnumerable<char>> terminalSymbolsGroups => this._terminalSymbols;

        public bool IsValidExpression(string s)
        {
            try
            {
                List<SubExpression> subExpressions = new List<SubExpression>();
                if (s.ElementAtOrDefault(0) == '-')
                {
                    subExpressions.Add(SubExpression.Minus);
                    s = new string(s.Skip(1).ToArray());
                }

                s = IsNumber(s, subExpressions);



                while (s.Length > 0)
                {
                    s = IsOperationNumberPair(s, subExpressions);
                }

                bool condtion1 =
                        subExpressions.ElementAtOrDefault(0) == SubExpression.Number &&
                        subExpressions.Where((_, index) => index > 0 && index < subExpressions.Count)
                        .All(x => x == SubExpression.OperatorNumberPair);


                bool condtion2 =
                        subExpressions.ElementAtOrDefault(0) == SubExpression.Minus && subExpressions.ElementAtOrDefault(1) == SubExpression.Number &&
                        subExpressions.Where((_, index) => index > 1 && index < subExpressions.Count)
                        .All(x => x == SubExpression.OperatorNumberPair);


                return condtion1 || condtion2;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        private string IsOperationNumberPair(string s, List<SubExpression> subExpressions)
        {
            List<OperationNumberPairSubExpression> operationNumberPairExpressions = new List<OperationNumberPairSubExpression>();

            if (this._terminalSymbols["<oper>"].Contains(s.ElementAtOrDefault(0)))
            {
                operationNumberPairExpressions.Add(OperationNumberPairSubExpression.Operation);
                s = new string(s.Skip(1).ToArray());
            }
            else
            {
                if (!allTerminalSymbols.Contains(s.ElementAtOrDefault(0)))
                    throw new ArgumentException("Invalid character");

                return String.Empty;
            }

            s = IsNumber(s, operationNumberPairExpressions);

            if (operationNumberPairExpressions.ElementAtOrDefault(0) == OperationNumberPairSubExpression.Operation && operationNumberPairExpressions.ElementAtOrDefault(1) == OperationNumberPairSubExpression.Number && operationNumberPairExpressions.Count == 2)
            {
                subExpressions.Add(SubExpression.OperatorNumberPair);
            }

            return s;
        }

        private string IsNumber(string s, List<SubExpression> subExpressions)
        {
            List<NumberSubExpression> numberSubExpressions = new List<NumberSubExpression>();

            if (this._terminalSymbols["<digit>"].Contains(s.ElementAtOrDefault(0)))
            {
                numberSubExpressions.Add(NumberSubExpression.Digit1);
                s = new string(s.Skip(1).ToArray());
            }
            else
            {
                if (!allTerminalSymbols.Contains(s.ElementAtOrDefault(0)))
                    throw new ArgumentException("Invalid character");

                return String.Empty;
            }

            while (s.Length > 0)
            {
                if (this._terminalSymbols["<digit2>"].Contains(s.ElementAtOrDefault(0)))
                {
                    numberSubExpressions.Add(NumberSubExpression.Digit2);
                    s = new string(s.Skip(1).ToArray());
                }
                else
                {
                    if (!allTerminalSymbols.Contains(s.ElementAtOrDefault(0)))
                        throw new ArgumentException("Invalid character");

                    break;
                }
            }

            if (numberSubExpressions.ElementAtOrDefault(0) == NumberSubExpression.Digit1 || (numberSubExpressions.ElementAtOrDefault(0) == NumberSubExpression.Digit1 && numberSubExpressions.Where((_, index) => index > 0 && index < numberSubExpressions.Count).All(x => x == NumberSubExpression.Digit2)))
            {
                subExpressions.Add(SubExpression.Number);
            }

            return s;
        }

        private string IsNumber(string s, List<OperationNumberPairSubExpression> subExpressions)
        {
            List<NumberSubExpression> numberSubExpressions = new List<NumberSubExpression>();

            if (this._terminalSymbols["<digit>"].Contains(s.ElementAtOrDefault(0)))
            {
                numberSubExpressions.Add(NumberSubExpression.Digit1);
                s = new string(s.Skip(1).ToArray());
            }
            else
            {
                if (!allTerminalSymbols.Contains(s.ElementAtOrDefault(0)))
                    throw new ArgumentException("Invalid character");

                return String.Empty;
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (this._terminalSymbols["<digit2>"].Contains(s.ElementAtOrDefault(0)))
                {
                    numberSubExpressions.Add(NumberSubExpression.Digit2);
                    s = new string(s.Skip(1).ToArray());
                }
                else
                {
                    if (!allTerminalSymbols.Contains(s.ElementAtOrDefault(0)))
                        throw new ArgumentException("Invalid character");

                    break;
                }
            }

            if (numberSubExpressions.ElementAtOrDefault(0) == NumberSubExpression.Digit1 && numberSubExpressions.Where((_, index) => index > 0 && index < numberSubExpressions.Count).All(x => x == NumberSubExpression.Digit2))
            {
                subExpressions.Add(OperationNumberPairSubExpression.Number);
            }

            return s;
        }
    }
}

