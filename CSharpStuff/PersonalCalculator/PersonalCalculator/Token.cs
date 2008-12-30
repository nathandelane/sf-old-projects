using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
    internal class Token
    {
        #region Fields

        private string _value;
        private TokenType _type;

        #endregion

        #region Properties

        public string Value
        {
            get { return _value; }
        }

        public TokenType Type
        {
            get { return _type; }
        }

        #endregion

        #region Constructors

        public Token(string value, TokenType type)
        {
            _value = value;
            _type = type;
        }

        #endregion

        #region Public Methods

        public static Token CreateNullToken()
        {
            Token token = new Token(String.Empty, TokenType.Null);

            return token;
        }

        public bool Equals(Token token)
        {
            bool result = false;

            if (token.Type == _type && token.Value.Equals(_value))
            {
                result = true;
            }

            return result;
        }

        public override string ToString()
        {
            String value = String.Format("Token(Type={0}; Value={1})", _type, _value);

            return value;
        }

        #endregion
    }
}
