<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "PhyleBoxPage.inc.php");

/**
 * _Upload_Files_Page
 * This page is the file upload page.
 * @author lanathan
 *
 */
class _Upload_Files_Page extends PhyleBoxPage {
	
	public $successMessage;
	
	/**
	 * Creates an instance of _Upload_Files_Page.
	 * @return _Upload_Files_Page
	 */
	public function _Upload_Files_Page() {
		parent::__construct("Upload Files | PhyleBox");
		
		if ($this->getFieldValue("token")) {
			if (count($_FILES["filesBeingUploaded"]) > 0) {
				$this->successMessage = "Uploaded: " . $_FILES["filesBeingUploaded"]["name"] . "<br />" . $_FILES["filesBeingUploaded"]["tmp_name"];
			} else {
				$this->successMessage = "No good";
			}
		}
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/phyle-box/presentation/PhyleBoxPage::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/phyle-box/presentation/PhyleBoxPage::closeDocument()
	 */
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>
