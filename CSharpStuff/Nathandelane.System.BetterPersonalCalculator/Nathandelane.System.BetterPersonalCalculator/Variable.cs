using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public class Variable : IExpression
	{
		#region Fields

		private static readonly string __matchExpression =  "^[A-Za-z_]+[A-Za-z_\\d]*";

		private string _name;

		#endregion

		#region Properties

		public static string MatchExpression
		{
			get { return Variable.__matchExpression; }
		}

		#endregion

		#region Constructors

		public Variable(string name)
		{
			_name = name;
		}

		#endregion

		#region Methods

		public IExpression Calculate(IDictionary<string, IExpression> context)
		{
			return context[_name];
		}

		public override string ToString()
		{
			return _name;
		}

		#endregion
	}
}
