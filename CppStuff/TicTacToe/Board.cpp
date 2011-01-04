#include <algorithm>
#include <iostream>
#include <string>
#include <sstream>
#include "Board.h"

/**
 * Creates an instance of Board.
 */ 
Board::Board()
{
	std::fill_n(this->_board, Board::BOARD_SIZE, 0);
}

/**
 * Creates an instance of Board.
 */ 
Board::Board(const Board & board)
{
	std::copy(board._board, board._board + Board::BOARD_SIZE, this->_board);
}

/**
 * Destroys the current instance of Board.
 */
Board::~Board()
{
	std::fill_n(this->_board, Board::BOARD_SIZE, 0);
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

	if (squareIndex < Board::BOARD_SIZE && squareIndex >= 0)
	{
		if (this->_board[(squareIndex - 1)] == 0)
		{
			this->_board[(squareIndex - 1)] = player;
			
			str();
		}
		else
		{
			ss << "That square on the board is already occupied by " << this->_board[squareIndex] << ".";

			log(ss.str());
			
			result = false;
		}
	}
	else
	{
		ss << "The square you have chosen exceed the bounds of the board (size is " << Board::BOARD_SIZE << "). You chose " << squareIndex << ".";

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

	for (int boardIndex = 0; boardIndex < Board::BOARD_SIZE; boardIndex++)
	{
		if (boardIndex > 0 && boardIndex < Board::BOARD_SIZE)
		{
			ss << ", ";
		}

		ss << this->_board[boardIndex];
	}

	log(ss.str());
}

/**
 * Assignment operator for Board.
 */
Board & Board::operator=(const Board & board)
{
	if (this != &board)
	{
		std::copy(board._board, board._board + Board::BOARD_SIZE, this->_board);
	}
	
	return *this;
}
