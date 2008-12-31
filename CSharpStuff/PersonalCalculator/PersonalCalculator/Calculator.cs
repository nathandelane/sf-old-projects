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
                    case TokenType.ConditionalOperator:
                        string condRight = unusedTokens.Pop().Value;
                        string condLeft = unusedTokens.Pop().Value;
                        switch (equation.Peek().Value)
                        {
                            case "<":
                                calculationResult = Evaluator.LessThan(condLeft, condRight);
                                break;
                            case ">":
                                calculationResult = Evaluator.GreaterThan(condLeft, condRight);
                                break;
                            case "!=":
                                calculationResult = Evaluator.NotEqual(condLeft, condRight);
                                break;
                            case "<=":
                                calculationResult = Evaluator.LesThanOrEqual(condLeft, condRight);
                                break;
                            case ">=":
                                calculationResult = Evaluator.GreaterThanOrEqual(condLeft, condRight);
                                break;
                            case "==":
                                calculationResult = Evaluator.AreEqual(condLeft, condRight);
                                break;
                        }

                        equation.Pop();
                        unusedTokens.Push(new BooleanToken(calculationResult));
                        break;
                    case TokenType.SpecialNumber:
                        switch (equation.Peek().Value.ToLower())
                        {
                            case "e":
                                calculationResult = Evaluator.GetE();
                                break;
                            case "pi":
                                calculationResult = Evaluator.GetPi();
                                break;
                        }

                        equation.Pop();
                        unusedTokens.Push(new NumberToken(calculationResult));
                        break;
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
                            string left = String.Empty;

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
                                case "log":
                                    right = unusedTokens.Pop().Value;
                                    localResult = Evaluator.Logarithm(String.Format("{0}", 10), right);
                                    break;
                                case "ln":
                                    right = unusedTokens.Pop().Value;
                                    localResult = Evaluator.Logarithm(String.Format("{0}", System.Math.E), right);
                                    break;
                                case "bin":
                                    right = unusedTokens.Pop().Value;
                                    localResult = Evaluator.ToBinary(right);
                                    break;
                                case "hex":
                                    right = unusedTokens.Pop().Value;
                                    localResult = Evaluator.ToHexadecimal(right);
                                    break;
                                case "oct":
                                    right = unusedTokens.Pop().Value;
                                    localResult = Evaluator.ToOctal(right);
                                    break;
                                case "mod":
                                    right = unusedTokens.Pop().Value;
                                    left = unusedTokens.Pop().Value;
                                    localResult = Evaluator.Modulus(left, right);
                                    break;
                                case "round":
                                    right = unusedTokens.Pop().Value;
                                    localResult = Evaluator.Round(right);
                                    break;
                                case "ceil":
                                    right = unusedTokens.Pop().Value;
                                    localResult = Evaluator.Ceiling(right);
                                    break;
                                case "floor":
                                    right = unusedTokens.Pop().Value;
                                    localResult = Evaluator.Floor(right);
                                    break;
                            }

                            unusedTokens.Push(new NumberToken(localResult));
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine("Could not evaluate subtraction because not enough operands were present. This may be an internal error. {0}", ex.Message);
                        }
                        break;
                    case TokenType.Factorial:
                        equation.Pop();
                        try
                        {
                            string left = unusedTokens.Pop().Value;
                            string localResult = Evaluator.Factorial(left);

                            unusedTokens.Push(new NumberToken(localResult));
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine("Could not evaluate subtraction because not enough operands were present. This may be an internal error. {0}", ex.Message);
                        }
                        break;
                    case TokenType.Power:
                        equation.Pop();
                        try
                        {
                            string pow = unusedTokens.Pop().Value;
                            string bas = unusedTokens.Pop().Value;
                            string localResult = Evaluator.Power(bas, pow);

                            unusedTokens.Push(new NumberToken(localResult));
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine("Could not evaluate subtraction because not enough operands were present. This may be an internal error. {0}", ex.Message);
                        }
                        break;
                    case TokenType.BitwiseOperation:
                        string bitwiseOperator = equation.Pop().Value;
                        try
                        {
                            string right = unusedTokens.Pop().Value;
                            string left = unusedTokens.Pop().Value;
                            string localResult = String.Empty;

                            switch (bitwiseOperator)
                            {
                                case "&":
                                    localResult = Evaluator.BitwiseAnd(left, right);
                                    break;
                                case "|":
                                    localResult = Evaluator.BitwiseOr(left, right);
                                    break;
                                case "%":
                                    localResult = Evaluator.BitwiseXor(left, right);
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
