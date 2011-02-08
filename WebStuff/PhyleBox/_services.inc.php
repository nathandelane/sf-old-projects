<?php

require_once(dirname(__FILE__) . "/Config.inc.php");
require_once(PhyerNet_Config::getLocalPresentationLocation() . "PhyerNetPage.inc.php");

final class _Services_Page extends PhyerNetPage {
	
	public function _Services_Page() {
		parent::__construct("Services | PhyerNet");
	}
	
	public function openDocument() {
		parent::openDocument();
	}
	
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>