using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.IO.Chess
{
	public class ChessPiece
	{
		private ChessPieceType _type;
		private ChessPieceColor _color;
		private List<ChessMove> _movesAllowed;
		private int _numberOfMoves;

		public ChessPiece(ChessPieceColor color, ChessPieceType type)
		{
			_type = type;
			_color = color;
			_movesAllowed = new List<ChessMove>();
			_numberOfMoves = 0;
		}

		public void AddAllowedMove(ChessMove chessMove)
		{
			_movesAllowed.Add(chessMove);
		}

		public bool MoveIsAllowed(ChessMove chessMove)
		{
			bool result = false;

			if (_movesAllowed.Contains(chessMove))
			{
				result = true;
			}

			return result;
		}

		public ChessPieceColor Color
		{
			get { return _color; }
		}

		public ChessPieceType Type
		{
			get { return _type; }
		}

		public int NumberOfMoves
		{
			get { return _numberOfMoves; }
		}

		public static ChessPiece CreateKing(ChessPieceColor color)
		{
			ChessPiece king = new ChessPiece(color, ChessPieceType.King);
			king.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerBackward, 1) }));
			king.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerForward, 1) }));
			king.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerLeft, 1) }));
			king.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerRight, 1) }));

			return king;
		}

		public static ChessPiece CreateQueen(ChessPieceColor color)
		{
			ChessPiece queen = new ChessPiece(color, ChessPieceType.Queen);
			queen.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerBackward, 8) }));
			queen.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerForward, 8) }));
			queen.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerLeft, 8) }));
			queen.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerRight, 8) }));
			queen.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerBackward, 8), new DirectionDistance(MoveDirection.PlayerRight, 8) }));
			queen.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerBackward, 8), new DirectionDistance(MoveDirection.PlayerLeft, 8) }));
			queen.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerForward, 8), new DirectionDistance(MoveDirection.PlayerRight, 8) }));
			queen.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerForward, 8), new DirectionDistance(MoveDirection.PlayerLeft, 8) }));

			return queen;
		}

		public static ChessPiece CreateBishop(ChessPieceColor color)
		{
			ChessPiece bishop = new ChessPiece(color, ChessPieceType.Bishop);
			bishop.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerBackward, 8), new DirectionDistance(MoveDirection.PlayerLeft, 8) }));
			bishop.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerBackward, 8), new DirectionDistance(MoveDirection.PlayerRight, 8) }));
			bishop.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerForward, 8), new DirectionDistance(MoveDirection.PlayerLeft, 8) }));
			bishop.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerForward, 8), new DirectionDistance(MoveDirection.PlayerRight, 8) }));

			return bishop;
		}

		public static ChessPiece CreateKnight(ChessPieceColor color)
		{
			ChessPiece knight = new ChessPiece(color, ChessPieceType.Knight);
			knight.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerBackward, 2), new DirectionDistance(MoveDirection.PlayerLeft, 1) }));
			knight.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerBackward, 2), new DirectionDistance(MoveDirection.PlayerRight, 1) }));
			knight.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerBackward, 1), new DirectionDistance(MoveDirection.PlayerLeft, 2) }));
			knight.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerBackward, 1), new DirectionDistance(MoveDirection.PlayerRight, 2) }));
			knight.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerForward, 2), new DirectionDistance(MoveDirection.PlayerLeft, 1) }));
			knight.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerForward, 2), new DirectionDistance(MoveDirection.PlayerRight, 1) }));
			knight.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerForward, 1), new DirectionDistance(MoveDirection.PlayerLeft, 2) }));
			knight.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerForward, 1), new DirectionDistance(MoveDirection.PlayerRight, 2) }));
			knight.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerLeft, 2), new DirectionDistance(MoveDirection.PlayerForward, 1) }));
			knight.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerLeft, 2), new DirectionDistance(MoveDirection.PlayerBackward, 1) }));
			knight.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerLeft, 1), new DirectionDistance(MoveDirection.PlayerForward, 2) }));
			knight.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerLeft, 1), new DirectionDistance(MoveDirection.PlayerBackward, 2) }));
			knight.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerRight, 2), new DirectionDistance(MoveDirection.PlayerForward, 1) }));
			knight.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerRight, 2), new DirectionDistance(MoveDirection.PlayerBackward, 1) }));
			knight.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerRight, 1), new DirectionDistance(MoveDirection.PlayerForward, 2) }));
			knight.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerRight, 1), new DirectionDistance(MoveDirection.PlayerBackward, 2) }));

			return knight;
		}

		public static ChessPiece CreateRook(ChessPieceColor color)
		{
			ChessPiece rook = new ChessPiece(color, ChessPieceType.Rook);
			rook.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerBackward, 8) }));
			rook.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerForward, 8) }));
			rook.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerLeft, 8) }));
			rook.AddAllowedMove(new ChessMove(ChessMoveType.Both, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerRight, 8) }));

			return rook;
		}

		public static ChessPiece CreatePawn(ChessPieceColor color)
		{
			ChessPiece pawn = new ChessPiece(color, ChessPieceType.Pawn);
			pawn.AddAllowedMove(new ChessMove(ChessMoveType.Normal, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerForward, 1) }));
			pawn.AddAllowedMove(new ChessMove(ChessMoveType.Attack, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerForward, 1), new DirectionDistance(MoveDirection.PlayerLeft, 1) }));
			pawn.AddAllowedMove(new ChessMove(ChessMoveType.Attack, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerForward, 1), new DirectionDistance(MoveDirection.PlayerRight, 1) }));
			pawn.AddAllowedMove(new ChessMove(ChessMoveType.Special, new DirectionDistance[] { new DirectionDistance(MoveDirection.PlayerForward, 2) }));

			return pawn;
		}
	}
}
