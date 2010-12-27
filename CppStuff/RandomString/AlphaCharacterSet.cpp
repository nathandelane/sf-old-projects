/*
 * DefaultCharacterSet.cpp
 *
 *  Created on: Dec 27, 2010
 *      Author: nalane
 */

#include <string>
#include <algorithm>
#include "AlphaCharacterSet.h"

namespace Nathandelane
{

	/**
	 * Creates an instance of AlphaCharacterSet.
	 */
	AlphaCharacterSet::AlphaCharacterSet()
	{
		_characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		_size = _characters.size();
	}

	/**
	 * Gets the characters associated with this CharacterSet.
	 */
	std::string AlphaCharacterSet::GetCharacters()
	{
		random_shuffle(_characters.begin(), _characters.end());

		return _characters;
	}

	/**
	 * Gets the number of characters in this CharacterSet.
	 */
	unsigned int AlphaCharacterSet::Size()
	{
		return _size;
	}

}
