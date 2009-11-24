using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Nathandelane.ForFun.TheShapeProblem
{
	[AnglesMustEqual(360)]
	public class Quadrilateral : Polygon
	{
		#region Fields

		public static readonly int NumberOfSides = 4;

		#endregion

		#region Constructors

		public Quadrilateral(Point origin, Point endOfSide1, Point endOfSide2, Point endOfSide3)
			: base(origin)
		{
			base.AddPoint(endOfSide1);
			base.AddPoint(endOfSide2);
			base.AddPoint(endOfSide3);
		}

		#endregion

		#region Methods

		public override void AddPoint(Point point)
		{
			if (Points.Count == Quadrilateral.NumberOfSides)
			{
				throw new NotSupportedException("Not allowed.");
			}
			else
			{
				base.AddPoint(point);
			}
		}

		#endregion
	}
}
