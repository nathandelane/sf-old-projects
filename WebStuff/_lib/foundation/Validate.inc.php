<?php

require_once(dirname(__FILE__) . "/../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/Strings.inc.php");

/**
 * Validate
 * Provides assertion rules.
 * @author lanathan
 *
 */
final class Validate {
	
	const AMEX = 1;
	const DISCOVER = 2;
	const MASTERCARD = 3;
	const VISA = 4;
	
	private static $__amexRegex = '/^4[0-9]{12}(?:[0-9]{3})?$/';
	private static $__discRegex = '/^6(?:011|5[0-9]{2})[0-9]{12}$/';
	private static $__mastRegex = '/^5[1-5][0-9]{14}$/';
	private static $__visaRegex = '/^4[0-9]{12}(?:[0-9]{3})?$/';
	
	/**
	 * isValidAmexCardNumber
	 * Validates whether the number given is a valid American Express credit card number.
	 * @param string $creditCardNumber
	 * @param string $message
	 * @throws ValidationException
	 * @return bool
	 */
	public static function isValidAmexCardNumber(/*string*/ $creditCardNumber, /*string*/ $message) {
		ArgumentTypeValidator::isString($creditCardNumber, "CreditCardNumber must be a string.");

		$result = true;
		
		if (!self::_validateCreditCard($creditCardNumber, Validate::AMEX)) {
			throw new ValidationException($message);
		}
		
		return $result;
	}
	
	/**
	 * isValidDiscCardNumber
	 * Validates whether the number given is a valid Discover credit card number.
	 * @param string $creditCardNumber
	 * @param string $message
	 * @throws ValidationException
	 * @return bool
	 */
	public static function isValidDiscCardNumber(/*string*/ $creditCardNumber, /*string*/ $message) {
		ArgumentTypeValidator::isString($creditCardNumber, "CreditCardNumber must be a string.");

		$result = true;
		
		if (!self::_validateCreditCard($creditCardNumber, Validate::DISCOVER)) {
			throw new ValidationException($message);
		}
		
		return $result;
	}
	
	/**
	 * isValidMastercardNumber
	 * Validates whether the number given is a valid Mastercard credit card number.
	 * @param string $creditCardNumber
	 * @param string $message
	 * @throws ValidationException
	 * @return bool
	 */
	public static function isValidMastercardNumber(/*string*/ $creditCardNumber, /*string*/ $message) {
		ArgumentTypeValidator::isString($creditCardNumber, "CreditCardNumber must be a string.");

		$result = true;
		
		if (!self::_validateCreditCard($creditCardNumber, Validate::MASTERCARD)) {
			throw new ValidationException($message);
		}
		
		return $result;
	}
	
	/**
	 * isValidVisaNumber
	 * Validates whether the number given is a valid Visa credit card number.
	 * @param string $creditCardNumber
	 * @param string $message
	 * @throws ValidationException
	 * @return bool
	 */
	public static function isValidVisaNumber(/*string*/ $creditCardNumber, /*string*/ $message) {
		ArgumentTypeValidator::isString($creditCardNumber, "CreditCardNumber must be a string.");

		$result = true;
		
		if (!self::_validateCreditCard($creditCardNumber, Validate::VISA)) {
			throw new ValidationException($message);
		}
		
		return $result;
	}
	
	/**
	 * _validateCreditCard
	 * Validates a credit card.
	 * @param unknown_type $creditCardNumber
	 * @param unknown_type $cardType
	 * @return bool
	 */
	private static function _validateCreditCard(/*string*/ $creditCardNumber, /*int*/ $cardType) {
		$result = false;
		$localCreditCardNumber = Validate::_removeHyphens($creditCardNumber);
		
		if ($cardType === Validate::AMEX) {
			$result = (preg_match(self::$__amexRegex, $localCreditCardNumber) === 1);
		} else if ($cardType === Validate::DISCOVER) {
			$result = (preg_match(self::$__discRegex, $localCreditCardNumber) === 1);
		} else if ($cardType === Validate::MASTERCARD) {
			$result = (preg_match(self::$__mastRegex, $localCreditCardNumber) === 1);
		} else if ($cardType === Validate::VISA) {
			$result = (preg_match(self::$__visaRegex, $localCreditCardNumber) === 1);
		}
		
		return $result;
	}
	
	/**
	 * _removeHyphens
	 * Removes hyphens from a string.
	 * @param string $source
	 */
	private static function _removeHyphens(/*string*/ $source) {
		$result = $source;
		
		while (Strings::contains($result, "-")) {
			$result = Strings::replace($result, "-", "");
		}
		
		return $result;
	}
	
}

/**
 * ValidationException
 * Validation exception class.
 * @author lanathan
 *
 */
class ValidationException extends LogicException {
	
	/**
	 * Constructor
	 * @param string $message
	 * @param int $code
	 * @param Exception $other
	 * @return ValidationException
	 */
	public function ValidationException(/*string*/ $message, /*int*/ $code = 0, Exception $other = null) {
		parent::__construct($message, $code);
	}
	
}

?>