/*
 * interface.cpp
 *
 *  Created on: Nov 2, 2010
 *      Author: nalane
 */

#include <iostream>
#include <string>
using namespace std;

/**
 * Interface IPerson.
 */
class IPerson
{
public:
	/**
	 * Gets a IPerson's first name.
	 */
	virtual string GetFirstName() = 0;

	/**
	 * Sets the IPerson's first name.
	 */
	virtual void SetFirstName(const string &) = 0;

	/**
	 * Gets the IPerson's last name.
	 */
	virtual string GetLastName() = 0;

	/**
	 * Sets the IPerson's last name.
	 */
	virtual void SetLastName(const string &) = 0;
};

/**
 * Concrete class Author.
 */
class Author : public IPerson
{
private:
	string _firstName;
	string _lastName;

public:
	Author();

	Author(const string &, const string &);

	/**
	 * Gets a IPerson's first name.
	 */
	string GetFirstName();

	/**
	 * Sets an Author's first name.
	 */
	void SetFirstName(const string &);

	/**
	 * Gets an Author's last name.
	 */
	string GetLastName();

	/**
	 * Sets an Author's last name.
	 */
	void SetLastName(const string &);
};

/**
 * Creates an instance of Author and sets first and last name to an empty string.
 */
Author::Author()
{
	_firstName = "";
	_lastName = "";
}

/**
 * Creates an instance of Author
 * @param const string & firstName
 * @param const string & lastName
 */
Author::Author(const string & firstName, const string & lastName)
{
	_firstName = firstName;
	_lastName = lastName;
}

/**
 * Gets an Author's first name.
 */
string Author::GetFirstName()
{
	return this->_firstName;
}

/**
 * Sets an Author's first name.
 */
void Author::SetFirstName(const string & firstName)
{
	this->_firstName = firstName;
}

/**
 * Gets an Author's last name.
 */
string Author::GetLastName()
{
	return this->_lastName;
}

/**
 * Sets an Author's last name.
 */
void Author::SetLastName(const string & lastName)
{
	this->_lastName = lastName;
}

/**
 * Says 'Welcome,..' to a person.
 */
void sayWelcome(IPerson & person)
{
	cout << "Welcome, " << person.GetFirstName() << " " << person.GetLastName() << endl;
}

/**
 * Program entry point.
 */
int main()
{
	IPerson * author = (new Author());

	author->SetFirstName("Nathan");
	author->SetLastName("Lane");

	sayWelcome(*author);

	return 0;
}
