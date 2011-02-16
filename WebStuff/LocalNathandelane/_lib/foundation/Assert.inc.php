<?php

/**
 * Assert
 * Provides assertion rules.
 * @author lanathan
 *
 */
final class Assert {
	
	/**
	 * isTrue
	 * Asserts whether an expression is true.
	 * @param unknown_type $expression
	 * @param unknown_type $message
	 * @throws AssertionException
	 * @return void
	 */
	public static function isTrue(/*bool*/ $expression, /*string*/ $message = null) {
		if (!$expression || $expression === false) {
			throw new AssertionException($message);
		}
	}
	
}

/**
 * AssertionException
 * Assertion exception class.
 * @author lanathan
 *
 */
class AssertionException extends LogicException {
	
	/**
	 * Constructor
	 * @param string $message
	 * @param int $code
	 * @param Exception $other
	 * @return AssertionException
	 */
	public function AssertionException(/*string*/ $message, /*int*/ $code = 0, Exception $other = null) {
		parent::__construct($message, $code);
	}
	
}

?>