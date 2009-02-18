using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nathandelane.Math.Processor.Tokens;

namespace Nathandelane.Math.Processor.Evaluation
{
	public class Expression : List<IToken>
	{
		public Expression()
			: base()
		{
		}
	}
}
