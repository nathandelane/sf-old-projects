<?php

require_once(dirname(__FILE__) . "/../_lib/utahkoi/Config.inc.php");
require_once(UtahKoi_Config::getLocalPresentationLocation() . "UtahKoiPage.inc.php");

/**
 * Index page class codebehind for index.php.
 * @author Nathamberlane
 */
class _Index_Page extends UtahKoiPage {
	
	/**
	 * Constructs an instance of _Index_Page.
	 * @return _Index_Page.
	 */
	public function _Index_Page() {
		parent::__construct("Utah Koi - More than just fish, We Are Koi");
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/utahkoi/presentation/UtahKoiPage::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/utahkoi/presentation/UtahKoiPage::closeDocument()
	 */
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>