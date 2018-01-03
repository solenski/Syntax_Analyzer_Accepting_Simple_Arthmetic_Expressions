using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    public class SimpleEquationSyntaxAnalyser
    {
        private static readonly char[] digits1 = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static readonly char[] digits2 = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        private static readonly char[] operators = new[] { '+', '-', '/', '*' };

        private Func<char, bool> digit1predicate = (char symbol) => digits1.Contains(symbol);
        private Func<char, bool> digit2predicate = (char symbol) => digits2.Contains(symbol);
        private Func<char, bool> operatorpredicate = (char symbol) => operators.Contains(symbol);

        private int currentIndex = 0;

        private bool expression(string input)
        {
            while (currentIndex < input.Length)
            {
                if (input.ElementAtOrDefault(currentIndex) == '-')
                {
                    this.currentIndex++;
                    if (this.number(input))
                    {
                        while (!this.EmptySymbol(input))
                        {
                            if (this.operatorpredicate(input.ElementAtOrDefault(currentIndex)))
                            {
                                this.currentIndex++;
                                if (this.number(input))
                                {
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (input.ElementAtOrDefault(currentIndex) == '0')
                {
                    this.currentIndex++;

                    while (!this.EmptySymbol(input))
                    {
                        if (this.operatorpredicate(input.ElementAtOrDefault(currentIndex)))
                        {
                            this.currentIndex++;

                            if (this.number(input))
                            {
                                continue;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    if (this.number(input))
                    {
                        while (!this.EmptySymbol(input))
                        {
                            if (this.operatorpredicate(input.ElementAtOrDefault(currentIndex)))
                            {
                                this.currentIndex++;
                                if (this.number(input))
                                {
                                    continue;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }


            }
            return true;

        }

        private bool number(string input)
        {

            if (this.digit1predicate(input.ElementAtOrDefault(currentIndex)))
            {
                currentIndex++;
                if (this.numberPrime(input))
                {
                    return true;
                }

            }
            else if (input.ElementAtOrDefault(currentIndex) == '0')
            {
                currentIndex++;
                return true;
            }
            return false;
        }

        private bool numberPrime(string input)
        {
            while (currentIndex < input.Length)
            {
                if (this.digit2predicate(input.ElementAtOrDefault(currentIndex)))
                {
                    currentIndex++;
                }
                else
                {
                    return true;
                }
            }
            return this.EmptySymbol(input) ? true : throw new InvalidOperationException("Something went wrong shoudln't hit this");
        }

        private bool EmptySymbol(string input)
        {
            if (currentIndex >= input.Length)
            {
                return true;
            }
            return false;
        }

        public char[] TerminalSymbols => digits1.Concat(digits2).Concat(operators).ToArray();

        public bool Validate(string input)
        {
            var trimmedInput = new string(input.Where(x => !string.IsNullOrWhiteSpace(x.ToString())).ToArray());
            if (!trimmedInput.All(x => this.TerminalSymbols.Contains(x)))
            {
                return false;
            }

            var result = this.expression(trimmedInput);
            this.currentIndex = 0;
            return result;
        }
    }
}
