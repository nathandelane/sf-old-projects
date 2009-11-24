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


		#endregion

		#region Properties


		#endregion

		#region Constructors

		public Polygon(Point origin)
			: base(origin)
		{
			ParseAttributes();
		}

		public Polygon(IEnumerable<Point> points)
			: base(points)
		{
			ParseAttributes();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Adds a point to the Polygon.
		/// </summary>
		/// <param name="point"></param>
		public override void AddPoint(Point point)
		{
			base.AddPoint(point);
		}

		/// <summary>
		/// Parse any expected .NET attributes.
		/// </summary>
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
						double totalDegress = 0;

						foreach (double angle in Angles)
						{
							totalDegress += angle;
						}

						if (totalDegress != attr.Degrees)
						{
							throw new Exception(String.Format("Angles must equal {0} but equal {1}.", attr.Degrees, totalDegress));
						}
					}
					else if (nextAttribute is AllSidesMustBeEqualAttribute)
					{
						AllSidesMustBeEqualAttribute attr = (AllSidesMustBeEqualAttribute)nextAttribute;
						bool sidesAreEqual = true;
						double initialSideLength = 0.0;

						foreach (double sideLength in SegmentLengths)
						{
							if (initialSideLength == 0.0)
							{
								initialSideLength = sideLength;
							}
							else
							{
								if (sideLength != initialSideLength)
								{
									sidesAreEqual = false;
									break;
								}
							}

							if (!sidesAreEqual)
							{
								throw new Exception(String.Format("All sides must be equal to {0}.", initialSideLength));
							}
						}
					}
					else if (nextAttribute is AllAnglesMustBeEqualAttribute)
					{
						AllAnglesMustBeEqualAttribute attr = (AllAnglesMustBeEqualAttribute)nextAttribute;
						bool anglesAreEqual = true;
						double initialAngle = 0.0;

						foreach (double angle in SegmentLengths)
						{
							if (initialAngle == 0.0)
							{
								initialAngle = angle;
							}
							else
							{
								if (angle != initialAngle)
								{
									anglesAreEqual = false;
									break;
								}
							}

							if (!anglesAreEqual)
							{
								throw new Exception(String.Format("All angles must be equal to {0}.", initialAngle));
							}
						}
					}
				}
			}
		}

		#endregion
	}
}
