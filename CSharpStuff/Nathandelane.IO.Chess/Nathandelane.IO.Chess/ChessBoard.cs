using System;
using System.Collections.Generic;
using System.Text;

namespace Nathandelane.IO.Chess
{
	public class ChessBoard
	{
		public static readonly int ChessBoardWidth = 8;
		public static readonly int ChessBoardHeight = 8;
		public static readonly string[] Columns = new string[] { "a", "b", "c", "d", "e", "f", "g", "h" };

		private List<ChessPiece> _chessBoard;
		private Dictionary<ChessPieceColor, List<ChessPiece>> _piecesLost;

		public ChessBoard()
		{
			_chessBoard = new List<ChessPiece>();
			_piecesLost = new Dictionary<ChessPieceColor, List<ChessPiece>>();

			InitializePieces();
		}

		public ChessPiece GetPieceAt(int index)
		{
			ChessPiece piece = _chessBoard[index];

			return piece;
		}

		public bool MoveIsOnBoard(ChessMove move)
		{
			bool result = false;

			if (move.ToIndex() < _chessBoard.Count && move.ToIndex() > -1)
			{
				result = true;
			}

			return result;
		}

		public bool PieceIsInPathOf(ChessPiece piece, ChessMove move)
		{
			bool result = false;

			if (piece.Type != ChessPieceType.Knight)
			{
				List<ChessMove> granules = move.Granularize();

				foreach (ChessMove nextMove in granules)
				{
					int distance = nextMove.ToIndex();
					int currentLocation = _chessBoard.IndexOf(piece);

					if (GetPieceAt(currentLocation + distance).Color == piece.Color)
					{
						result = true;
					}
					else if (GetPieceAt(currentLocation + distance).Color != piece.Color)
					{
						result = true;
					}
				}
			}

			return result;
		}

		public ChessPiece MovePiece(Player player, ChessPiece chessPiece, ChessMove move)
		{
			ChessPiece result = null;

			if ((chessPiece.MoveIsAllowed(move) && MoveIsOnBoard(move) && !PieceIsInPathOf(chessPiece, move)) && ((move.MoveType == ChessMoveType.Special && chessPiece.NumberOfMoves == 0) || (move.MoveType != ChessMoveType.Special && chessPiece.NumberOfMoves > 0)))
			{
				int currentLocation = _chessBoard.IndexOf(chessPiece);
				int newLocation = currentLocation + ((chessPiece.Color == ChessPieceColor.Dark) ? move.ToIndex() : move.ToIndex() * -1);

				if (GetPieceAt(newLocation) == null)
				{
					_chessBoard[newLocation] = chessPiece;
					_chessBoard[currentLocation] = null;
				}
				else if (GetPieceAt(newLocation).Color == chessPiece.Color)
				{
					throw new Exception("Cannot move onto a space occupied by your own pieces.");
				}
				else if (GetPieceAt(newLocation).Color != chessPiece.Color)
				{
					string message = String.Format("You captured the {0} {1} piece.", ChessPieceColorToString(GetPieceAt(newLocation).Color), ChessPieceTypeToString(GetPieceAt(newLocation).Type));
					result = GetPieceAt(newLocation);
					_chessBoard[newLocation] = chessPiece;
					_chessBoard[currentLocation] = null;
				}
			}
			else if (!MoveIsOnBoard(move))
			{
				throw new Exception("The move you are trying to make is not on the defined board.");
			}
			else if (PieceIsInPathOf(chessPiece, move))
			{
				throw new Exception("There is a piece in your way.");
			}

			return result;
		}

		public void Render()
		{
			int counter = 0;
			int row = 8;

			Console.Write("\n:Dark:\n\n    a   b   c   d   e   f   g   h\n\n{0} |", row);

			foreach (ChessPiece piece in _chessBoard)
			{				
				if (counter == 8)
				{
					Console.WriteLine();
					counter = 0;
					row--;
					Console.Write("{0} ", row);
					Console.Write("|");
				}

				if (piece != null)
				{
					string color = (piece.Color == ChessPieceColor.Dark) ? "d" : "l";

					switch (piece.Type)
					{
						case ChessPieceType.Bishop:
							Console.Write("B{0}{1}|", ChessBoard.Columns[counter], color);
							break;
						case ChessPieceType.King:
							Console.Write("K{0}{1}|", ChessBoard.Columns[counter], color);
							break;
						case ChessPieceType.Knight:
							Console.Write("k{0}{1}|", ChessBoard.Columns[counter], color);
							break;
						case ChessPieceType.Pawn:
							Console.Write("P{0}{1}|", ChessBoard.Columns[counter], color);
							break;
						case ChessPieceType.Queen:
							Console.Write("Q{0}{1}|", ChessBoard.Columns[counter], color);
							break;
						case ChessPieceType.Rook:
							Console.Write("R{0}{1}|", ChessBoard.Columns[counter], color);
							break;
					}
				}
				else
				{
					Console.Write("___|");
				}

				counter++;
			}

			Console.WriteLine("\n\n:Light:\n");
		}

		private void InitializePieces()
		{
			// First row, dark
			_chessBoard.Add(ChessPiece.CreateRook(ChessPieceColor.Dark));
			_chessBoard.Add(ChessPiece.CreateKnight(ChessPieceColor.Dark));
			_chessBoard.Add(ChessPiece.CreateBishop(ChessPieceColor.Dark));
			_chessBoard.Add(ChessPiece.CreateQueen(ChessPieceColor.Dark));
			_chessBoard.Add(ChessPiece.CreateKing(ChessPieceColor.Dark));
			_chessBoard.Add(ChessPiece.CreateBishop(ChessPieceColor.Dark));
			_chessBoard.Add(ChessPiece.CreateKnight(ChessPieceColor.Dark));
			_chessBoard.Add(ChessPiece.CreateRook(ChessPieceColor.Dark));
			// Second row, dark
			_chessBoard.Add(ChessPiece.CreatePawn(ChessPieceColor.Dark));
			_chessBoard.Add(ChessPiece.CreatePawn(ChessPieceColor.Dark));
			_chessBoard.Add(ChessPiece.CreatePawn(ChessPieceColor.Dark));
			_chessBoard.Add(ChessPiece.CreatePawn(ChessPieceColor.Dark));
			_chessBoard.Add(ChessPiece.CreatePawn(ChessPieceColor.Dark));
			_chessBoard.Add(ChessPiece.CreatePawn(ChessPieceColor.Dark));
			_chessBoard.Add(ChessPiece.CreatePawn(ChessPieceColor.Dark));
			_chessBoard.Add(ChessPiece.CreatePawn(ChessPieceColor.Dark));
			// Four rows of empty spaces
			for (int i = 0; i < (4 * ChessBoard.ChessBoardWidth); i++)
			{
				_chessBoard.Add(null);
			}
			// Second row, light
			_chessBoard.Add(ChessPiece.CreatePawn(ChessPieceColor.Light));
			_chessBoard.Add(ChessPiece.CreatePawn(ChessPieceColor.Light));
			_chessBoard.Add(ChessPiece.CreatePawn(ChessPieceColor.Light));
			_chessBoard.Add(ChessPiece.CreatePawn(ChessPieceColor.Light));
			_chessBoard.Add(ChessPiece.CreatePawn(ChessPieceColor.Light));
			_chessBoard.Add(ChessPiece.CreatePawn(ChessPieceColor.Light));
			_chessBoard.Add(ChessPiece.CreatePawn(ChessPieceColor.Light));
			_chessBoard.Add(ChessPiece.CreatePawn(ChessPieceColor.Light));
			// First row, light
			_chessBoard.Add(ChessPiece.CreateRook(ChessPieceColor.Light));
			_chessBoard.Add(ChessPiece.CreateKnight(ChessPieceColor.Light));
			_chessBoard.Add(ChessPiece.CreateBishop(ChessPieceColor.Light));
			_chessBoard.Add(ChessPiece.CreateQueen(ChessPieceColor.Light));
			_chessBoard.Add(ChessPiece.CreateKing(ChessPieceColor.Light));
			_chessBoard.Add(ChessPiece.CreateBishop(ChessPieceColor.Light));
			_chessBoard.Add(ChessPiece.CreateKnight(ChessPieceColor.Light));
			_chessBoard.Add(ChessPiece.CreateRook(ChessPieceColor.Light));
		}

		private string ChessPieceColorToString(ChessPieceColor color)
		{
			string result = String.Empty;

			switch (color)
			{
				case ChessPieceColor.Dark:
					result = "Dark";
					break;
				case ChessPieceColor.Light:
					result = "Light";
					break;
			}

			return result;
		}

		private string ChessPieceTypeToString(ChessPieceType type)
		{
			string result = String.Empty;

			switch (type)
			{
				case ChessPieceType.Bishop:
					result = "DarBishop";
					break;
				case ChessPieceType.King:
					result = "King";
					break;
				case ChessPieceType.Knight:
					result = "Knight";
					break;
				case ChessPieceType.Pawn:
					result = "Pawn";
					break;
				case ChessPieceType.Queen:
					result = "Queen";
					break;
				case ChessPieceType.Rook:
					result = "Rook";
					break;
			}

			return result;
		}
	}
}
