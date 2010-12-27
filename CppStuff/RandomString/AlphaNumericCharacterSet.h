/*
 * DefaultCharacterSet.h
 *
 *  Created on: Dec 27, 2010
 *      Author: nalane
 */

#ifndef ALPHANUMERICCHARACTERSET_H_
#define ALPHANUMERICCHARACTERSET_H_

#include <string>
#include "ICharacterSet.h"

namespace Nathandelane
{

	class AlphaNumericCharacterSet : public Nathandelane::ICharacterSet
	{
	private:
		unsigned int _size;
		std::string _characters;
	public:
		AlphaNumericCharacterSet();
		std::string GetCharacters();
		unsigned int Size();
	};

}

#endif /* ALPHANUMERICCHARACTERSET_H_ */
