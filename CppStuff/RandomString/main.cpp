/*
 * main.cpp
 *
 *  Created on: Dec 21, 2010
 *  Author: nalane
 */

#include <iostream>
#include <sstream>
#include <string>
#include <cstdlib>
#include "RandomString.h"

using namespace std;

int main(int argc, char* argv[])
{
	int length;
	string printableCharacters = "";

	if (argc == 1)
	{
		cout << "Usage: RandomString NUMBER-OF-CHARS [CHARS-TO-USE]" << endl << endl;

		return 1;
	}
	else if (argc >= 2)
	{
		string numberOfChars = string(argv[1]);
		stringstream ss(numberOfChars);
		ss >> length;

		if (argc >= 3)
		{
			printableCharacters = string(argv[2]);
		}
	}

	RandomString randomString(length, printableCharacters);
	string nextString = randomString.NextString();

	for (int charIndex = 0; charIndex < length; charIndex++)
	{
		char nextChar = nextString[charIndex];

		cout << nextChar;
	}

	cout << endl << endl;

	for (int charIndex = 0; charIndex < length; charIndex++)
	{
		int nextChar = nextString[charIndex];

		cout << hex << nextChar;
	}

	cout << endl << endl;

	return 0;
}
