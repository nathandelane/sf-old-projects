<?php

/**
 * This class represents the configuration for the global library.
 * @author lanathan
 *
 */
final class Config {
		
	private static $__frameworkRoot;
	private static $__nathandelaneRoot;
	private static $__isDebugEnvironment = true;
	
	/**
	 * Gets the framework root.
	 * @return string
	 */
	public static function getFrameworkRoot() {
		if (!isset(self::$__frameworkRoot)) {
			$localConfigLocation = dirname(__FILE__);
			
			self::$__frameworkRoot = "$localConfigLocation/";
		}
		
		return self::$__frameworkRoot;
	}
	
	/**
	 * isDebugEnvironment
	 * Gets whether the current environment is a debug environment.
	 * @return bool
	 */
	public static function isDebugEnvironment() {
		return self::$__isDebugEnvironment;
	}
	
	/**
	 * getDatabaseCredentials
	 * Gets the database credentials for the current environment.
	 * @return array
	 */
	public static function getDatabaseCredentials() {
		return array("server" => "localhost", "userName" => "root", "password" => "i78y6zbgfhla");
	}
	
	/**
	 * getNathandelaneRoot
	 * Gets the root of nathandelane.
	 * @return string
	 */
	public static function getNathandelaneRoot() {
		if (!isset(self::$__frameworkRoot)) {
			$localConfigLocation = dirname(__FILE__);
			
			self::$__frameworkRoot = "$localConfigLocation/../nathandelane/";
		}
		
		return self::$__frameworkRoot;
	}
	
}

?>