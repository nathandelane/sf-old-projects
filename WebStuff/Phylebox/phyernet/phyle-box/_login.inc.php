<?php

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/AuthenticationPage.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "PhyleBoxAuthenticationPage.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/PhyleBoxLoginForm.inc.php");

/**
 * _Login_Page
 * This class represents the login page.
 * @author lanathan
 *
 */
class _Login_Page extends PhyleBoxAuthenticationPage {
	
	public $phyleBoxLoginForm;

	/**
	 * Constructor
	 * @return _Login_Page
	 */
	public function _Login_Page() {
		parent::__construct("Login | PhyleBox", PhyleBox_Config::getPhyleBoxRoot() . "/index.php", "34b14c5e-448e-4992-98a8-5274bb49d125");
		
		$this->phyleBoxLoginForm = new PhyleBoxLoginForm($this, PhyleBox_Config::getPhyleBoxRoot() . "/login.php", false);
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/AuthenticationPage::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/AuthenticationPage::closeDocument()
	 */
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>