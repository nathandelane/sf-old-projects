<?php

if(!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "PhyleBoxPage.inc.php");

class _Logout_Page extends PhyleBoxPage {
	
	public function _Logout_Page() {
		parent::__construct("PhyleBox | Logging you out...");
		
		$this->_logoutUser();
		
		header("Location: " . PhyleBox_Config::getPhyleBoxRoot() . "/login.php");
	}
	
	private function _logoutUser() {
		if(isset($_SESSION[$this->getAuthenticationKey()]) && Strings::equals($_SESSION[$this->getAuthenticationKey()], session_id())) {
			session_destroy();
		}
	}
	
}

?>