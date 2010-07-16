<?php

/**
 * This class represents the configuration for the global library.
 * @author lanathan
 *
 */
final class Config {
		
	private static $__frameworkRoot;
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
	
}

?>