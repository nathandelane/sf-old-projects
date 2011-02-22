<?php

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(PhyerNet_Config::getLocalPresentationLocation() . "PhyleBoxPage.inc.php");

class _Index_Page extends PhyleBoxPage {
	
	public function _Index_Page() {
		parent::__construct("An error has occurred | PhyleBox");
	}

	/**
	 * (non-PHPdoc)
	 * @see /presentation/PhyleBoxPage::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see /presentation/PhyleBoxPage::openDocument()
	 */
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>