/*
 * AllAsciiPrintable.cpp
 *
 *  Created on: Dec 27, 2010
 *      Author: nalane
 */

#include <string>
#include <algorithm>
#include "AllAsciiPrintable.h"

namespace Nathandelane
{

	/**
	 * Creates an instance of AllAsciiPrintable.
	 */
	AllAsciiPrintable::AllAsciiPrintable()
	{
		_characters = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
		_size = _characters.size();
	}

	/**
	 * Gets the characters associated with this CharacterSet.
	 */
	std::string AllAsciiPrintable::GetCharacters()
	{
		random_shuffle(_characters.begin(), _characters.end());

		return _characters;
	}

	/**
	 * Gets the number of characters in this CharacterSet.
	 */
	unsigned int AllAsciiPrintable::Size()
	{
		return _size;
	}

}
