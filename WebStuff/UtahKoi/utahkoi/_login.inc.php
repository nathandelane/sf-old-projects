<?php

require_once(dirname(__FILE__) . "/../_lib/utahkoi/Config.inc.php");
require_once(UtahKoi_Config::getFrameworkRoot() . "presentation/AuthenticationPage.inc.php");
require_once(UtahKoi_Config::getLocalPresentationLocation() . "UtahKoiAuthenticationPage.inc.php");
require_once(UtahKoi_Config::getLocalPresentationLocation() . "controls/UtahKoiLoginForm.inc.php");

/**
 * _Login_Page
 * This class represents the login page.
 * @author lanathan
 *
 */
class _Login_Page extends UtahKoiAuthenticationPage {
	
	public $utahKoiLoginForm;

	/**
	 * Constructor
	 * @return _Login_Page
	 */
	public function _Login_Page() {
		parent::__construct("Login | Utah Koi", UtahKoi_Config::getUtahKoiRoot() . "/index.php", "71ddf264-73b5-4fa2-8ecd-585288f5ab3c");
		
		$this->utahKoiLoginForm = new UtahKoiLoginForm($this, UtahKoi_Config::getUtahKoiRoot() . "/login.php", false);
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