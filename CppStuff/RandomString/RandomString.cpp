/*
 * RandomString.cpp
 *
 *  Created on: Dec 21, 2010
 *      Author: nalane
 */

#include <stdlib.h>
#include <ctime>
#include <string>
#include <iostream>
#include "RandomString.h"

using namespace std;

/**
 * Creates an instance of RandomString.
 * @param int length
 * @param string printableCharacters
 */
RandomString::RandomString(int length, string printableCharacters)
{
	_numberOfPossibleChars = 93;
	_length = length;

	if (!printableCharacters.empty())
	{
		_numberOfPossibleChars = (printableCharacters.size() + 1);
	}
	else
	{
		printableCharacters = string("!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~");
	}

	_printableCharacters = new char[_numberOfPossibleChars];

	for (int charCounter = 0; charCounter < _numberOfPossibleChars; charCounter++)
	{
		_printableCharacters[charCounter] = printableCharacters[charCounter];
	}
}

/**
 * Destructor for RandomString.
 */
RandomString::~RandomString()
{
	_length = 0;
	_printableCharacters = NULL;
}

/**
 * Generates the next random string.
 */
string RandomString::NextString()
{
	string retVal = "";
	int charCounter = 0;

	srand(time(NULL));

	while (charCounter <= _length)
	{
		int nextCharIndex = rand() % _numberOfPossibleChars;
		char nextChar = _printableCharacters[nextCharIndex];

		retVal += nextChar;
		charCounter++;
	}

	return retVal;
}
