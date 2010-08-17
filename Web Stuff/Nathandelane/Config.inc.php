<?php

if (!session_id()) {
	session_start();
}

/**
 * This class represents the configuration for Nathandelane.com.
 * @author lanathan
 *
 */
final class Nathandelane_Config {
	
	private static $__frameworkRoot;
	private static $__analyticsWebPropertyIds = array(
		"nathandelane.com" => "UA-17982449-1"
	);
	private static $__localPresentationFolder;
	private static $__localFoundationFolder;
	private static $__nathandelaneErrorHandlerPage = "/error/";
	private static $__nathandelaneRoot = "/";
	
	/**
	 * getFrameworkRoot
	 * Gets the framework root.
	 * @return string
	 */
	public static function getFrameworkRoot() {
		if (!isset(self::$__frameworkRoot)) {
			$localConfigLocation = dirname(__FILE__);
			
			self::$__frameworkRoot = "$localConfigLocation/_lib/";
		}
		
		return self::$__frameworkRoot;
	}
	
	/**
	 * getWebPropertyIdForSite
	 * Gets the Google Analytics web property ID for a particular site
	 * @param string $webSite
	 * @return string
	 */
	public static function getWebPropertyIdForSite(/*string*/ $webSite) {
		$propertyId = null;

		if (!is_string($webSite)) {
			throw new InvalidArgumentException("WebStie must be a string.", $code);
		}
		
		if (array_key_exists($webSite, self::$__analyticsWebPropertyIds)) {
			$propertyId = self::$__analyticsWebPropertyIds[$webSite];
		}
		
		return $propertyId;
	}
	
	/**
	 * getLocalPresentationLocation
	 * Gets the local presentation folder for this site or sub-site.
	 * @return string
	 */
	public static function getLocalPresentationLocation() {
		if (!isset(self::$__localPresentationFolder)) {
			$localConfigLocation = dirname(__FILE__);
			
			self::$__localPresentationFolder = "$localConfigLocation/presentation/";
		}
		
		return self::$__localPresentationFolder;
	}
	
	/**
	 * getLocalFoundationLocation
	 * Gets the local foundation folder for this site or sub-site.
	 * @return string
	 */
	public static function getLocalFoundationLocation() {
		if (!isset(self::$__localFoundationFolder)) {
			$localConfigLocation = dirname(__FILE__);
			
			self::$__localFoundationFolder = "$localConfigLocation/foundation/";
		}
		
		return self::$__localFoundationFolder;
	}
	
	/**
	 * handleError
	 * This function handles errors, and is defined as the global error handler.
	 * @param int $number
	 * @param string $string
	 * @param string $file
	 * @param int $line
	 */
	public static function handleError($number, $string, $file, $line) {
		$phpError = array("number" => $number, "string" => $string, "file" => $file, "line" => $line);
		
		$_SESSION["PHP_ERROR"] = $phpError;
		
		if ($number == E_USER_ERROR) {
			header("Location: " . self::$__nathandelaneErrorHandlerPage);
		} else {
			
		}
	}
	
	/**
	 * getPhyerNetRoot
	 * Gets the root HTTP directory for PhyerNet.
	 * @return string
	 */
	public static function getPhyerNetRoot() {
		return self::$__nathandelaneRoot;
	}
	
}

set_error_handler("Nathandelane_Config::handleError");

?>