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
#include "ICharacterSet.h"
#include "DefaultCharacterSet.h"
#include "HtmlFriendlyCharacterSet.h"
#include "AlphaCharacterSet.h"
#include "AlphaNumericCharacterSet.h"

namespace Nathandelane
{

	/**
	 * Creates an instance of RandomString.
	 * @param int length
	 * @param string printableCharacters
	 * @param bool uniqueOnly
	 */
	RandomString::RandomString(int length, Nathandelane::ICharacterSet & characterSet, bool uniqueOnly)
	{
		srand(time(0));

		_length = length;
		_characterSet = &characterSet;
		_uniqueOnly = uniqueOnly;
		_clock = clock();
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
		std::string possibleCharacters = (* _characterSet).GetCharacters();
		unsigned int charCounter = 0;

		while (charCounter <= _length)
		{
			unsigned int nextCharIndex = rand() % (* _characterSet).Size();
			char nextChar = possibleCharacters[nextCharIndex];

			retVal << nextChar;
			charCounter++;

			random_shuffle(possibleCharacters.begin(), possibleCharacters.end());
		}

		return retVal.str();
	}

	/**
	 * Generates a random string that contains only unique characters.
	 */
	std::string RandomString::GenerateUniqueRandomString()
	{
		std::stringstream retVal;

		if (_length > (* _characterSet).Size())
		{
			std::stringstream ss;
			ss << "Requested length (" << _length << ") was greater than possible characters (" << (* _characterSet).Size() << ") for unique random string.";

			std::string message = ss.str();

			throw UniqueLengthException(message.c_str());
		}

		std::string remainingPrintableCharacters = (* _characterSet).GetCharacters();
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

			random_shuffle(remainingPrintableCharacters.begin(), remainingPrintableCharacters.end());
		}

		return retVal.str();
	}

	/**
	 * Cleans up memory in use by RandomString.
	 */
	RandomString::~RandomString()
	{
		delete(_characterSet);
	}

}
