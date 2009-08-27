using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.PersonalCalculator
{
	public class Equation
	{
		#region Fields

		private List<IToken> _eq;

		#endregion

		#region Constructors

		private Equation()
		{
			_eq = new List<IToken>();
		}

		#endregion

		#region Methods

		public static Equation Parse(string eq)
		{
			Equation equation = new Equation();
			
			return equation;
		}

		#endregion
	}
}
