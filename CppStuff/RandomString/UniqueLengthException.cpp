/*
 * UniqueLengthException.cpp
 *
 *  Created on: Dec 27, 2010
 *      Author: nalane
 */

#include <string>
#include <stdexcept>
#include "UniqueLengthException.h"

namespace Nathandelane
{

	/**
	 * Creates an instance of UniqueLengthException.
	 */
	UniqueLengthException::UniqueLengthException(const std::string &message)
		: std::runtime_error(message)
	{
	}

}
