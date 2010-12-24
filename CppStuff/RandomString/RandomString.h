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

using namespace std;

class RandomString
{
private:
	int _length;
	int _numberOfPossibleChars;
	char* _printableCharacters;

public:
	RandomString(int length, string printableCharacters);
	~RandomString();
	string NextString();
};
