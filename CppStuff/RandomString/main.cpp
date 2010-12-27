/*
 * main.cpp
 *
 *  Created on: Dec 21, 2010
 *  Author: nalane
 */

#include <iostream>
#include <string>
#include <cstring>
#include <sstream>
#include <cstdlib>
#include <exception>
#include <stdexcept>
#include "RandomString.h"
#include "UniqueLengthException.h"

int main(int argc, const char* argv[])
{
	const char* HexArgValue = "hex";
	const char* HtmlFriendlyArgValue = "htmlfriendly";
	const char* UniqueOnlyArgValue = "uniqueonly";
	const char* AlphaOnlyArgValue = "alphaonly";
	const char* AlphaNumericArgValue = "alphanumeric";

	int length;
	bool printHexString = false;
	bool uniqueOnly = false;
	std::string printableCharacters = "";

	if (argc == 1)
	{
		std::cout << "Usage: RandomString NUMBER-OF-CHARS [ [CHARS-TO-USE (overrides any other set)] [" << HexArgValue << "] [" << HtmlFriendlyArgValue << "] [" << UniqueOnlyArgValue << "] [" << AlphaOnlyArgValue << "]  [" << AlphaNumericArgValue << "] ]" << std::endl << std::endl;

		return 1;
	}
	else if (argc >= 2)
	{
		std::string numberOfChars = std::string(argv[1]);
		std::stringstream ss(numberOfChars);
		ss >> length;

		if (argc >= 3)
		{
			for (int argIndex = 2; argIndex < argc; argIndex++)
			{
				if (strcmp(argv[argIndex], HexArgValue) == 0)
				{
					printHexString = true;
				}
				else if (strcmp(argv[argIndex], HtmlFriendlyArgValue) == 0 and printableCharacters.empty())
				{
					printableCharacters = "0123456789@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_`abcdefghijklmnopqrstuvwxyz{|}~";
				}
				else if (strcmp(argv[argIndex], AlphaOnlyArgValue) == 0 and printableCharacters.empty())
				{
					printableCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
				}
				else if (strcmp(argv[argIndex], AlphaNumericArgValue) == 0 and printableCharacters.empty())
				{
					printableCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
				}
				else if (strcmp(argv[argIndex], UniqueOnlyArgValue) == 0)
				{
					uniqueOnly = true;
				}
				else
				{
					if (printableCharacters.empty())
					{
						printableCharacters = std::string(argv[argIndex]);
					}
				}
			}
		}
	}

	if (length > 0)
	{
		Nathandelane::RandomString randomString(length, printableCharacters, uniqueOnly);

		try
		{
			std::string nextString = randomString.NextString();

			for (int charIndex = 0; charIndex < length; charIndex++)
			{
				char nextChar = nextString[charIndex];

				std::cout << nextChar;
			}

			if (printHexString)
			{
				for (int charIndex = 0; charIndex < length; charIndex++)
				{
					int nextChar = nextString[charIndex];

					std::cout << std::hex << nextChar;
				}
			}
		}
		catch(Nathandelane::UniqueLengthException& ex)
		{
			std::cout << ex.what() << std::endl << std::endl;
			return 1;
		}
		catch(std::exception& ex)
		{
			std::cout << "Unexpected exception occurred: " << ex.what() << std::endl << std::endl;
			return 1;
		}
	}
	else
	{
		std::cout << "Length must be greater than zero.";
	}

	std::cout << std::endl << std::endl;

	return 0;
}
