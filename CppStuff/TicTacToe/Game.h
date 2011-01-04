#ifndef _GAME_H
#define _GAME_H

#include "Board.h"

class Game
{
private:
	Board _board;
	Player swapPlayers(Player currentPlayer);
	
public:
	Game();
	Game(Board board);
	~Game();
	void play();
	void log(std::string message);
};

#endif