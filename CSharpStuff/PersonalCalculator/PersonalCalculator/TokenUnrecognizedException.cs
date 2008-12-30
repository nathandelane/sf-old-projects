using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.Math.PersonalCalculator
{
    internal class TokenUnrecognizedException : Exception
    {
        public TokenUnrecognizedException(string message)
            : base(message)
        {
        }
    }
}
