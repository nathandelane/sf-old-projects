using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Math.PersonalCalculator
{
    internal class NumberToken : Token
    {
        #region Constructors

        public NumberToken(string value)
            : base(value, TokenType.Number)
        {
        }

        #endregion

        #region Static Methods

        public static bool Matches(string value)
        {
            bool result = false;
            Regex regex = new Regex("[\\d.,]+");

            if (regex.IsMatch(value))
            {
                result = true;
            }

            return result;
        }

        #endregion
    }
}
