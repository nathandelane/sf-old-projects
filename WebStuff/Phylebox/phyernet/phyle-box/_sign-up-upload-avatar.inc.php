<?php

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "PhyleBoxNonAuthenticationPage.inc.php");

class _Sign_Up_Upload_Avatar extends PhyleBoxNonAuthenticationPage {
	
	public function _Sign_Up_Upload_Avatar() {
		parent::__construct("Sign Up - Upload Avatar | PhyleBox");
	}
	
	public function openDocument() {
		parent::openDocument();
	}
	
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>