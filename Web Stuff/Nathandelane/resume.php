<?php

require_once(dirname(__FILE__) . "/Config.inc.php");
require_once(Nathandelane_Config::getLocalPresentationLocation() . "NathandelanePage.inc.php");

class _Resume_Page extends NathandelanePage {
	
	public function _Resume_Page() {
		parent::__construct("My Resume | Nathandelane.com");
	}
	
}

$page = new _Resume_Page();
$page->openDocument();
$page->closeDocument();

?>