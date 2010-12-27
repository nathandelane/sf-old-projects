/*
 * HtmlFriendlyCharacterSet.h
 *
 *  Created on: Dec 27, 2010
 *      Author: nalane
 */

#ifndef HTMLFRIENDLYCHARACTERSET_H_
#define HTMLFRIENDLYCHARACTERSET_H_

#include <string>
#include "ICharacterSet.h"

namespace Nathandelane
{

	class HtmlFriendlyCharacterSet : public Nathandelane::ICharacterSet
	{
	private:
		unsigned int _size;
		std::string _characters;
	public:
		HtmlFriendlyCharacterSet();
		std::string GetCharacters();
		unsigned int Size();
	};

}

#endif /* HTMLFRIENDLYCHARACTERSET_H_ */
