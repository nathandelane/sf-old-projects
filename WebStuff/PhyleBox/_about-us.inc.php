<?php

require_once(dirname(__FILE__) . "/Config.inc.php");
require_once(PhyerNet_Config::getLocalPresentationLocation() . "PhyerNetPage.inc.php");

final class _AboutUs_Page extends PhyerNetPage {
	
	public function _AboutUs_Page() {
		parent::__construct("About Us | PhyerNet");
		
		$this->registerStylesheet("/_css/about-us.css");
	}
	
	public function openDocument() {
		parent::openDocument();
	}
	
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>