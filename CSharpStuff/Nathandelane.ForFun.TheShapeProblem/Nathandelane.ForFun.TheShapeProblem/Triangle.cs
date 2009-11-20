using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Nathandelane.ForFun.TheShapeProblem
{
	[AnglesMustEqual(180)]
	public class Triangle : Polygon
	{
		#region Fields

		public static readonly int NumberOfSides = 3;

		#endregion

		#region Constructors

		public Triangle(Point startPoint, Point endOfSide1, Point endOfSide2)
			: base(startPoint)
		{
			base.AddCorner(endOfSide1);
			base.AddCorner(endOfSide2);
		}

		#endregion

		#region Methods

		public override void AddCorner(Point point)
		{
			throw new NotSupportedException("Not allowed.");
		}

		#endregion
	}
}
