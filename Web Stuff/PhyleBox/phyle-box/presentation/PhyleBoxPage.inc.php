<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/Page.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/AuthenticationPage.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/PhyleBoxHeaderControl.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/PhyleBoxFooterControl.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/Breadcrumb.inc.php");

/**
 * PhyleBoxPage
 * This class represents every page in PhyleBox.
 * @author lanathan
 *
 */
abstract class PhyleBoxPage extends Page {
	
	const AUTHENTICATION_PAGE_URL = "/phyle-box/login.php";

	protected $_breadcrumb;
	
	private static $__phyleBoxHeaderControl;
	private static $__phyleBoxFooterControl;
		
	/**
	 * Constructor
	 * @param string $title
	 * @return PhyleBoxPage
	 */
	public function PhyleBoxPage(/*string*/ $title) {
		parent::__construct($title);
		
		$this->registerStylesheet("_css/main.css");
		
		if (!isset(self::$__phyleBoxHeaderControl)) {
			self::$__phyleBoxHeaderControl = new PhyleBoxHeaderControl($this);
		}
		
		if (!isset(self::$__phyleBoxFooterControl)) {
			self::$__phyleBoxFooterControl = new PhyleBoxFooterControl($this);
		}
		
		$this->_breadcrumb = new Breadcrumb();
		
		$this->_checkAuthenticated();
		
		$this->registerScript("../_js/jquery.js");
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

		self::$__phyleBoxHeaderControl->render();
		
		$this->_breadcrumb->render();
	}
	
	public function closeDocument() {
		self::$__phyleBoxFooterControl->render();
		
?>
</div>
<?php
		
		parent::closeDocument();
	}
	
	/**
	 * getBreadcrumb
	 * Gets the breadcrumb object for the page.
	 * @return Breadcrumb
	 */
	public function getBreadcrumb() {
		return $this->_breadcrumb;
	}
	
	/**
	 * getAvatar
	 * Gets the location for the current user's avatar.
	 * @return string
	 */
	protected function getAvatar() {
		$result = "None.png";
		
		if (file_exists(dirname(__FILE__) . "/../_img/avatars/" . $this->getSessionFieldValue("userName") . ".png")) {
			$result = $this->getSessionFieldValue("userName") . ".png";
		}
		
		return $result;
	}
	
	/**
	 * _checkAuthenticated
	 * Checks the authentication of the current session.
	 */
	private function _checkAuthenticated() {
		if(!($this->getSessionFieldValue(AuthenticationPage::AUTHENTICATION_KEY) && Strings::equals($this->getSessionFieldValue(AuthenticationPage::AUTHENTICATION_KEY), session_id()))) {
			$url = PhyleBoxPage::AUTHENTICATION_PAGE_URL . "?" . AuthenticationPage::REFERRER_KEY . "=" . $_SERVER["REQUEST_URI"];
			
			header("Location: $url");
		}
	}
	
}

?>