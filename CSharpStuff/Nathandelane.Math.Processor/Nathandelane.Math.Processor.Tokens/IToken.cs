using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.Math.Processor.Tokens
{
    public interface IToken
    {
        #region Propeties

        TokenType Type { get; }
        TokenPrecedence Precedence { get; set; }
        string Value { get; }

        #endregion

		#region Methods

		bool Matches(string str);

		#endregion
	}
}
