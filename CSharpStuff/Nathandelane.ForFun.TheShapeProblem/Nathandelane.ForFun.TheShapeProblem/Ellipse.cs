using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Nathandelane.ForFun.TheShapeProblem
{
	public class Ellipse : Primitive, IElliptical
	{
		#region Fields

		private double _horizontalRadius;
		private double _verticalRadius;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the horizontal radius of the Ellipse.
		/// </summary>
		public double HorizontalRadius
		{
			get { return _horizontalRadius; }
		}

		/// <summary>
		/// Gets the vertical radius of the Ellipse.
		/// </summary>
		public double VerticalRadius
		{
			get { return _verticalRadius; }
		}

		#endregion

		#region Constructors

		public Ellipse(Point startPoint, double horizontalRadius, double verticalRadius)
			: base(startPoint)
		{
			_horizontalRadius = horizontalRadius;
			_verticalRadius = verticalRadius;
		}

		#endregion

		#region Methods

		public override void Draw()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
