#include <algorithm>
#include <iostream>
#include <string>
#include <sstream>

/**
 * Possible players.
 */
enum Player
{
	Human = 1,
	Computer
};

/**
 * Board representation.
 */
class Board
{
public:
	static const unsigned int BOARD_SIZE = 9;

	Board();
	~Board();
	void move(const Player player, const unsigned int squareIndex);
	void log(std::string message);
	void str();
private:
	unsigned int _board[Board::BOARD_SIZE];
};

/**
 * Creates an instance of Board.
 */ 
Board::Board()
{
	std::fill_n(this->_board, Board::BOARD_SIZE, 0);
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
void Board::move(const Player player, const unsigned int squareIndex)
{
	std::stringstream ss;

	ss << "Player " << player << " placed token on square " << (squareIndex + 1) << ".";

	log(ss.str());
	
	ss.str(std::string());

	if (squareIndex < Board::BOARD_SIZE)
	{
		if (this->_board[squareIndex] == 0)
		{
			this->_board[squareIndex] = player;
			
			str();
		}
		else
		{
			ss << "That square on the board is already occupied by " << this->_board[squareIndex] << ".";

			log(ss.str());
		}
	}
	else
	{
		ss << "The square you have chosen exceed the bounds of the board" << Board::BOARD_SIZE << ". You chose " << squareIndex << ".";

		log(ss.str());
	}
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

void playGame()
{
	Board board;

	board.move(Human, 0);
	board.move(Computer, 3);
	board.move(Human, 3);
	board.move(Human, 4);
}

int main(int argc, char* argv[])
{
	playGame();

	return 0;
}

