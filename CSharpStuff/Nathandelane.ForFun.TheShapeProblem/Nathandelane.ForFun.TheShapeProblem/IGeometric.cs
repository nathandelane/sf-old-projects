using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Nathandelane.ForFun.TheShapeProblem
{
	public interface IGeometric
	{
		/// <summary>
		/// Gets all of the points defined in the IGeometric.
		/// </summary>
		List<Point> Points { get; }
	}
}
