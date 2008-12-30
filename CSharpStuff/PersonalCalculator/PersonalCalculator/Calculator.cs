using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
    /// <summary>
    /// Calculator is the front of this program. It serves as a dispatch service for the actual calls that are made to it.
    /// </summary>
    internal class Calculator : IDisposable
    {
        #region Fields

        private bool _isDisposed;

        #endregion

        #region Properties

        public bool IsDisposed
        {
            get { return _isDisposed; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// For interactive use. This constructor is called internally by Run.
        /// </summary>
        private Calculator()
        {
            _isDisposed = false;
        }

        /// <summary>
        /// For explicit use from the command line. This constructor relies on a formula being supplied on the command line.
        /// </summary>
        /// <param name="equation"></param>
        public Calculator(string userInput)
        {
            _isDisposed = false;

            Equation equation = Equation.Parse(userInput);
            string result = Calculate(equation);
            Console.WriteLine("{0}", result);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This gets the static instance of Calculator for use as an interactive shell.
        /// </summary>
        public static void Run()
        {
            try
            {
                using (Calculator calculator = new Calculator())
                {
                    while (true)
                    {
                        Console.Write("\n> ");
                        string userInput = Console.ReadLine();
                        userInput = userInput.Trim();
                        userInput = userInput.Replace(" ", "");

                        switch (userInput)
                        {
                            default:
                                Equation equation = Equation.Parse(userInput);
                                string result = Calculate(equation);
                                Console.WriteLine("{0}", result);
                                break;
                            case "h":
                                DisplayHelp();
                                break;
                            case "help":
                                DisplayHelp();
                                break;
                            case "?":
                                DisplayHelp();
                                break;
                            case "q":
                                throw new UserQuitException();
                            case "quit":
                                throw new UserQuitException();
                        }
                    }
                }
            }
            catch (UserQuitException)
            {
                Console.WriteLine("Thanks for using PC.NET.");
            }
        }

        /// <summary>
        /// Displays the help information for the current context.
        /// </summary>
        public static void DisplayHelp()
        {
        }

        #endregion

        #region Private Methods

        private static string Calculate(Equation equation)
        {
            string calculationResult = String.Empty;

            Stack<Token> unusedTokens = new Stack<Token>();

            while (equation.Length > 0)
            {
                switch (equation.Peek().Type)
                {
                    case TokenType.Number:
                        unusedTokens.Push(equation.Pop());
                        break;
                    case TokenType.Negation:
                        equation.Pop();
                        try
                        {
                            string right = unusedTokens.Pop().Value;
                            string result = Evaluator.Negate(right);

                            unusedTokens.Push(new NumberToken(result));
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine("Could not evaluate subtraction because not enough operands were present. This may be an internal error. {0}", ex.Message);
                        }
                        break;
                    case TokenType.Subtraction:
                        equation.Pop();
                        try
                        {
                            string subtrahend = unusedTokens.Pop().Value;
                            string minuend = unusedTokens.Pop().Value;
                            string result = Evaluator.Subtract(minuend, subtrahend);

                            unusedTokens.Push(new NumberToken(result));
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine("Could not evaluate subtraction because not enough operands were present. This may be an internal error. {0}", ex.Message);
                        }
                        break;
                    case TokenType.Addition:
                        equation.Pop();
                        try
                        {
                            string leftAddend = unusedTokens.Pop().Value;
                            string rightAddend = unusedTokens.Pop().Value;
                            string result = Evaluator.Add(leftAddend, rightAddend);

                            unusedTokens.Push(new NumberToken(result));
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine("Could not evaluate subtraction because not enough operands were present. This may be an internal error. {0}", ex.Message);
                        }
                        break;
                    case TokenType.Division:
                        equation.Pop();
                        try
                        {
                            string divisor = unusedTokens.Pop().Value;
                            string dividend = unusedTokens.Pop().Value;
                            string result = Evaluator.Divide(dividend, divisor);

                            unusedTokens.Push(new NumberToken(result));
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine("Could not evaluate subtraction because not enough operands were present. This may be an internal error. {0}", ex.Message);
                        }
                        break;
                    case TokenType.Multiplication:
                        equation.Pop();
                        try
                        {
                            string multiplicand = unusedTokens.Pop().Value;
                            string multiplier = unusedTokens.Pop().Value;
                            string result = Evaluator.Multiply(multiplicand, multiplier);

                            unusedTokens.Push(new NumberToken(result));
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine("Could not evaluate subtraction because not enough operands were present. This may be an internal error. {0}", ex.Message);
                        }
                        break;
                    case TokenType.Function:
                        Token function = equation.Pop();
                        try
                        {
                            string localResult = String.Empty;
                            string right = String.Empty;

                            switch (function.Value.ToLower())
                            {
                                case "sin":
                                    right = unusedTokens.Pop().Value;
                                    localResult = Evaluator.Sine(right);
                                    break;
                                case "cos":
                                    right = unusedTokens.Pop().Value;
                                    localResult = Evaluator.Cosine(right);
                                    break;
                                case "tan":
                                    right = unusedTokens.Pop().Value;
                                    localResult = Evaluator.Tangent(right);
                                    break;
                                case "arcsin":
                                    right = unusedTokens.Pop().Value;
                                    localResult = Evaluator.InverseSine(right);
                                    break;
                                case "arccos":
                                    right = unusedTokens.Pop().Value;
                                    localResult = Evaluator.InverseCosine(right);
                                    break;
                                case "arctan":
                                    right = unusedTokens.Pop().Value;
                                    localResult = Evaluator.InverseTangent(right);
                                    break;
                            }

                            unusedTokens.Push(new NumberToken(localResult));
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine("Could not evaluate subtraction because not enough operands were present. This may be an internal error. {0}", ex.Message);
                        }
                        break;
                    default:
                        throw new TokenUnrecognizedException(String.Format("{0}", equation.Peek()));
                }
            }

            calculationResult = unusedTokens.Pop().Value;

            return calculationResult;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
            }
        }

        #endregion
    }
}
