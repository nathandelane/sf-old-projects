using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.System.BetterPersonalCalculator
{
	public interface IOperation
	{
		OperationType OperationType { get; }
	}
}
