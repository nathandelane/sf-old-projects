using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Nathandelane.Math.PersonalCalculator
{
    internal class FunctionToken : Token
    {
        #region Constructors

        public FunctionToken(string value)
            : base(value, TokenType.Function)
        {
        }

        #endregion

        #region Static Methods

        public static bool Matches(string value)
        {
            bool result = false;
            Regex regex = new Regex("[a-z]+");

            if(regex.IsMatch(value))
            {
                result = true;
            }

            return result;
        }

        #endregion
    }
}
