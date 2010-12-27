/*
 * RandomString.cpp
 *
 *  Created on: Dec 21, 2010
 *      Author: nalane
 */

#include <stdlib.h>
#include <ctime>
#include <string>
#include <sstream>
#include <cstring>
#include <iostream>
#include <algorithm>
#include "RandomString.h"
#include "UniqueLengthException.h"

namespace Nathandelane
{

	/**
	 * Creates an instance of RandomString.
	 * @param int length
	 * @param string printableCharacters
	 * @param bool uniqueOnly
	 */
	RandomString::RandomString(int length, std::string printableCharacters, bool uniqueOnly)
	{
		srand(time(0));

		_numberOfPossibleChars = 93;
		_length = length;
		_uniqueOnly = uniqueOnly;
		_clock = clock();

		if (!printableCharacters.empty())
		{
			_numberOfPossibleChars = (printableCharacters.size() + 1);
		}
		else
		{
			printableCharacters = std::string("!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~");
		}

		_printableCharacters = printableCharacters;

		random_shuffle(_printableCharacters.begin(), _printableCharacters.end());
	}

	/**
	 * Destructor for RandomString.
	 */
	RandomString::~RandomString()
	{
		_length = 0;
		_printableCharacters = "";
	}

	/**
	 * Generates the next random string.
	 */
	std::string RandomString::NextString()
	{
		std::string retVal = "";

		srand(time(0) + (clock() - _clock));

		if (_uniqueOnly)
		{
			retVal = GenerateUniqueRandomString();
		}
		else
		{
			retVal = GenerateRandomString();
		}

		return retVal;
	}

	/**
	 * Generates a random string with possible duplicates.
	 */
	std::string RandomString::GenerateRandomString()
	{
		std::stringstream retVal;
		unsigned int charCounter = 0;

		while (charCounter <= _length)
		{
			int nextCharIndex = rand() % _numberOfPossibleChars;
			char nextChar = _printableCharacters[nextCharIndex];

			retVal << nextChar;
			charCounter++;
		}

		return retVal.str();
	}

	/**
	 * Generates a random string that contains only unique characters.
	 */
	std::string RandomString::GenerateUniqueRandomString()
	{
		std::stringstream retVal;

		if (_length > _printableCharacters.size())
		{
			std::stringstream ss;
			ss << "Requested length (" << _length << ") was greater than possible characters (" << _printableCharacters.size() << ") for unique random string.";

			std::string message = ss.str();

			throw UniqueLengthException(message.c_str());
		}

		std::string remainingPrintableCharacters = _printableCharacters;
		unsigned int charCounter = 0;
		unsigned int numberOfPossibleCharacters = remainingPrintableCharacters.size();

		while (charCounter <= _length)
		{
			int nextCharIndex = rand() % numberOfPossibleCharacters;
			char nextChar = remainingPrintableCharacters[nextCharIndex];

			retVal << nextChar;
			charCounter++;

			remainingPrintableCharacters.replace(nextCharIndex, 1, std::string(""));
			numberOfPossibleCharacters = remainingPrintableCharacters.size();
		}

		return retVal.str();
	}

}
