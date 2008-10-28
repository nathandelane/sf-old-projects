using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.IO.Chess
{
	public class DirectionDistance
	{
		private MoveDirection _direction;
		private int _distance;

		public DirectionDistance(MoveDirection direction, int maxDistance)
		{
			_direction = direction;
			_distance = maxDistance;
		}

		public MoveDirection Direction
		{
			get { return _direction; }
		}

		public int Distance
		{
			get { return _distance; }
		}
	}
}
