/*
 * object.cpp
 *
 *  Created on: Nov 1, 2010
 *      Author: nalane
 */

#include <iostream>
#include <string>
using namespace std;

// From C++
class Object
{
private:
	string name;
public:
	Object(const string & name);
	string getName();
};

Object::Object(const string & name)
{
	this->name = name;
}

string Object::getName()
{
	return this->name;
}

//int main()
//{
//	Object object("Object 1");
//
//	cout << "First object's name: " << object.getName() << endl;
//
//	return 0;
//}
