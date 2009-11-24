using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Nathandelane.ForFun.TheWrongWay
{
	public class Ellipse : Shape
	{
		#region Fields

		private double _horizontalRadius;
		private double _verticalRadius;

		#endregion

		#region Properties

		public double HorizontalRadius
		{
			get { return _horizontalRadius; }
		}

		public double VerticalRadius
		{
			get { return _verticalRadius; }
		}

		public Point Center
		{
			get { return base.StartPoint; }
		}

		#endregion

		#region Constructors

		public Ellipse(string name, Point center, double horizontalRadius, double verticalRadius)
			: base(name, center)
		{
			_horizontalRadius = horizontalRadius;
			_verticalRadius = verticalRadius;
		}

		#endregion
	}
}
