<?php

require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");

/**
 * This class represents the configuration for PhyleBox.
 * @author lanathan
 *
 */
final class PhyleBox_Config {
	
	private static $__frameworkRoot;
	private static $__analyticsWebPropertyIds = array(
		"www.phyer.net" => "UA-12532045-2"
	);
	private static $__localPresentationFolder;
	private static $__localFoundationFolder;
	private static $__phyerNetErrorHandlerPage = "/error/";
	private static $__phyleBoxRoot = "http://localhost.phyer.net:8080/phyle-box";
	private static $__phyleBoxSalt = "34b14c5e-448e-4992-98a8-5274bb49d125";
	
	/**
	 * getFrameworkRoot
	 * Gets the framework root.
	 * @return string
	 */
	public static function getFrameworkRoot() {
		if (!isset(self::$__frameworkRoot)) {
			$localConfigLocation = dirname(__FILE__);
			
			self::$__frameworkRoot = "$localConfigLocation/../../_lib/";
		}
		
		return self::$__frameworkRoot;
	}
		
	/**
	 * getFrameworkFoundation
	 * Gets the framework foundation folder.
	 * @return string
	 */
	public static function getFrameworkFoundation() {
		return self::getFrameworkRoot() . "foundation/";
	}
	
	/**
	 * getFrameworkPresentation
	 * Gets the framework presentation folder.
	 * @return string
	 */
	public static function getFrameworkPresentation() {
		return self::getFrameworkRoot() . "presentation/";
	}
	
	/**
	 * getWebPropertyIdForSite
	 * Gets the Google Analytics web property ID for a particular site
	 * @param string $webSite
	 * @return string
	 */
	public static function getWebPropertyIdForSite(/*string*/ $webSite) {
		$propertyId = null;
		
		ArgumentTypeValidator::isString($webSite, "WebSite must be a string in the form www.domain.com.");
		
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
			
			self::$__localPresentationFolder = "$frameworkRoot/phyle-box/presentation/";
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
			
			self::$__localFoundationFolder = "$frameworkRoot/phyle-box/foundation/";
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
			header("Location: " . self::getPhyleBoxRoot() . self::$__phyleBoxErrorHandlerPage);
		} else {
			
		}
	}
	
	/**
	 * getPhyleBoxRoot
	 * Gets the HTTP root of PhyleBox
	 * @return string
	 */
	public static function getPhyleBoxRoot() {
		return self::$__phyleBoxRoot;
	}
	
	/**
	 * getSalt
	 * Returns salt expected for PhyleBox authentication.
	 * @Return string
	 */
	public static function getSalt() {
		return self::$__phyleBoxSalt;
	}
	
}

set_error_handler("PhyleBox_Config::handleError");

?>