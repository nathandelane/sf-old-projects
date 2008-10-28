using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Nathandelane.IO.Chess
{
	public class Game
	{
		private ChessBoard _chessBoard;
		private Player[] _players;
		private string[] _colors;
		private PlayerStatus _status;

		public Game()
		{
			_chessBoard = new ChessBoard();
			_players = new Player[2];
			_colors = new string[] { "Light", "Dark" };
			_status = PlayerStatus.Normal;
		}

		public Game(ChessBoard board)
		{
			_chessBoard = board;
			_players = new Player[2];
			_colors = new string[] { "Light", "Dark" };
			_status = PlayerStatus.Normal;
		}

		public void Start()
		{
			int currentPlayer = 0;

			if (GetPlayers())
			{
				Console.WriteLine("Let the Chess game begin...\n");

				_chessBoard.Render();

				while (_players[0].Status != PlayerStatus.CheckMate && _players[1].Status != PlayerStatus.CheckMate)
				{
					while (_players[0].Status != PlayerStatus.Check && _players[1].Status != PlayerStatus.Check)
					{
						if (GetPlayerInput(currentPlayer))
						{
							switch (currentPlayer)
							{
								case 0:
									currentPlayer = 1;
									break;
								case 1:
									currentPlayer = 0;
									break;
							}
						}
					}

					if (_players[0].Status == PlayerStatus.Check)
					{
						Console.WriteLine("The Light player is in Check. Please move out of Check.");
					}
					else if (_players[1].Status == PlayerStatus.Check)
					{
						Console.WriteLine("The Dark player is in Check. Please move out of Check.");
					}
					else if (_players[0].Status == PlayerStatus.CheckMate)
					{
						Console.WriteLine("The Light player is in CheckMate. The game is over.");
						Console.WriteLine("Press any key to continue...");
						Console.ReadKey();
					}
					else if (_players[1].Status == PlayerStatus.CheckMate)
					{
						Console.WriteLine("The Dark player is in CheckMate. The game is over.");
						Console.WriteLine("Press any key to continue...");
						Console.ReadKey();
					}
				}
			}
		}

		private bool GetPlayerInput(int player)
		{
			bool result = true;

			string userInput = String.Empty;

			do
			{
				Console.WriteLine("{0}, please tell me where you want to move.", _colors[player]);
				Console.Write("> ");

				userInput = Console.ReadLine();
			}
			while (!Validate(userInput));

			ChessMove move = UserInputToChessMove(userInput);

			return result;
		}

		private ChessMove UserInputToChessMove(string userInput)
		{
			ChessMove move = null;

			// Convert the input into a move object

			return move;
		}

		private bool Validate(string userInput)
		{
			bool result = false;
			Regex regex = new Regex("(Ra|Rad|Ral|kb|kbd|kbl|Bc|Bcd|Bcl|Qd|Qdd|Qdl|Ke|Ked|Kel|Bf|Bfd|Bfl|kg|kgd|kgl|Rh|Rhd|Rhl|Pa|Pad|Pal|Pb|Pbd|Pbl|Pc|Pcd|Pcl|Pd|Pdd|Pdl|Pe|Ped|Pel|Pf|Pfd|Pfl|Pg|Pgd|Pgl|Ph|Phd|Phl){1}\\s(to){1}\\s[a-h1-8]{1}");

			if (String.IsNullOrEmpty(userInput))
			{
				Console.WriteLine("Sorry an empty command is not valid.");
			}
			else if (userInput.ToLower().StartsWith("quit") || userInput.ToLower().StartsWith("q") || userInput.ToLower().StartsWith("exit") || userInput.ToLower().StartsWith("x"))
			{
				Console.WriteLine("User quit game.\nPress any key to continue...");
				Console.ReadKey();
				Environment.Exit(1);
			}
			else if (userInput.ToLower().StartsWith("help") || userInput.ToLower().StartsWith("h") || userInput.ToLower().StartsWith("?"))
			{
				DisplayHelp();
			}
			else if (regex.IsMatch(userInput))
			{
				Console.WriteLine("...Parsing input...");
				result = true;
			}
			else
			{
				Console.WriteLine("Cannot understand `{0}` command.", userInput);
			}

			return result;
		}

		private void DisplayHelp()
		{
			string helpMessage =
				"Chess HELP\n" +
				"----------\n" +
				"Commands must be entered as ChessPiece to SpaceCoordinates. This will help\n" +
				"the interpreter to understand what you are trying to do. You may exclude 'd'\n" +
				"and 'l' or include them as you'd like in the ChessPiece designation.\n" +
				"SpaceCoordinates must be in the form columnRow.\n";
			Console.WriteLine(helpMessage);
		}

		private bool GetPlayers()
		{
			bool result = false;

			for(int i = 0; i < 2; i++)
			{
				string userInput = String.Empty;
				ChessPieceColor color = (i == 0) ? ChessPieceColor.Light : ChessPieceColor.Dark;

				do
				{
					Console.Write("Please choose the player type for the {0} player (Computer, Human, Quit): ", _colors[i]);

					userInput = Console.ReadLine().Substring(0, 1);
				}
				while (String.IsNullOrEmpty(userInput));

				if (userInput.Equals("c"))
				{
					_players[i] = new Player(PlayerType.Computer, color);
					result = true;
				}
				else if (userInput.Equals("h"))
				{
					_players[i] = new Player(PlayerType.Human, color);
					result = true;
				}
				else if (userInput.Equals("q"))
				{
					result = false;
					break;
				}
				else
				{
					Console.WriteLine("I did not understand your response.");
				}
			}

			return result;
		}
	}
}
