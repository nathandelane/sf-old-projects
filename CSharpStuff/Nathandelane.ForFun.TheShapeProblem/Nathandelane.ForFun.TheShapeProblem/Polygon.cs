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

		private List<double> _segmentLengths;

		#endregion

		#region Properties


		#endregion

		#region Constructors

		public Polygon(Point startPoint)
			: base(startPoint)
		{
			ParseAttributes();
		}

		public Polygon(IEnumerable<Point> points)
			: base(points)
		{
		}

		#endregion

		#region Methods

		private void ParseAttributes()
		{
			Attribute[] attributes = Attribute.GetCustomAttributes(this.GetType());
			if (attributes.Length > 0)
			{
				foreach (Attribute nextAttribute in attributes)
				{
					if (nextAttribute is AnglesMustEqualAttribute)
					{
						AnglesMustEqualAttribute attr = (AnglesMustEqualAttribute)nextAttribute;
						int totalDegress = 0;

						if (totalDegress != attr.Degrees)
						{
							throw new Exception(String.Format("Angles must equal {0} but equal {1}", attr.Degrees, totalDegress));
						}
					}
				}
			}
		}

		/// <summary>
		/// Calculates a segment's length
		/// </summary>
		/// <param name="lastPoint"></param>
		/// <param name="currentPoint"></param>
		/// <returns></returns>
		private double CalculateSegmentLength(Point lastPoint, Point currentPoint)
		{
			double segmentLength = 0.0;

			return segmentLength;
		}

		#endregion
	}
}
