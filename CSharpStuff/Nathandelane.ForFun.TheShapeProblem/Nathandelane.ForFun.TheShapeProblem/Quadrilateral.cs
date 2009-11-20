using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Nathandelane.ForFun.TheShapeProblem
{
	public class Quadrilateral : Polygon
	{
		#region Fields

		public static readonly int NumberOfSides = 4;

		#endregion

		#region Constructors

		public Quadrilateral(Point startPoint, Point endOfSide1, Point endOfSide2, Point endOfSide3)
			: base(startPoint)
		{
			base.AddCorner(endOfSide1);
			base.AddCorner(endOfSide2);
			base.AddCorner(endOfSide3);
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
