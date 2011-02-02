<?php

/**
 * This class represents the configuration for the global library.
 * @author lanathan
 *
 */
final class Config {
		
	private static $__frameworkRoot;
	private static $__phyernetRoot;
	private static $__phyleboxRoot;
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
		return array("server" => "localhost", "userName" => "phyeradmin", "password" => "o6dv28upwrgeq");
//		return array("server" => "localhost", "userName" => "root", "password" => "i78y6zbgfhla");
	}
	
	/**
	 * getPhyernetRoot
	 * Gets the root of phyernet.
	 * @return string
	 */
	public static function getPhyernetRoot() {
		if (!isset(self::$__phyernetRoot)) {
			$localConfigLocation = dirname(__FILE__);
			
			self::$__phyernetRoot = "$localConfigLocation/../phyernet/";
		}
		
		return self::$__phyernetRoot;
	}
	
	/**
	 * getPhyleboxRoot
	 * Gets the root of phyle-box.
	 * @return string
	 */
	public static function getPhyleboxRoot() {
		if (!isset(self::$__phyleboxRoot)) {
			$localConfigLocation = dirname(__FILE__);
			
			self::$__phyleboxRoot = "$localConfigLocation/../phyernet/phyle-box/";
		}
		
		return self::$__phyleboxRoot;
	}
	
}

?>