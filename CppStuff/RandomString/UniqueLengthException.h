/*
 * UniqueLengthException.h
 *
 *  Created on: Dec 27, 2010
 *      Author: nalane
 */

#ifndef UNIQUELENGTHEXCEPTION_H_
#define UNIQUELENGTHEXCEPTION_H_

#include <string>
#include <stdexcept>

namespace Nathandelane
{

	class UniqueLengthException : public std::runtime_error
	{
	public:
		UniqueLengthException(const std::string &message);
	};

}

#endif /* UNIQUELENGTHEXCEPTION_H_ */
