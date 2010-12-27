/*
 * ICharacterSet.h
 *
 *  Created on: Dec 27, 2010
 *      Author: nalane
 */

#ifndef ICHARACTERSET_H_
#define ICHARACTERSET_H_

#include <string>

namespace Nathandelane
{

	class ICharacterSet
	{
	public:
		virtual std::string GetCharacters() = 0;
		virtual unsigned int Size() = 0;
	};

}

#endif /* ICHARACTERSET_H_ */
