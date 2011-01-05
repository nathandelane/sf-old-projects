#include <algorithm>
#include <iostream>
#include <string>
#include <sstream>
#include "Board.h"
#include "WinningMoves.h"

namespace Nathandelane
{

	/**
	 * Creates an instance of Board.
	 */ 
	Board::Board()
	{
		std::fill_n(_board, Board::BOARD_SIZE, Nobody);
	}

	/**
	 * Creates an instance of Board.
	 */ 
	Board::Board(const Board & board)
	{
		std::copy(board._board, board._board + Board::BOARD_SIZE, _board);
	}

	/**
	 * Destroys the current instance of Board.
	 */
	Board::~Board()
	{
		std::fill_n(_board, Board::BOARD_SIZE, Nobody);
	}

	/**
	 * Places a move into a space on the board.
	 */
	bool Board::move(const Player player, const signed int squareIndex)
	{
		bool result = true;
		std::stringstream ss;

		ss << "Player " << player << " placed token on square " << squareIndex << ".";

		log(ss.str());
		
		ss.str(std::string());

		if (squareIndex <= Board::BOARD_SIZE && squareIndex > 0)
		{
			if (convertPlayerToMarker(_board[(squareIndex - 1)]) == ' ')
			{
				_board[(squareIndex - 1)] = player;
				
				str();
			}
			else
			{
				ss << "That square on the board is already occupied by '" << convertPlayerToMarker(_board[(squareIndex - 1)]) << "'.";

				log(ss.str());
				
				result = false;
			}
		}
		else
		{
			ss << "The square you have chosen exceeds the bounds of the board (size is " << Board::BOARD_SIZE << "). You chose " << squareIndex << ".";

			log(ss.str());
			
			result = false;
		}
		
		return result;
	}

	/**
	 * Writes a log message to stderr.
	 */
	void Board::log(std::string message)
	{
		std::cerr << message << std::endl;
	}

	/**
	 * Prints string representation of this board.
	 */
	void Board::str()
	{
		std::stringstream ss;

		for (int boardIndex = 0, columnIndex = 0; boardIndex < Board::BOARD_SIZE; boardIndex++)
		{			
			if (boardIndex > 0 && boardIndex < Board::BOARD_SIZE)
			{
				ss << ", ";
				
				if (columnIndex == 3)
				{
					columnIndex = 0;
					ss << std::endl;
				}
			}

			ss << (boardIndex + 1) << "[" << convertPlayerToMarker(_board[boardIndex]) << "]";

			columnIndex++;
		}

		log(ss.str());
	}
	
	/**
	 * Gets the board array.
	 */
	Player * Board::getBoardArray()
	{
		return _board;
	}

	/**
	 * Assignment operator for Board.
	 */
	Board & Board::operator=(const Board & board)
	{
		if (this != &board)
		{
			std::copy(board._board, board._board + Board::BOARD_SIZE, _board);
		}
		
		return *this;
	}
	
	/**
	 * Determines whether there is a winner.
	 */
	bool Board::isWinner(Player player)
	{
		bool winnerExists = false;
		Player boardArray[Board::BOARD_SIZE];	

		std::copy(_board, _board + Board::BOARD_SIZE, boardArray);
		
		for (int winningSetsIndex = 0; winningSetsIndex < WinningMoves::NUMBER_WINNING_MOVES; winningSetsIndex++)
		{
			if (boardArray[(WinningMoves::WINNING_MOVES[winningSetsIndex][0] - 1)] == player && boardArray[(WinningMoves::WINNING_MOVES[winningSetsIndex][1] - 1)] == player && boardArray[(WinningMoves::WINNING_MOVES[winningSetsIndex][2] - 1)] == player)
			{
				winnerExists = true;
				break;
			}
		}
		
		return winnerExists;
	}
	
	/**
	 * Converts the Player (1, or 2) to a marker (X or O).
	 */
	char Board::convertPlayerToMarker(Player player)
	{
		char result = ' ';
		
		if (player == Human)
		{
			result = 'X';
		}
		else if (player == Computer)
		{
			result = 'O';
		}
		
		return result;
	}

}