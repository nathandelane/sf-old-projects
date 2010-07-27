<?php

require_once(dirname(__FILE__) . "/../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");

/**
 * Strings
 * This class extends the string in PHP and normalizes string functionality.
 * @author lanathan
 *
 */
class Strings {
	
	/**
	 * equals
	 * Compares two strings.
	 * @param string $source
	 * @param string $other
	 * @param bool $ignoreCase False by default
	 * @return bool
	 */
	public static function equals(/*string*/ $source, /*string*/ $other, /*bool*/ $ignoreCase = false) {
		if ($ignoreCase) {
			$source = Strings::toLower($source);
			$other = Strings::toLower($other);
		}
		
		return (strcmp($source, $other) == 0);
	}
	
	/**
	 * startsWith
	 * Determines whether a source string starts with another string.
	 * @param string $source
	 * @param string $start
	 * @param bool $ignoreCase False by default
	 * @return bool
	 */
	public static function startsWith(/*string*/ $source, /*string*/ $start, /*bool*/ $ignoreCase = false) {
		if ($ignoreCase) {
			$source = Strings::toLower($source);
			$start = Strings::toLower($start);
		}
		
		return (Strings::substring($source, 0, Strings::length($start)) === $start);
	}
	
	/**
	 * endsWith
	 * Determines whether a source string ends with another string.
	 * @param string $source
	 * @param string $end
	 * @param bool $ignoreCase False by default
	 */
	public static function endsWith(/*string*/ $source, /*string*/ $end, /*bool*/ $ignoreCase = false) {
		if ($ignoreCase) {
			$source = Strings::toLower($source);
			$end = Strings::toLower($end);
		}
		
		return (Strings::substring($source, (Strings::length($source) - Strings::length($end))) === $end);
	}
	
	/**
	 * toLower
	 * Converts a source string to lowercase.
	 * @param string $source
	 * @return string
	 */
	public static function toLower(/*string*/ $source) {
		return strtolower($source);
	}
	
	/**
	 * toUpper
	 * Converts a source string to uppercase.
	 * @param string $source
	 * @return string
	 */
	public static function toUpper(/*string*/ $source) {
		return strtoupper($source);
	}
	
	/**
	 * length
	 * Gets the length of a source string.
	 * @param string $source
	 * @return int
	 */
	public static function length(/*string*/ $source) {
		return strlen($source);
	}
	
	/**
	 * substring
	 * Gets a substring of a source string.
	 * @param string $source
	 * @param int $startIndex
	 * @param int $length 0 returns the entire remainder.
	 * @return string
	 */
	public static function substring(/*string*/ $source, /*int*/ $startIndex, /*int*/ $length = 0) {
		$result = null;
		
		if ($length == 0) {
			$result = substr($source, $startIndex);
		} else {
			$result = substr($source, $startIndex, $length);
		}
		
		return $result;
	}
	
}

?>