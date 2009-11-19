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
		Corners CornerPoints { get; }

		void AddCorner(Point point);
	}
}
