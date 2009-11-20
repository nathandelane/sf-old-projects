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
		/// Gets a specific corner of the IPolyoganol.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		Point this[int index] { get; }

		/// <summary>
		/// Adds a point to the IPolyoganol.
		/// </summary>
		/// <param name="point"></param>
		void AddPoint(Point point);
	}
}
