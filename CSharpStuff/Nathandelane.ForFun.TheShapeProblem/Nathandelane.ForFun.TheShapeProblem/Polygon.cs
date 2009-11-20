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

		private List<Point> _corners;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the corners.
		/// </summary>
		public List<Point> CornerPoints
		{
			get { return _corners; }
		}

		/// <summary>
		/// Gets a specific point of the Polygon.
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public Point this[int index]
		{
			get { return _corners[index]; }
		}

		#endregion

		#region Constructors

		public Polygon(Point startPoint)
			: base(startPoint)
		{
			_corners = new List<Point>();

			Attribute[] attributes = Attribute.GetCustomAttributes(this.GetType());
			if (attributes.Length > 0)
			{
				foreach (Attribute nextAttribute in attributes)
				{
					if (nextAttribute is AnglesMustEqualAttribute)
					{
						AnglesMustEqualAttribute attr = (AnglesMustEqualAttribute)nextAttribute;
						int totalDegress = CalculateTotalDegrees();

						if (totalDegress != attr.Degrees)
						{
							throw new Exception(String.Format("Angles must equal {0} but equal {1}", attr.Degrees, totalDegress));
						}
					}
				}
			}
		}

		public Polygon(Point startPoint, List<Point> corners)
			: this(startPoint)
		{
			_corners = corners;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a point to the polygon.
		/// </summary>
		/// <param name="point"></param>
		public virtual void AddCorner(Point point)
		{
			_corners.Add(point);
		}

		/// <summary>
		/// Draws the polygon.
		/// </summary>
		public override void Draw()
		{
			// TODO: implement this method
		}

		public int CalculateTotalDegrees()
		{
			int result = 0;

			if (_corners.Count > 1)
			{
				if (_corners.Count == 2)
				{
					result = 180;
				}
				else
				{
					// TODO: calculate all points, convex = +, concave = -
				}
			}

			return result;
		}

		#endregion
	}
}
