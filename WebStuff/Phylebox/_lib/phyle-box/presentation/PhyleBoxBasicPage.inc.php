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
 * PhyleBoxEditorPage
 * This class represents every editor page in PhyleBox.
 * @author lanathan
 *
 */
abstract class PhyleBoxEditorPage extends Page {
	
	const AUTHENTICATION_PAGE_URL = "/phyle-box/login.php";
		
	/**
	 * Constructor
	 * @param string $title
	 * @return PhyleBoxEditorPage
	 */
	public function PhyleBoxEditorPage(/*string*/ $title) {
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
		if(!($this->getSessionFieldValue(AuthenticationPage::AUTHENTICATION_KEY) && Strings::equals($this->getSessionFieldValue(AuthenticationPage::AUTHENTICATION_KEY), session_id()))) {
			$url = PhyleBoxEditorPage::AUTHENTICATION_PAGE_URL . "?" . AuthenticationPage::REFERRER_KEY . "=" . $_SERVER["REQUEST_URI"];
			
			header("Location: $url");
		}
	}
	
}

?>