<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/Page.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/AuthenticationPage.inc.php");

/**
 * PhyleBoxBasicPage
 * This class represents every basic page in PhyleBox.
 * @author lanathan
 *
 */
abstract class PhyleBoxBasicPage extends Page {
	
	const AUTHENTICATION_PAGE_URL = "/phyle-box/login.php";
		
	/**
	 * Constructor
	 * @param string $title
	 * @return PhyleBoxBasicPage
	 */
	public function PhyleBoxBasicPage(/*string*/ $title) {
		parent::__construct($title);
		
		$this->registerStylesheet("_css/main.css");
		
		$this->_checkAuthenticated();
		
		$this->registerScript("/_js/jquery.js");
		$this->registerScript("/_js/jquery.fieldselection.js");
		$this->registerScript("/_js/Phyer.js");
		$this->registerScript("_js/FileManager.js");
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/Page::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}

	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/Page::closeDocument()
	 */
	public function closeDocument() {
		parent::closeDocument();
	}
	
	/**
	 * _checkAuthenticated
	 * Checks the authentication of the current session.
	 */
	private function _checkAuthenticated() {
		if(!($this->getSessionFieldValue($this->getAuthenticationKey()) && Strings::equals($this->getSessionFieldValue($this->getAuthenticationKey()), session_id()))) {
			$url = PhyleBoxEditorPage::AUTHENTICATION_PAGE_URL . "?" . AuthenticationPage::REFERRER_KEY . "=" . $_SERVER["REQUEST_URI"];
			
			header("Location: $url");
		}
	}
	
}

?>