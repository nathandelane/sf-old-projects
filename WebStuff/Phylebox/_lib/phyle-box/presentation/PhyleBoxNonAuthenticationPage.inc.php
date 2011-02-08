<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/AuthenticationPage.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/GoogleAnalytics.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/PhyleBoxHeaderControl.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/PhyleBoxFooterControl.inc.php");

/**
 * PhyleBoxNonAuthenticationPage
 * This class represents a phyle-box non-authentication page.
 * @author lanathan
 *
 */
abstract class PhyleBoxNonAuthenticationPage extends Page {
	
	private static $__phyleBoxHeaderControl;
	private static $__phyleBoxFooterControl;
	private static $__googleAnalyticsControl;
	
	/**
	 * Constructor
	 * @return PhyleBoxNonAuthenticationPage
	 */
	public function PhyleBoxNonAuthenticationPage(/*string*/ $title) {
		parent::__construct($title);
		
		$this->registerStylesheet("_css/main.css");
		$this->registerStylesheet("_css/sign-up.css");
		$this->registerScript("/_js/jquery.js");
		
		if (!isset(self::$__phyleBoxHeaderControl)) {
			self::$__phyleBoxHeaderControl = new PhyleBoxHeaderControl($this);
		}
		
		if (!isset(self::$__phyleBoxFooterControl)) {
			self::$__phyleBoxFooterControl = new PhyleBoxFooterControl($this);
		}
		
		if (!isset(self::$__googleAnalyticsControl)) {
			self::$__googleAnalyticsControl = new GoogleAnalytics("www.phyer.net");
		}
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/AuthenticationPage::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
		
?>
<div class="root">
<?php

		self::$__phyleBoxHeaderControl->render();
		
?>
	<div class="content">
<?php
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/AuthenticationPage::closeDocument()
	 */
	public function closeDocument() {
?>
	</div>
<?php
		
		self::$__phyleBoxFooterControl->render();
		self::$__googleAnalyticsControl->render();
		
?>
</div>
<?php

		parent::closeDocument();
	}
		
}

?>