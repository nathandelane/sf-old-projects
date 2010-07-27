<?php

require_once(dirname(__FILE__) . "/Config.inc.php");
require_once(PhyerNet_Config::getFrameworkRoot() . "presentation/controls/Button.inc.php");
require_once(PhyerNet_Config::getLocalPresentationLocation() . "PhyerNetPage.inc.php");

final class _Index_Page extends PhyerNetPage {
	
	public $buttons;
	
	public function _Index_Page() {
		parent::__construct("Welcome to PhyerNet");
		
		$this->buttons = array();
		$this->buttons["signUpNow"] = new Button("signUpNow", "Sign Up Now",	"/services.php");
	}
	
	public function openDocument() {
		parent::openDocument();
	}
	
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>