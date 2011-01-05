#include <string>
#include <iostream>
#include <sstream>
#include <algorithm>
#include "Game.h"

namespace Nathandelane
{

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
		_board = board;
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
				if (_board.move(currentPlayer, squareIndex))
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
				else if (inputTokens.str().compare("p") == 0 || inputTokens.str().compare("P") == 0)
				{
					_board.str();
				}
				else if (inputTokens.str().compare("?") == 0)
				{
					std::cout << "Valid input is: q|Q (quit), p|P (print board), 1-9 (place token on board in numbered space)" << std::endl;
				}
				else
				{
					std::stringstream ss;
					ss << "Your input was not valid. 1-9 are valid inputs.";
					
					log(ss.str());
				}
			}
			
			inputTokens.str(std::string());
			
			if (std::count(_board.getBoardArray(), _board.getBoardArray() + Board::BOARD_SIZE, Human) >= Board::WINNER_STRING_LENGTH_REQUIREMENT || std::count(_board.getBoardArray(), _board.getBoardArray() + Board::BOARD_SIZE, Computer) >= Board::WINNER_STRING_LENGTH_REQUIREMENT)
			{
				if (_board.isWinner(Human) || _board.isWinner(Computer))
				{
					std::cout << "Player " << Human << " has won this round. Would you like to play again? [y/n]";
					std::cin >> userInput;
					
					if (userInput.compare("n") != 0 && userInput.compare("N") != 0)
					{
						_board = Board();
					}
					else
					{
						std::cout << "Thanks for playing!" << std::endl;
						break;
					}
				}
			}
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

}