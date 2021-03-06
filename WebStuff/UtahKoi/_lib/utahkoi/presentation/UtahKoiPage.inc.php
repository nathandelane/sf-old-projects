<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../Config.inc.php");
require_once(UtahKoi_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(UtahKoi_Config::getFrameworkRoot() . "presentation/Page.inc.php");
require_once(UtahKoi_Config::getFrameworkRoot() . "presentation/AuthenticationPage.inc.php");
require_once(UtahKoi_Config::getLocalPresentationLocation() . "controls/UtahKoiHeaderControl.inc.php");
require_once(UtahKoi_Config::getLocalPresentationLocation() . "controls/UtahKoiFooterControl.inc.php");

/**
 * UtahKoiPage
 * This class represents every page in UtahKoi.com.
 * @author lanathan
 *
 */
abstract class UtahKoiPage extends Page {
	
	const AUTHENTICATION_PAGE_URL = "/login.php";
	
	private static $__utahKoiHeaderControl;
	private static $__utahKoiFooterControl;
		
	/**
	 * Constructor
	 * @param string $title
	 * @return UtahKoiPage
	 */
	public function UtahKoiPage(/*string*/ $title) {
		parent::__construct($title, "ticket-utahkoi");
		
		$this->registerStylesheet("_css/main.css");
		
		if (!isset(self::$__utahKoiHeaderControl)) {
			self::$__utahKoiHeaderControl = new UtahKoiHeaderControl($this);
		}
		
		if (!isset(self::$__utahKoiFooterControl)) {
			self::$__utahKoiFooterControl = new UtahKoiFooterControl($this);
		}
		
		$this->_checkAuthenticated();
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

		self::$__utahKoiHeaderControl->render();
	}
	
	public function closeDocument() {
		self::$__utahKoiFooterControl->render();
		
?>
</div>
<?php
		
		parent::closeDocument();
	}
		
	/**
	 * _checkAuthenticated
	 * Checks the authentication of the current session.
	 */
	private function _checkAuthenticated() {
		if(!($this->getSessionFieldValue($this->getAuthenticationKey()) && Strings::equals($this->getSessionFieldValue($this->getAuthenticationKey()), session_id()))) {
			$url = self::AUTHENTICATION_PAGE_URL . "?" . AuthenticationPage::REFERRER_KEY . "=" . $_SERVER["REQUEST_URI"];
			
			header("Location: $url");
		}
	}
	
}

?>