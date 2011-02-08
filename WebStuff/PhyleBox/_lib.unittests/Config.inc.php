<?php

/**
 * This class represents the configuration for the global library.
 * @author lanathan
 *
 */
final class LibUnitTests_Config {
		
	private static $__frameworkRoot;
	
	/**
	 * Gets the framework root.
	 * @return string
	 */
	public static function getFrameworkRoot() {
		if (!isset(self::$__frameworkRoot)) {
			$localConfigLocation = dirname(__FILE__);
			
			self::$__frameworkRoot = "$localConfigLocation/../_lib/";
		}
		
		return self::$__frameworkRoot;
	}
	
}

?>