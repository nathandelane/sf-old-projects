<?php

/**
 * ArgumentTypeValidator
 * Simplified extension class useful for validating arguments.
 * @author lanathan
 *
 */
class ArgumentTypeValidator {
	
	const STRING = 1;
	const INTEGER = 2;
	const DOUBLE = 3;
	const LONG = 4;
	const NUMERIC = 5;
	const BOOL = 6;
	const OBJECT = 7;
	const REAL = 8;
	const RESOURCE = 9;
	const SCALAR = 10;
	const T_ARRAY = 11;
	const FLOAT = 12;
	
	/**
	 * isString
	 * Validates whether an argument is a string and throws an exception if it isn't.
	 * @param mixed $arg
	 * @param string $exceptionMessage
	 */
	public static function isString(/*mixed*/ $arg, /*string*/ $exceptionMessage) {
		$result = self::_validate(ArgumentTypeValidator::STRING, $arg, $exceptionMessage);
		
		return $result;
	}
	
	/**
	 * isInteger
	 * Validates whether an argument is an integer and throws an exception if it isn't.
	 * @param mixed $arg
	 * @param string $exceptionMessage
	 */
	public static function isInteger(/*mixed*/ $arg, /*string*/ $exceptionMessage) {
		$result = self::_validate(ArgumentTypeValidator::INTEGER, $arg, $exceptionMessage);
				
		return $result;
	}
		
	/**
	 * isDouble
	 * Validates whether an argument is a double and throws an exception if it isn't.
	 * @param mixed $arg
	 * @param string $exceptionMessage
	 */
	public static function isDouble(/*mixed*/ $arg, /*string*/ $exceptionMessage) {
		$result = self::_validate(ArgumentTypeValidator::DOUBLE, $arg, $exceptionMessage);
				
		return $result;
	}
	
	/**
	 * isBool
	 * Validates whether an argument is a bool and throws an exception if it isn't.
	 * @param mixed $arg
	 * @param string $exceptionMessage
	 */
	public static function isBool(/*mixed*/ $arg, /*string*/ $exceptionMessage) {
		$result = self::_validate(ArgumentTypeValidator::BOOL, $arg, $exceptionMessage);
				
		return $result;
	}
	
	/**
	 * isFloat
	 * Validates whether an argument is a float and throws an exception if it isn't.
	 * @param mixed $arg
	 * @param string $exceptionMessage
	 */
	public static function isFloat(/*mixed*/ $arg, /*string*/ $exceptionMessage) {
		$result = self::_validate(ArgumentTypeValidator::FLOAT, $arg, $exceptionMessage);
				
		return $result;
	}
	
	/**
	 * isLong
	 * Validates whether an argument is a long and throws an exception if it isn't.
	 * @param mixed $arg
	 * @param string $exceptionMessage
	 */
	public static function isLong(/*mixed*/ $arg, /*string*/ $exceptionMessage) {
		$result = self::_validate(ArgumentTypeValidator::LONG, $arg, $exceptionMessage);
				
		return $result;
	}
	
	/**
	 * isNumeric
	 * Validates whether an argument is numeric and throws an exception if it isn't.
	 * @param mixed $arg
	 * @param string $exceptionMessage
	 */
	public static function isNumeric(/*mixed*/ $arg, /*string*/ $exceptionMessage) {
		$result = self::_validate(ArgumentTypeValidator::NUMERIC, $arg, $exceptionMessage);
				
		return $result;
	}
	
	/**
	 * isObject
	 * Validates whether an argument is an object and throws an exception if it isn't.
	 * @param mixed $arg
	 * @param string $exceptionMessage
	 */
	public static function isObject(/*mixed*/ $arg, /*string*/ $exceptionMessage) {
		$result = self::_validate(ArgumentTypeValidator::OBJECT, $arg, $exceptionMessage);
				
		return $result;
	}
	
	/**
	 * isReal
	 * Validates whether an argument is a real and throws an exception if it isn't.
	 * @param mixed $arg
	 * @param string $exceptionMessage
	 */
	public static function isReal(/*mixed*/ $arg, /*string*/ $exceptionMessage) {
		$result = self::_validate(ArgumentTypeValidator::REAL, $arg, $exceptionMessage);
				
		return $result;
	}
	
	/**
	 * isResource
	 * Validates whether an argument is a resource and throws an exception if it isn't.
	 * @param mixed $arg
	 * @param string $exceptionMessage
	 */
	public static function isResource(/*mixed*/ $arg, /*string*/ $exceptionMessage) {
		$result = self::_validate(ArgumentTypeValidator::RESOURCE, $arg, $exceptionMessage);
				
		return $result;
	}
	
	/**
	 * isScalar
	 * Validates whether an argument is a scalar and throws an exception if it isn't.
	 * @param mixed $arg
	 * @param string $exceptionMessage
	 */
	public static function isScalar(/*mixed*/ $arg, /*string*/ $exceptionMessage) {
		$result = self::_validate(ArgumentTypeValidator::SCALAR, $arg, $exceptionMessage);
				
		return $result;
	}
	
	/**
	 * isArray
	 * Validates whether an argument is an array and throws an exception if it isn't.
	 * @param mixed $arg
	 * @param string $exceptionMessage
	 */
	public static function isArray(/*mixed*/ $arg, /*string*/ $exceptionMessage) {
		$result = self::_validate(ArgumentTypeValidator::T_ARRAY, $arg, $exceptionMessage);
				
		return $result;
	}
	
	/**
	 * Takes care of the validation and throws an exception if the validation fails.
	 * Enter description here ...
	 * @param int $type Constant from ArgumentTypeValidator
	 * @param mixed $arg
	 * @param string $exceptionMessage
	 * @throws InvalidArgumentException
	 * @return bool
	 */
	private static function _validate(/*int*/ $type, /*mixed*/ $arg, /*string*/ $exceptionMessage) {
		$localExceptionMessage = $exceptionMessage;
		$isValid = false;
		
		if (!is_string($exceptionMessage)) {
			$localExceptionMessage = strval($exceptionMessage);
		}
		
		if ($type == ArgumentTypeValidator::BOOL) {
			if (is_bool($arg)) {
				$isValid = true;
			}
		} else if ($type == ArgumentTypeValidator::DOUBLE) {
			if (is_double($arg)) {
				$isValid = true;
			}
		} else if ($type == ArgumentTypeValidator::FLOAT) {
			if (is_float($arg)) {
				$isValid = true;
			}
		} else if ($type == ArgumentTypeValidator::INTEGER) {
			if (is_integer($arg)) {
				$isValid = true;
			}
		} else if ($type == ArgumentTypeValidator::LONG) {
			if (is_long($arg)) {
				$isValid = true;
			}
		} else if ($type == ArgumentTypeValidator::NUMERIC) {
			if (is_numeric($arg)) {
				$isValid = true;
			}
		} else if ($type == ArgumentTypeValidator::OBJECT) {
			if (is_object($arg)) {
				$isValid = true;
			}
		} else if ($type == ArgumentTypeValidator::REAL) {
			if (is_real($arg)) {
				$isValid = true;
			}
		} else if ($type == ArgumentTypeValidator::RESOURCE) {
			if (is_resource($arg)) {
				$isValid = true;
			}
		} else if ($type == ArgumentTypeValidator::SCALAR) {
			if (is_scalar($arg)) {
				$isValid = true;
			}
		} else if ($type == ArgumentTypeValidator::STRING) {
			if (is_string($arg)) {
				$isValid = true;
			}
		} else if ($type == ArgumentTypeValidator::T_ARRAY) {
			if (is_array($arg)) {
				$isValid = true;
			}
		}
		
		if (!$isValid) {
			throw new InvalidArgumentException($exceptionMessage);
		}
		
		return $isValid;
	}
		
}

?>