<?php

require_once(dirname(__FILE__) . "/Config.inc.php");
require_once(Nathandelane_Config::getLocalPresentationLocation() . "NathandelanePage.inc.php");

class _Index_Page extends NathandelanePage {
	
	public function _Index_Page() {
		parent::__construct("Nathandelane.com");
	}
	
}

$page = new _Index_Page();
$page->openDocument();
$page->closeDocument();

?>