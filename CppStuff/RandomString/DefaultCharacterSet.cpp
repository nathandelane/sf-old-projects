/*
 * DefaultCharacterSet.cpp
 *
 *  Created on: Dec 27, 2010
 *      Author: nalane
 */

#include <string>
#include <algorithm>
#include "DefaultCharacterSet.h"

namespace Nathandelane
{

	/**
	 * Creates an instance of DefaultCharacterSet.
	 */
	DefaultCharacterSet::DefaultCharacterSet()
	{
		_characters = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
		_size = _characters.size();
	}

	/**
	 * Gets the characters associated with this CharacterSet.
	 */
	std::string DefaultCharacterSet::GetCharacters()
	{
		random_shuffle(_characters.begin(), _characters.end());

		return _characters;
	}

	/**
	 * Gets the number of characters in this CharacterSet.
	 */
	unsigned int DefaultCharacterSet::Size()
	{
		return _size;
	}

}
