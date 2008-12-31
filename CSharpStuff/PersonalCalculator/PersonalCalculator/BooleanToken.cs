using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.Math.PersonalCalculator
{
    internal class BooleanToken : Token
    {
        #region Constructors

        public BooleanToken(string value)
            : base(value, TokenType.SpecialNumber)
        {
        }

        #endregion
    }
}
