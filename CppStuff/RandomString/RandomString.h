/*
 * RandomString.h
 *
 *  Created on: Dec 21, 2010
 *  Author: nalane
 */

#ifndef RANDOMSTRING_H_
#define RANDOMSTRING_H_


#endif /* RANDOMSTRING_H_ */

#include <string>
#include <ctime>

namespace Nathandelane
{

	class RandomString
	{
	private:
		unsigned int _length;
		unsigned int _numberOfPossibleChars;
		std::string _printableCharacters;
		bool _uniqueOnly;
		clock_t _clock;

		std::string GenerateRandomString();
		std::string GenerateUniqueRandomString();

	public:
		RandomString(int length, std::string printableCharacters, bool uniqueOnly);
		~RandomString();
		std::string NextString();
	};

}
