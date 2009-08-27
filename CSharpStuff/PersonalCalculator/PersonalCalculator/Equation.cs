using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
    /// <summary>
    /// This class takes care of and maintains a particular equation. The equation comes into the Parse function as infix, and comes out as a postfix equation.
    /// </summary>
    internal class Equation
    {
        #region Fields

        private static Stack<Token> _tokens;

        #endregion

        #region Properties

        public int Length
        {
            get { return _tokens.Count; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor. This may only be used internally, making this class a singleton. Only one equation as it were may be present at a time.
        /// </summary>
        private Equation()
        {
            _tokens = new Stack<Token>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Parses a string equation.
        /// </summary>
        /// <param name="equation"></param>
        /// <returns></returns>
        public static Equation Parse(string userInput)
        {
            Equation equation = new Equation();
            Token lastToken = Token.CreateNullToken();
            char[] parts = userInput.ToCharArray();
            int numParts = parts.Length;
            string tokenValue = String.Empty;

            for (int index = 0; index < numParts; index++)
            {
                tokenValue = String.Format("{0}", parts[index]);

                if (ConditionalOperatorToken.Matches(tokenValue) || AssignmentOperatorToken.Matches(tokenValue))
                {
                    if (tokenValue.Equals("=")) // Could be either an assignment operator or a conditional
                    {
                        if (String.Format("{0}", parts[index + 1]).Equals("="))
                        {
                            lastToken = new ConditionalOperatorToken(String.Format("{0}=", tokenValue));
                            index++;
                        }
                        else
                        {
                            lastToken = new AssignmentOperatorToken();
                        }
                    }
                    else if (tokenValue.Equals("!")) // Could either be a conditional or factorial
                    {
                        if (String.Format("{0}", parts[index + 1]).Equals("="))
                        {
                            lastToken = new ConditionalOperatorToken(String.Format("{0}=", tokenValue));
                            index++;
                        }
                        else
                        {
                            lastToken = new FactorialToken();
                        }
                    }
                    else // It is a conditional that is either one or two characters long, i.e. <, >, <=, >=
                    {
                        if (String.Format("{0}", parts[index + 1]).Equals("="))
                        {
                            lastToken = new ConditionalOperatorToken(String.Format("{0}=", tokenValue));
                            index++;
                        }
                        else
                        {
                            lastToken = new ConditionalOperatorToken(tokenValue);
                        }
                    }
                }
                else if (SpecialNumberToken.Matches(tokenValue))
                {
                    int tokenIndex = index;
                    string strPart = String.Empty;

                    do
                    {
                        tokenValue = String.Format("{0}{1}", tokenValue, strPart);
                        tokenIndex++;

                        if (tokenIndex < numParts)
                        {
                            strPart = String.Format("{0}", parts[tokenIndex]);
                        }
                        else
                        {
                            strPart = String.Empty;
                        }
                    }
                    while (FunctionToken.Matches(strPart));

                    lastToken = new SpecialNumberToken(tokenValue);
                    index = tokenIndex - 1;
                }
                else if (NumberToken.Matches(tokenValue)) // Common case, where the token is a number
                {
                    int tokenIndex = index;
                    string strPart = String.Empty;

                    do
                    {
                        tokenValue = String.Format("{0}{1}", tokenValue, strPart);
                        tokenIndex++;

                        if (tokenIndex < numParts)
                        {
                            strPart = String.Format("{0}", parts[tokenIndex]);
                        }
                        else
                        {
                            strPart = String.Empty;
                        }
                    }
                    while (NumberToken.Matches(strPart));

                    lastToken = new NumberToken(tokenValue);
                    index = tokenIndex - 1;
                }
                else if (AdditionToken.Matches(tokenValue))
                {
                    lastToken = new AdditionToken();
                }
                else if (SubtractionToken.Matches(tokenValue) || NegationToken.Matches(tokenValue))
                {
                    if (lastToken.Type == TokenType.Number || lastToken.Type == TokenType.RightPerenthesis)
                    {
                        lastToken = new SubtractionToken();
                    }
                    else
                    {
                        lastToken = new NegationToken();
                    }
                }
                else if (MultiplicationToken.Matches(tokenValue))
                {
                    lastToken = new MultiplicationToken();
                }
                else if (DivisionToken.Matches(tokenValue))
                {
                    lastToken = new DivisionToken();
                }
				else if(ModulusToken.Matches(tokenValue))
				{
					lastToken = new ModulusToken();
				}
                else if (LeftPerenthesisToken.Matches(tokenValue))
                {
                    lastToken = new LeftPerenthesisToken();
                }
                else if (RightPerenthesisToken.Matches(tokenValue))
                {
                    lastToken = new RightPerenthesisToken();
                }
                else if (FunctionToken.Matches(tokenValue))
                {
                    int tokenIndex = index;
                    string strPart = String.Empty;

                    do
                    {
                        tokenValue = String.Format("{0}{1}", tokenValue, strPart);
                        tokenIndex++;

                        if (tokenIndex < numParts)
                        {
                            strPart = String.Format("{0}", parts[tokenIndex]);
                        }
                        else
                        {
                            strPart = String.Empty;
                        }
                    }
                    while (FunctionToken.Matches(strPart));

                    lastToken = new FunctionToken(tokenValue);
                    index = tokenIndex - 1;
                }
                else if (BooleanToken.Matches(tokenValue))
                {
                    int tokenIndex = index;
                    string strPart = String.Empty;

                    do
                    {
                        tokenValue = String.Format("{0}{1}", tokenValue, strPart);
                        tokenIndex++;

                        if (tokenIndex < numParts)
                        {
                            strPart = String.Format("{0}", parts[tokenIndex]);
                        }
                        else
                        {
                            strPart = String.Empty;
                        }
                    }
                    while (BooleanToken.Matches(strPart));

                    switch (tokenValue)
                    {
                        case "True":
                            lastToken = new BooleanToken("True");
                            break;
                        case "False":
                            lastToken = new BooleanToken("False");
                            break;
                    }

                    index = tokenIndex - 1;
                }
                else if (FactorialToken.Matches(tokenValue))
                {
                    lastToken = new FactorialToken();
                }
                else if (PowerToken.Matches(tokenValue))
                {
                    lastToken = new PowerToken();
                }
                else if (BitwiseOperationToken.Matches(tokenValue))
                {
                    lastToken = new BitwiseOperationToken(tokenValue);
                }
                else // This case is that we are probably dealing with a variable, but for now I'm going to throw an exception.
                {
                    // TODO: fix this to handle variables.
                    throw new TokenUnrecognizedException(tokenValue);
                }

				if (!lastToken.Equals(Token.CreateNullToken()))
				{
					AddComponent(lastToken);
				}
			}

            // Need to transform the equation into a postfix equation so that I can evaluate it more easily.
            _tokens = ReverseTokens(_tokens);
            _tokens = PostfixateEquation(_tokens);

            return equation;
        }

        /// <summary>
        /// Peeks at the next token on the stack.
        /// </summary>
        /// <returns></returns>
        public Token Peek()
        {
            return _tokens.Peek();
        }

        /// <summary>
        /// Pops the next token off of the stack.
        /// </summary>
        /// <returns></returns>
        public Token Pop()
        {
            return _tokens.Pop();
        }

        #endregion

        #region Private Methods

        private static Stack<Token> ReverseTokens(Stack<Token> stack)
        {
            Stack<Token> tokens = new Stack<Token>();

            while (stack.Count > 0)
            {
                tokens.Push(stack.Pop());
            }

            return tokens;
        }

        /// <summary>
        /// Adds a component to the _tokens list.
        /// </summary>
        /// <param name="token"></param>
        private static void AddComponent(Token token)
        {
            _tokens.Push(token);
        }

        /// <summary>
        /// Transforms the infix equation into a postfix equation.
        /// </summary>
        /// <param name="equation"></param>
        /// <returns></returns>
        private static Stack<Token> PostfixateEquation(Stack<Token> equation)
        {
            Stack<Token> newEquation = new Stack<Token>();
            Stack<Token> operationStack = new Stack<Token>();

            while (equation.Count > 0)
            {
                Token nextToken = equation.Pop();

                switch (nextToken.Type)
                {
                    case TokenType.BooleanToken:
                        newEquation.Push(nextToken);
                        break;
                    case TokenType.AssignmentOperator:
                        operationStack.Push(nextToken);
                        break;
                    case TokenType.ConditionalOperator:
                        operationStack.Push(nextToken);
                        break;
                    case TokenType.SpecialNumber:
                        newEquation.Push(nextToken);
                        break;
                    case TokenType.Number:
                        newEquation.Push(nextToken);
                        break;
                    case TokenType.Negation:
                        operationStack.Push(nextToken);
                        break;
                    case TokenType.Subtraction:
                        if (operationStack.Count > 0 && operationStack.Peek().Type > nextToken.Type)
                        {
                            newEquation.Push(operationStack.Pop());
                            operationStack.Push(nextToken);
                        }
                        else
                        {
                            operationStack.Push(nextToken);
                        }
                        break;
                    case TokenType.Addition:
                        if (operationStack.Count > 0 && operationStack.Peek().Type > nextToken.Type)
                        {
                            newEquation.Push(operationStack.Pop());
                            operationStack.Push(nextToken);
                        }
                        else
                        {
                            operationStack.Push(nextToken);
                        }
                        break;
                    case TokenType.Division:
                        if (operationStack.Count > 0 && (operationStack.Peek().Type < nextToken.Type && operationStack.Peek().Type != TokenType.LeftPerenthesis))
                        {
                            newEquation.Push(operationStack.Pop());
                            operationStack.Push(nextToken);
                        }
                        else
                        {
                            operationStack.Push(nextToken);
                        }
                        break;
                    case TokenType.Multiplication:
                        if (operationStack.Count > 0 && (operationStack.Peek().Type < nextToken.Type && operationStack.Peek().Type != TokenType.LeftPerenthesis))
                        {
                            newEquation.Push(operationStack.Pop());
                            operationStack.Push(nextToken);
                        }
                        else
                        {
                            operationStack.Push(nextToken);
                        }
                        break;
                    case TokenType.LeftPerenthesis:
                        operationStack.Push(nextToken);
                        break;
                    case TokenType.RightPerenthesis:
                        try
                        {
                            while (operationStack.Peek().Type != TokenType.LeftPerenthesis)
                            {
                                newEquation.Push(operationStack.Pop());
                            }

                            operationStack.Pop(); // Pops the left perenthesis off of the stack.
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine("Could not find left perenthesis on operation stack. This might be an internal error. {0}", ex.Message);
                        }
                        break;
                    case TokenType.Function:
                        operationStack.Push(nextToken);
                        break;
                    case TokenType.Factorial:
                        operationStack.Push(nextToken);
                        break;
                    case TokenType.Power:
                        operationStack.Push(nextToken);
                        break;
                    case TokenType.BitwiseOperation:
                        operationStack.Push(nextToken);
                        break;
                }
            }

            while (operationStack.Count > 0)
            {
                newEquation.Push(operationStack.Pop());
            }

            newEquation = ReverseTokens(newEquation);

            return newEquation;
        }

        #endregion
    }
}
