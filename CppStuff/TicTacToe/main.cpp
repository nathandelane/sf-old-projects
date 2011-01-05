#include "Game.h"
#include "Board.h"

void playGame()
{
	Nathandelane::Game game;
	game.play();
/*
	Nathandelane::Board board;

	board.move(Nathandelane::Human, 1);
	board.move(Nathandelane::Computer, 4);
	board.move(Nathandelane::Human, 4);
	board.move(Nathandelane::Human, 5);*/
}

int main(int argc, char* argv[])
{
	playGame();

	return 0;
}

//  g++ -o tictactoe.exe -Xlinker --enable-auto-import main.cpp Board.h Board.cpp Game.h Game.cpp