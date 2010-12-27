/*
 * RandomString.h
 *
 *  Created on: Dec 21, 2010
 *  Author: nalane
 */

#ifndef RANDOMSTRING_H_
#define RANDOMSTRING_H_

#include <string>
#include <ctime>
#include "ICharacterSet.h"

namespace Nathandelane
{

	class RandomString
	{
	private:
		unsigned int _length;
		Nathandelane::ICharacterSet * _characterSet;
		bool _uniqueOnly;
		clock_t _clock;

		std::string GenerateRandomString();
		std::string GenerateUniqueRandomString();

	public:
		RandomString(int length, Nathandelane::ICharacterSet & characterSet, bool uniqueOnly);
		~RandomString();
		std::string NextString();
	};

}

#endif /* RANDOMSTRING_H_ */
