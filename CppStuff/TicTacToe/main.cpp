#include "Game.h"

void playGame()
{
	Game game;
	game.play();

//	Board board;

//	board.move(Human, 0);
//	board.move(Computer, 3);
//	board.move(Human, 3);
//	board.move(Human, 4);
}

int main(int argc, char* argv[])
{
	playGame();

	return 0;
}

//  g++ -o tictactoe.exe -Xlinker --enable-auto-import main.cpp Board.h Board.cpp Game.h Game.cpp