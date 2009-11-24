using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Nathandelane.ForFun.TheShapeProblem
{
	[AllSidesMustBeEqual(true)]
	public class Rhombus : Quadrilateral
	{
		#region Constructors

		public Rhombus(Point origin, Point endOfSide1, Point endOfSide2, Point endOfSide3)
			: base(origin, endOfSide1, endOfSide2, endOfSide3)
		{
		}

		#endregion
	}
}
