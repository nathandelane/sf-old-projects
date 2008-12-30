using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Math.PersonalCalculator
{
    internal class AdditionToken : Token
    {
        #region Constructors

        public AdditionToken()
            : base("+", TokenType.Addition)
        {
        }

        #endregion

        #region Static Methods

        public static bool Matches(string value)
        {
            bool result = false;
            Regex regex = new Regex("[\\+]{1}");

            if (regex.IsMatch(value))
            {
                result = true;
            }

            return result;
        }

        #endregion
    }
}
