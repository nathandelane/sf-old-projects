<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/collections/ArrayList.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/controls/GoogleAnalytics.inc.php");

/**
 * This class is a handler for Google Analytics modules.
 * @author lanathan
 *
 */
class GoogleAnalyticsHandler implements IRenderable {
	
	private $_headGoogleAnalyticsScripts;
	private $_bodyGoogleAnalyticsScripts;
	
	/**
	 * Creates a new instance of GoogleAnalyticsHandler.
	 * @return GoogleAnalyticsHandler
	 */
	public function GoogleAnalyticsHandler() {
		$this->_headGoogleAnalyticsScripts = new ArrayList();
		$this->_bodyGoogleAnalyticsScripts = new ArrayList();
	}
	
	/**
	 * Adds a GoogleAnalytics control to the head collection.
	 * @param GoogleAnalytics $analyticsControl
	 */
	public function addToHead(GoogleAnalytics $analyticsControl) {
		$this->_headGoogleAnalyticsScripts->add($analyticsControl);
	}
	
	/**
	 * Adds a GoogleAnalytics control to the body collection.
	 * @param GoogleAnalytics $analyticsControl
	 */
	public function addToBody(GoogleAnalytics $analyticsControl) {
		$this->_bodyGoogleAnalyticsScripts->add($analyticsControl);
	}
	
	/**
	 * Gets the complete head collection of analytics.
	 * @return ArrayList
	 */
	public function getHeadAnalyticsScripts() {
		return $this->_headGoogleAnalyticsScripts;
	}
	
	/**
	 * Gets the complete body collection of analytics.
	 * @return ArrayList
	 */
	public function getBodyAnalyticsScripts() {
		return $this->_bodyGoogleAnalyticsScripts;
	}
	
}

?>