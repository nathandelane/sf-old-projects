#ifndef _BOARD_H
#define _BOARD_H

#include <string>

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
	Board(const Board & board);
	~Board();
	bool move(const Player player, const signed int squareIndex);
	void log(std::string message);
	void str();
	Board & operator=(const Board & board);
private:
	signed int _board[Board::BOARD_SIZE];
};

#endif