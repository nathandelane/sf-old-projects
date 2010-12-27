/*
 * DefaultCharacterSet.h
 *
 *  Created on: Dec 27, 2010
 *      Author: nalane
 */

#ifndef ALPHACHARACTERSET_H_
#define ALPHACHARACTERSET_H_

#include <string>
#include "ICharacterSet.h"

namespace Nathandelane
{

	class AlphaCharacterSet : public Nathandelane::ICharacterSet
	{
	private:
		unsigned int _size;
		std::string _characters;
	public:
		AlphaCharacterSet();
		std::string GetCharacters();
		unsigned int Size();
	};

}

#endif /* ALPHACHARACTERSET_H_ */
