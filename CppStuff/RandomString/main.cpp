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
#include <vector>
#include "RandomString.h"
#include "UniqueLengthException.h"
#include "ICharacterSet.h"
#include "DefaultCharacterSet.h"
#include "HtmlFriendlyCharacterSet.h"
#include "AlphaCharacterSet.h"
#include "AlphaNumericCharacterSet.h"
#include "UniqueCharacterSet.h"

/**
 * Tokenizes a string.
 */
std::vector<std::string> tokenize(const std::string & str, const std::string delimiters)
{
	std::vector<std::string> result;
	std::string::size_type lastPos = str.find_first_not_of(delimiters, 0);
	std::string::size_type pos = str.find_first_of(delimiters, lastPos);

	while (std::string::npos != pos || std::string::npos != lastPos)
	{
		result.push_back(str.substr(lastPos, pos - lastPos));

		lastPos = str.find_first_not_of(delimiters, pos);
		pos = str.find_first_of(delimiters, lastPos);
	}

	return result;
}

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
	Nathandelane::ICharacterSet * characterSet = (new Nathandelane::DefaultCharacterSet());

	if (argc == 1)
	{
		std::string firstArgument(argv[0]);
		std::vector<std::string> tokens = tokenize(firstArgument, "/\\");

		std::cout << "Usage: " << tokens.back() << " NUMBER-OF-CHARS [ [CHARS-TO-USE|" << HtmlFriendlyArgValue << "|" << AlphaOnlyArgValue << "|" << AlphaNumericArgValue << " (last named character set overrides)] [" << HexArgValue << "] [" << UniqueOnlyArgValue << "] ]" << std::endl << std::endl;

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
				else if (strcmp(argv[argIndex], HtmlFriendlyArgValue) == 0)
				{
					characterSet = (new Nathandelane::HtmlFriendlyCharacterSet());
				}
				else if (strcmp(argv[argIndex], AlphaOnlyArgValue) == 0)
				{
					characterSet = (new Nathandelane::AlphaCharacterSet());
				}
				else if (strcmp(argv[argIndex], AlphaNumericArgValue) == 0)
				{
					characterSet = (new Nathandelane::AlphaNumericCharacterSet());
				}
				else if (strcmp(argv[argIndex], UniqueOnlyArgValue) == 0)
				{
					uniqueOnly = true;
				}
				else
				{
					characterSet = (new Nathandelane::UniqueCharacterSet(argv[argIndex]));
				}
			}
		}
	}

	if (length > 0)
	{
		Nathandelane::RandomString randomString(length, * characterSet, uniqueOnly);

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
				std::cout << std::endl << std::endl;

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
