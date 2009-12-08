﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Nathandelane.ForFun.TheShapeProblem
{
	public class Circle : Ellipse
	{
		#region Properties

		/// <summary>
		/// Gets the radius of the Circle.
		/// </summary>
		public double Radius
		{
			get { return base.SemiMinorRadius; }
		}

		#endregion

		#region Constructors

		public Circle(Point origin, Point radialPoint)
			: base(origin, radialPoint, radialPoint)
		{
		}

		#endregion
	}
}