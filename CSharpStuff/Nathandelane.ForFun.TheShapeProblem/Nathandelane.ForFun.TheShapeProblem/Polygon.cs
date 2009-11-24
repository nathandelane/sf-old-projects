using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nathandelane.ForFun.TheShapeProblem
{
	public class Polygon : Primitive, IPolyoganol
	{
		#region Fields
		
		private List<double> _segment;
		private List<double> _angles;
		
		#endregion

		#region Properties

		/// <summary>
		/// Gets the segment lengths.
		/// </summary>
		public IList<double> Segments
		{
			get { return _segment; }
		}

		/// <summary>
		/// Gets the angles.
		/// </summary>
		public IList<double> Angles
		{
			get { return _angles; }
		}

		/// <summary>
		/// Gets whether the Polygon is equilateral.
		/// </summary>
		public bool IsEquilateral
		{
			get
			{
				bool anglesAndSidesAreEqual = true;

				double initialAngle = _angles[0];
				for(int anglesIndex = 1; anglesIndex < _angles.Count; anglesIndex++)
				{
					if (_angles[anglesIndex] != initialAngle)
					{
						anglesAndSidesAreEqual = false;
					}
				}

				double initialSide = _segment[0];
				for(int sidesIndex = 1; sidesIndex < _segment.Count; sidesIndex++)
				{
					if (_segment[sidesIndex] != initialSide)
					{
						anglesAndSidesAreEqual = false;
					}
				}

				return anglesAndSidesAreEqual;
			}
		}

		#endregion

		#region Constructors

		public Polygon(Point origin)
			: this(new Point[] { origin })
		{
		}

		public Polygon(IEnumerable<Point> points)
			: base(points)
		{
			_segment = new List<double>();
			_angles = new List<double>();

			if (Points.Count > 1)
			{
				Point lastPoint = Points[0];
				for (int pointsIndex = 1; pointsIndex < Points.Count; pointsIndex++)
				{
					_segment.Add(CalculateSegmentLength(lastPoint, Points[pointsIndex]));

					lastPoint = Points[pointsIndex];
				}
				_segment.Add(CalculateSegmentLength(lastPoint, Points[0]));
			}

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

			int newestPointIndex = Points.Count - 1;
			if (Points.Count > 1)
			{
				_segment.Add(CalculateSegmentLength(Points[newestPointIndex - 1], Points[newestPointIndex]));
			}
			else if (Points.Count > 2)
			{
				_angles.Add(CalculateAngle(Points[newestPointIndex - 1], Points[newestPointIndex - 2], Points[newestPointIndex]));
			}
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

						foreach (double sideLength in Segments)
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

						foreach (double angle in Segments)
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
