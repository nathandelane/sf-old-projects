<?php

require_once(dirname(__FILE__) . "/../Config.inc.php");
require_once(Nathandelane_Config::getFrameworkRoot() . "presentation/Page.inc.php");
require_once(Nathandelane_Config::getFrameworkRoot() . "presentation/controls/GoogleAnalytics.inc.php");
require_once(Nathandelane_Config::getLocalPresentationLocation() . "controls/Header.inc.php");

/**
 * NathandelanePage
 * Base page class for every page on Nathandelane.com
 * @author lanathan
 *
 */
abstract class NathandelanePage extends Page {
	
	private static $__googleAnalytics;
	private static $__header;
	
	/**
	 * Constructor
	 * @param string $title
	 * @return NathandelanePage
	 */
	public function NathandelanePage(/*string*/ $title) {
		parent::__construct($title);
		
		if (!isset(self::$__googleAnalytics)) {
			self::$__googleAnalytics = new GoogleAnalytics("www.nathandelane.com");
		}
		
		if (!isset(self::$__header)) {
			self::$__header = new Header();
		}
		
		$this->registerStylesheet("/_css/main.css");
		$this->registerScript("/_js/jquery.js");
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/Page::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
		
?>
<div class="root">
<?php
		
		self::$__googleAnalytics->render();
		self::$__header->render();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/Page::closeDocument()
	 */
	public function closeDocument() {
?>
</div>
<?php		
		
		parent::closeDocument();
	}
	
}

?>