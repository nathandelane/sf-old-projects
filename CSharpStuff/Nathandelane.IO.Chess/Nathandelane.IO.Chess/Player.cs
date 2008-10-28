using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.IO.Chess
{
	public class Player
	{
		private PlayerType _type;
		private ChessPieceColor _color;
		private PlayerStatus _status;
		private int _points;

		public Player(PlayerType type, ChessPieceColor color)
		{
			_type = type;
			_color = color;
			_status = PlayerStatus.Normal;
			_points = 0;
		}

		public ChessPieceColor Color
		{
			get { return _color; }
		}

		public PlayerType Type
		{
			get { return _type; }
		}

		public PlayerStatus Status
		{
			get { return _status; }
		}

		public int Points
		{
			get { return _points; }
		}

		public void Check()
		{
			_status = PlayerStatus.Check;
		}

		public void CheckMate()
		{
			_status = PlayerStatus.CheckMate;
		}

		public void ResetStatus()
		{
			_status = PlayerStatus.Normal;
		}

		public void AddPoints(ChessPieceType value)
		{
			_points += (int)value;
		}
	}
}
