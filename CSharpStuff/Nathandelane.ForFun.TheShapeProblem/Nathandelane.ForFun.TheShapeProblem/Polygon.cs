using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Nathandelane.ForFun.TheShapeProblem
{
	public class Polygon : Primitive, IPolyoganol
	{
		#region Fields

		private Corners _corners;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the corners.
		/// </summary>
		public Corners CornerPoints
		{
			get { return _corners; }
		}

		#endregion

		#region Constructors

		public Polygon(Point startPoint)
			: base(startPoint)
		{
			_corners = new Corners();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a point to the polygon.
		/// </summary>
		/// <param name="point"></param>
		public void AddCorner(GeneralPoint point)
		{
			_corners.Add(point);
		}

		/// <summary>
		/// Draws the polygon.
		/// </summary>
		public void Draw()
		{
			// TODO: implement this method
		}

		#endregion
	}
}
