<?php

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "PhyleBoxPage.inc.php");

class _Index_Page extends PhyleBoxPage {
	
	public function _Index_Page() {
		parent::__construct("Welcome to PhyleBox, a Service of PhyerNet");
		
		$this->registerStylesheet("_css/index.css");
	}
	
	/**
	 * (non-PHPdoc)
	 * @see phyle-box/presentation/PhyleBoxPage::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see phyle-box/presentation/PhyleBoxPage::closeDocument()
	 */
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>