using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class NotANumberToken : Token
	{
		#region Properties

		public override TokenType Type
		{
			get { return TokenType.NotANumber; }
		}

		#endregion

		#region Constructors

		public NotANumberToken()
			: base(String.Empty)
		{
		}

		#endregion
	}
}
