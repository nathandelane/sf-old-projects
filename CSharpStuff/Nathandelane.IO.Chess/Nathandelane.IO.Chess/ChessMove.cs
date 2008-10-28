using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.IO.Chess
{
	public class ChessMove
	{
		private ChessMoveType _moveType;
		private List<DirectionDistance> _directionDistance;

		public ChessMove(ChessMoveType moveType, List<DirectionDistance> directionDistance)
		{
			_moveType = moveType;
			_directionDistance = directionDistance;
		}

		public ChessMove(ChessMoveType moveType, DirectionDistance[] directionDistance)
		{
			_moveType = moveType;
			_directionDistance = new List<DirectionDistance>();

			foreach (DirectionDistance dd in directionDistance)
			{
				_directionDistance.Add(dd);
			}
		}

		public ChessMoveType MoveType
		{
			get { return _moveType; }
		}

		public DirectionDistance this[int index]
		{
			get { return _directionDistance[index]; }
		}

		public DirectionDistance[] GetDirectionDistancePairs()
		{
			return _directionDistance.ToArray();
		}

		public int ToIndex()
		{
			int result = 0;
			int x = 0;
			int y = 0;

			foreach (DirectionDistance dd in _directionDistance)
			{
				switch (dd.Direction)
				{
					case MoveDirection.PlayerBackward:
						y -= dd.Distance;
						break;
					case MoveDirection.PlayerForward:
						y += dd.Distance;
						break;
					case MoveDirection.PlayerLeft:
						x -= dd.Distance;
						break;
					case MoveDirection.PlayerRight:
						x += dd.Distance;
						break;
				}
			}

			result = y * ChessBoard.ChessBoardWidth + x;

			return result;
		}

		public List<ChessMove> Granularize()
		{
			List<ChessMove> list = new List<ChessMove>();

			foreach (DirectionDistance dd in _directionDistance)
			{
				for (int i = 0; i < dd.Distance; i++)
				{
					list.Add(new ChessMove(ChessMoveType.Normal, new DirectionDistance[] { new DirectionDistance(dd.Direction, 1) }));
				}
			}

			return list;
		}

		public bool Equals(ChessMove chessMove)
		{
			bool result = false;

			if (MoveType == chessMove.MoveType)
			{
				DirectionDistance[] thisDd = GetDirectionDistancePairs();
				DirectionDistance[] thatDd = chessMove.GetDirectionDistancePairs();

				if (thisDd.Length == thatDd.Length)
				{
					int counter = 0;
					for (int i = 0; i < thisDd.Length; i++)
					{
						if (thisDd[i] == thatDd[i])
						{
							counter++;
						}
					}

					if (counter == thisDd.Length)
					{
						result = true;
					}
				}
			}

			return result;
		}
	}
}
