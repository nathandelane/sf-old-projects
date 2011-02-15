<?php

if (!session_id()) {
	session_start();
}

/**
 * This class represents the configuration for PhyleBox.
 * @author lanathan
 *
 */
final class PhyerNet_Config {
	
	private static $__frameworkRoot;
	private static $__analyticsWebPropertyIds = array(
		"www.phyer.net" => "UA-21407973-2"
	);
	private static $__localPresentationFolder;
	private static $__localFoundationFolder;
	private static $__phyerNetErrorHandlerPage = "/error/";
	private static $__phyerNetRoot = "http://localhost.phyer.net:8080";
	
	/**
	 * getFrameworkRoot
	 * Gets the framework root.
	 * @return string
	 */
	public static function getFrameworkRoot() {
		if (!isset(self::$__frameworkRoot)) {
			$localConfigLocation = dirname(__FILE__);
			
			self::$__frameworkRoot = "$localConfigLocation/../";
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
			$frameworkRoot = self::getFrameworkRoot();
			
			self::$__localPresentationFolder = "$frameworkRoot/phyernet/presentation/";
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
			$frameworkRoot = self::getFrameworkRoot();
			
			self::$__localFoundationFolder = "$frameworkRoot/phyernet/foundation/";
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
			header("Location: " . self::$__phyerNetErrorHandlerPage);
		} else {
			
		}
	}
	
	/**
	 * getPhyerNetRoot
	 * Gets the root HTTP directory for PhyerNet.
	 * @return string
	 */
	public static function getPhyerNetRoot() {
		return self::$__phyerNetRoot;
	}
	
}

set_error_handler("PhyerNet_Config::handleError");

?>