<?php

if(!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../../_lib/admin/Config.inc.php");
require_once(Admin_Config::getLocalPresentationLocation() . "AdminPage.inc.php");

class _Logout_Page extends AdminPage {
	
	public function _Logout_Page() {
		parent::__construct("Admin | Logging you out...");
		
		$this->_logoutUser();
		
		header("Location: " . Admin_Config::getAdminRoot() . "/login.php");
	}
	
	private function _logoutUser() {
		if(isset($_SESSION[$this->getAuthenticationKey()]) && Strings::equals($_SESSION[$this->getAuthenticationKey()], session_id())) {
			session_destroy();
		}
	}
	
}

?>