using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Nathandelane.ForFun.TheWrongWay
{
	public abstract class Shape
	{
		#region Fields

		private string _name;
		private Point _startPoint;

		#endregion

		#region Properties

		public string Name
		{
			get { return _name; }
		}

		public Point StartPoint
		{
			get { return _startPoint; }
		}

		#endregion

		#region Constructors

		public Shape(string name, Point startPoint)
		{
			_name = name;
			_startPoint = startPoint;
		}

		#endregion
	}
}
