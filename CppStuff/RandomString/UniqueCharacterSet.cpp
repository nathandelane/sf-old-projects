/*
 * UniqueCharacterSet.cpp
 *
 *  Created on: Dec 27, 2010
 *      Author: nalane
 */

#include <string>
#include <algorithm>
#include "UniqueCharacterSet.h"

namespace Nathandelane
{

	/**
	 * Creates an instance of UniqueCharacterSet.
	 */
	UniqueCharacterSet::UniqueCharacterSet(const char* characters)
	{
		_characters = std::string(characters);
		_size = _characters.size();
	}

	/**
	 * Gets the characters associated with this CharacterSet.
	 */
	std::string UniqueCharacterSet::GetCharacters()
	{
		random_shuffle(_characters.begin(), _characters.end());

		return _characters;
	}

	/**
	 * Gets the number of characters in this CharacterSet.
	 */
	unsigned int UniqueCharacterSet::Size()
	{
		return _size;
	}

}
