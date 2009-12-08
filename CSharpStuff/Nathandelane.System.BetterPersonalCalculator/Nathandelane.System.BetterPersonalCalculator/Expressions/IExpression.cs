using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public interface IExpression
	{
		IExpression Calculate(IDictionary<string, IExpression> context);
	}
}
