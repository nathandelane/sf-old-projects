<?php

require_once(dirname(__FILE__) . "/../../_lib/admin/Config.inc.php");
require_once(Admin_Config::getLocalPresentationLocation() . "AdminPage.inc.php");

class _Index_Page extends AdminPage {
	
	public function _Index_Page() {
		parent::__construct("PhyerNet Admin", $this);
	}
	
	/**
	 * (non-PHPdoc)
	 * @see admin/presentation/AdminPage::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see admin/presentation/AdminPage::closeDocument()
	 */
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>