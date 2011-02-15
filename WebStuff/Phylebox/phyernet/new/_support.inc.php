<?php

require_once(dirname(__FILE__) . "/../_lib/phyernet/Config.inc.php");
require_once(PhyerNet_Config::getLocalPresentationLocation() . "PhyerNetPage.inc.php");

final class _Support_Page extends PhyerNetPage {
	
	public function _Support_Page() {
		parent::__construct("Support | PhyerNet");
	}
	
	public function openDocument() {
		parent::openDocument();
	}
	
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>