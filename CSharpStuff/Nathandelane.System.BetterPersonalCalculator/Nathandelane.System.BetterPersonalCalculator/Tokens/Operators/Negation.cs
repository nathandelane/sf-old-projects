using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class Negation : AbstractToken, IArithmeticOperator
	{
		#region Constructors

		public Negation(AbstractToken token)
			: base(token)
		{
		}

		#endregion
	
		#region Methods

		/// <summary>
		/// Calculates the negative of the value.
		/// </summary>
		/// <returns></returns>
		public AbstractToken Calculate()
		{
			AbstractToken result = new Number(Value);

			if(Value.StartsWith("-"))
			{
				result = new Number(Value.Substring(1));
			}
			else
			{
				result = new Number(String.Concat("-", Value));
			}

			return result;
		}

		#endregion}
}
