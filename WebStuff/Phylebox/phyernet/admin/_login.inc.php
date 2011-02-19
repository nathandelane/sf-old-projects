<?php

require_once(dirname(__FILE__) . "/../../_lib/admin/Config.inc.php");
require_once(Admin_Config::getFrameworkRoot() . "presentation/AuthenticationPage.inc.php");
require_once(Admin_Config::getLocalPresentationLocation() . "AdminAuthenticationPage.inc.php");
require_once(Admin_Config::getLocalPresentationLocation() . "controls/AdminLoginForm.inc.php");

/**
 * _Login_Page
 * This class represents the login page.
 * @author lanathan
 *
 */
class _Login_Page extends AdminAuthenticationPage {
	
	public $adminLoginForm;

	/**
	 * Constructor
	 * @return _Login_Page
	 */
	public function _Login_Page() {
		parent::__construct("Login | PhyerNet Admin", Admin_Config::getAdminRoot() . "/", "34b14c5e-448e-4992-98a8-5274bb49d125");
		
		$this->adminLoginForm = new AdminLoginForm($this, Admin_Config::getAdminRoot() . "/login.php");
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