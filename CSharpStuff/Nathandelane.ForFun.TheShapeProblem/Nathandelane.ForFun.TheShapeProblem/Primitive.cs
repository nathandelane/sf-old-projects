using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Nathandelane.ForFun.TheShapeProblem
{
	public class Primitive : IGeometric
	{
		#region Fields

		private Point _startPoint;

		#endregion

		#region Properties

		/// <summary>
		/// Gets the start point.
		/// </summary>
		public Point StartPoint
		{
			get { return _startPoint; }
		}

		#endregion

		#region Constructors

		public Primitive(Point startPoint)
		{
			_startPoint = startPoint;
		}

		#endregion

		#region Methods

		public abstract void Draw()
		{
			// TODO: Implement this
		}

		#endregion
	}
}
