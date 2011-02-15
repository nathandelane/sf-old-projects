<?php

require_once(dirname(__FILE__) . "/../_lib/phyernet/Config.inc.php");
require_once(PhyerNet_Config::getLocalPresentationLocation() . "PhyerNetPage.inc.php");

class _Index_Page extends PhyerNetPage {
	
	/**
	 * Creates an instance of _Index_Page.
	 * @return _Index_Page
	 */
	public function _Index_Page() {
		parent::__construct("Welcome to PhyerNet");
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/phyernet/presentation/PhyerNetPage::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/phyernet/presentation/PhyerNetPage::closeDocument()
	 */
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>