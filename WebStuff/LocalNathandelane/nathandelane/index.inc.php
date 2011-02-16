<?php

require_once(dirname(__FILE__) . "/../_lib/nathandelane/Config.inc.php");
require_once(Nathandelane_Config::getLocalPresentationLocation() . "NathandelanePage.inc.php");

class _Index_Page extends NathandelanePage {
	
	/**
	 * Constructor
	 * Creates an instance of _Index_Page.
	 * @Return _Index_Page
	 */
	public function _Index_Page() {
		parent::__construct("Nathandelane.com");
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/Page::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/Page::closeDocument()
	 */
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>