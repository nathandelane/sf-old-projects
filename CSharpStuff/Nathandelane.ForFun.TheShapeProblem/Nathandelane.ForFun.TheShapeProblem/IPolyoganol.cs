using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Nathandelane.ForFun.TheShapeProblem
{
	public interface IPolyoganol : IGeometric
	{
		/// <summary>
		/// Gets the corner points of the the IPolyoganol.
		/// </summary>
		List<Point> CornerPoints { get; }

		/// <summary>
		/// Gets a specific corner of the IPolyoganol.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		Point this[int index] { get; }

		void AddCorner(Point point);
	}
}
