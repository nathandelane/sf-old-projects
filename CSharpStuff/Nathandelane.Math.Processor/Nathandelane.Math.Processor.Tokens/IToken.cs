using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Math.Processor.Tokens
{
    public interface IToken
    {
        #region Propeties

        TokenType Type { get; set; }
        TokenPrecedence Precedence { get; set; }
        string Value { get; set; }

        #endregion
    }
}
