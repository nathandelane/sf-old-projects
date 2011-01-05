#ifndef _BOARD_H
#define _BOARD_H

#include <string>

namespace Nathandelane
{

	/**
	 * Possible players.
	 */
	enum Player
	{
		Human = 1,
		Computer,
		Nobody
	};

	/**
	 * Board representation.
	 */
	class Board
	{
	public:
		static const unsigned int BOARD_SIZE = 9;
		static const unsigned int BOARD_WIDTH = 3;
		static const unsigned int BOARD_HEIGHT = 3;
		static const unsigned int WINNER_STRING_LENGTH_REQUIREMENT = 3;

		Board();
		Board(const Board & board);
		~Board();
		bool move(const Player player, const signed int squareIndex);
		void log(std::string message);
		void str();
		Player * getBoardArray();
		Board & operator=(const Board & board);
		bool isWinner(Player player);
	private:
		Player _board[Board::BOARD_SIZE];
		
		char convertPlayerToMarker(Player player);
	};

}

#endif