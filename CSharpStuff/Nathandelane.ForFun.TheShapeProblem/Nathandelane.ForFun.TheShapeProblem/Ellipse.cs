using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.ForFun.TheShapeProblem
{
	/// <summary>
	/// Creates an Ellipse, which may be vertically and horizontally aligned, or based on its radial points, transformed radially.
	/// </summary>
	public class Ellipse : Primitive
	{
		#region Properties

		public Point Center
		{
			get
			{
				Point center = new Point();
				double greaterX = Math.Max(Points[0].X, Points[1].X);
				double lesserX = Math.Min(Points[0].X, Points[1].X);
				double greaterY = Math.Max(Points[0].Y, Points[1].Y);
				double lesserY = Math.Min(Points[0].Y, Points[1].Y);

				center.X = greaterX - ((greaterX - lesserX) / 2);
				center.Y = greaterY - ((greaterY - lesserY) / 2);

				return center;
			}
		}

		#endregion

		#region Constructors

		public Ellipse(Point focus1, Point focus2)
			: base(new Point[] { focus1, focus2 })
		{
		}

		#endregion
	}
}
