﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Nathandelane.ForFun.TheShapeProblem
{
	public interface IGeometric
	{
		Point StartPoint { get; }

		void Draw();
	}
}
