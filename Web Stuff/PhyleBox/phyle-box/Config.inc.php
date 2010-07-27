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
	private static $__phyleBoxRoot = "/PhyerNet2010/phyle-box";
	
	/**
	 * getFrameworkRoot
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
	 * getPhyleBoxRoot
	 * Gets the HTTP root of PhyleBox
	 * @return string
	 */
	public static function getPhyleBoxRoot() {
		return self::$__phyleBoxRoot;
	}
	
}

?>