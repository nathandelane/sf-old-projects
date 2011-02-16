<?php

/**
 * Environment
 * This class provides static environment-related information
 * @author lanathan
 *
 */
final class Environment {
	
	const NIX = 1;
	const WINDOWS = 3;
	
	private static $__newLineString;
	private static $__operatingSystem;
	private static $__directorySeparator;
	
	/**
	 * getNewLine
	 * Gets the current newline string based on the operating system.
	 * @return string
	 */
	public static function getNewLine() {
		if (!isset(self::$__newLineString)) {
			if (self::getOperatingSystem() == Environment::WINDOWS) {
				self::$__newLineString = "\r\n";
			} else {
				self::$__newLineString = "\n";
			}
		}
		
		return self::$__newLineString;
	}
	
	/**
	 * getOperatingSystem
	 * Gets the current operating system.
	 * @return int Either the constant NIX or WINDOWS is returned.
	 */
	public static function getOperatingSystem() {
		if (!isset(self::$__operatingSystem)) {
			if (substr(PHP_OS, 0, 3) === "WIN") {
				self::$__operatingSystem = Environment::WINDOWS;
			} else {
				self::$__operatingSystem = Environment::NIX;
			}
		}
		
		return self::$__operatingSystem;
	}
	
	/**
	 * getDirectorySeparator
	 * Gets the directory separator for the current environment.
	 * @return string
	 */
	public static function getDirectorySeparator() {
		if (!isset(self::$__directorySeparator)) {
			if (self::getOperatingSystem() == Environment::WINDOWS) {
				self::$__directorySeparator = "\\";
			} else {
				self::$__directorySeparator = "/";
			}
		}
		
		return self::$__directorySeparator;
	}
	
}

?>