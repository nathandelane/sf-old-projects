/*
 * DefaultCharacterSet.cpp
 *
 *  Created on: Dec 27, 2010
 *      Author: nalane
 */

#include <string>
#include <algorithm>
#include "AlphaNumericCharacterSet.h"

namespace Nathandelane
{

	/**
	 * Creates an instance of AlphaNumericCharacterSet.
	 */
	AlphaNumericCharacterSet::AlphaNumericCharacterSet()
	{
		_characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
		_size = _characters.size();
	}

	/**
	 * Gets the characters associated with this CharacterSet.
	 */
	std::string AlphaNumericCharacterSet::GetCharacters()
	{
		random_shuffle(_characters.begin(), _characters.end());

		return _characters;
	}

	/**
	 * Gets the number of characters in this CharacterSet.
	 */
	unsigned int AlphaNumericCharacterSet::Size()
	{
		return _size;
	}

}
