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
#include "AllAsciiPrintable.h"
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
	const std::string HexArgValue("hex");
	const std::string HtmlFriendlyArgValue("htmlfriendly");
	const std::string UniqueOnlyArgValue("uniqueonly");
	const std::string AlphaOnlyArgValue("alphaonly");
	const std::string AlphaNumericArgValue("alphanumeric");
	const std::string AllAsciiPrintableValue("allasciiprintable");
	const std::string NumStringsValue("numstrings");

	int length;
	int numStrings = 1;
	bool printHexString = false;
	bool uniqueOnly = false;
	Nathandelane::ICharacterSet * characterSet = (new Nathandelane::DefaultCharacterSet());

	if (argc == 1)
	{
		std::string firstArgument(argv[0]);
		std::vector<std::string> tokens = tokenize(firstArgument, "/\\");

		std::cout << "Usage: " << tokens.back() << " NUMBER-OF-CHARS [" << NumStringsValue << "=<number of strings>] [ [CHARS-TO-USE|" << HtmlFriendlyArgValue << "|" << AlphaOnlyArgValue << "|" << AlphaNumericArgValue << "|" << AllAsciiPrintableValue << " (last named character set overrides)] [" << HexArgValue << "] [" << UniqueOnlyArgValue << "] ]" << std::endl << std::endl;

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
				std::string nextArgument(argv[argIndex]);

				if (nextArgument.compare(HexArgValue) == 0)
				{
					printHexString = true;
				}
				else if (nextArgument.find_first_of("=") != std::string::npos)
				{
					std::vector<std::string> nextArgTokens = tokenize(nextArgument, "=");

					if (nextArgTokens.size() == 2)
					{
						if ((nextArgTokens.front()).compare(NumStringsValue) == 0)
						{
							std::string numberOfStrings = nextArgTokens.back();
							std::stringstream numStringsStream(numberOfStrings);

							if (!(numStringsStream >> numStrings))
							{
								std::cout << NumStringsValue << " value must be an integer greater than 0." << std::endl << std::endl;
								return 1;
							}
						}
						else
						{
							std::cout << nextArgTokens.front() << "= is not recognized as a valid argument." << std::endl << std::endl;
							return 2;
						}
					}
					else
					{
						std::cout << "No rvalue found for argument " << nextArgTokens.front() << "=" << std::endl << std::endl;
						return 3;
					}
				}
				else if (nextArgument.compare(HtmlFriendlyArgValue) == 0)
				{
					characterSet = (new Nathandelane::HtmlFriendlyCharacterSet());
				}
				else if (nextArgument.compare(AlphaOnlyArgValue) == 0)
				{
					characterSet = (new Nathandelane::AlphaCharacterSet());
				}
				else if (nextArgument.compare(AlphaNumericArgValue) == 0)
				{
					characterSet = (new Nathandelane::AlphaNumericCharacterSet());
				}
				else if (nextArgument.compare(UniqueOnlyArgValue) == 0)
				{
					uniqueOnly = true;
				}
				else if (nextArgument.compare(AllAsciiPrintableValue) == 0)
				{
					characterSet = (new Nathandelane::AllAsciiPrintable());
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
			for (int stringCounter = 0; stringCounter < numStrings; stringCounter++)
			{
				std::string nextString = randomString.NextString();

				for (int charIndex = 0; charIndex < length; charIndex++)
				{
					char nextChar = nextString[charIndex];

					std::cout << nextChar;
				}

				std::cout << std::endl;

				if (printHexString)
				{
					for (int charIndex = 0; charIndex < length; charIndex++)
					{
						int nextChar = nextString[charIndex];

						std::cout << std::hex << nextChar;
					}

					std::cout << std::endl;
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
