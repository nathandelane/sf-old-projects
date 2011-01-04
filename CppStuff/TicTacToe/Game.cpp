#include <string>
#include <iostream>
#include <sstream>
#include "Game.h"

/**
 * Creates an instance of Game.
 */
Game::Game()
{
}

/**
 * Creates an instance of Game.
 */
Game::Game(Board board)
{
	this->_board = board;
}

/**
 * Destroys this Game object.
 */
Game::~Game()
{
}

/**
 * Plays this Game.
 */
void Game::play()
{
	Player currentPlayer = Human;
	signed int squareIndex = -1;
	
	std::string userInput;
	
	while (true)
	{
		std::cout << "Enter the square you would like to place your marker on (1-9) player " << currentPlayer << ": ";
		std::cin >> userInput;
		
		std::stringstream inputTokens(userInput);
		
		if (inputTokens >> squareIndex)
		{
			if (this->_board.move(currentPlayer, squareIndex))
			{
				currentPlayer = swapPlayers(currentPlayer);
			}
		}
		else
		{
			if (inputTokens.str().compare("q") == 0 || inputTokens.str().compare("Q") == 0)
			{
				break;
			}
			
			std::stringstream ss;
			ss << "Your input was not valid. 1-9 are valid inputs.";
			
			log(ss.str());
		}
		
		inputTokens.str(std::string());
	}
}

/**
 * Writes a log message to stderr.
 */
void Game::log(std::string message)
{
	std::cerr << message << std::endl;
}

/**
 * Changes which player's turn is up.
 */
Player Game::swapPlayers(Player currentPlayer)
{
	if (currentPlayer == Human)
	{
		currentPlayer = Computer;
	}
	else
	{
		currentPlayer = Human;
	}
	
	return currentPlayer;
}
