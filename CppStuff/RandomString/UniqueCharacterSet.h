/*
 * UniqueCharacterSet.h
 *
 *  Created on: Dec 27, 2010
 *      Author: nalane
 */

#ifndef UNIQUECHARACTERSET_H_
#define UNIQUECHARACTERSET_H_

#include <string>
#include "ICharacterSet.h"

namespace Nathandelane
{

	class UniqueCharacterSet : public Nathandelane::ICharacterSet
	{
	private:
		unsigned int _size;
		std::string _characters;
	public:
		UniqueCharacterSet(const char* characters);
		std::string GetCharacters();
		unsigned int Size();
	};

}

#endif /* UNIQUECHARACTERSET_H_ */
