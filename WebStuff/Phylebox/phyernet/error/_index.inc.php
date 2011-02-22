<?php

require_once(dirname(__FILE__) . "/../../_lib/phyernet/Config.inc.php");
require_once(PhyerNet_Config::getLocalPresentationLocation() . "PhyerNetPage.inc.php");

class _Index_Page extends PhyerNetPage {
	
	public function _Index_Page() {
		parent::__construct("An error has occurred | PhyerNet");
	}

	/**
	 * (non-PHPdoc)
	 * @see /presentation/PhyerNetPage::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see /presentation/PhyerNetPage::openDocument()
	 */
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>