/*
 * HtmlFriendlyCharaterSet.cpp
 *
 *  Created on: Dec 27, 2010
 *      Author: nalane
 */

#include <string>
#include <algorithm>
#include "HtmlFriendlyCharacterSet.h"

namespace Nathandelane
{
	/**
	 * Creates an instance of HtmlFriendlyCharacterSet.
	 */
	HtmlFriendlyCharacterSet::HtmlFriendlyCharacterSet()
	{
		_characters = "0123456789@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_`abcdefghijklmnopqrstuvwxyz{|}~";
		_size = _characters.size();
	}

	/**
	 * Gets the available characters for an HTML-friendly character set.
	 */
	std::string HtmlFriendlyCharacterSet::GetCharacters()
	{
		random_shuffle(_characters.begin(), _characters.end());

		return _characters;
	}

	/**
	 * Gets the number of characters in the character set.
	 */
	unsigned int HtmlFriendlyCharacterSet::Size()
	{
		return _size;
	}

}
