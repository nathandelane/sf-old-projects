<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/ImageLoaderControl.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "PhyleBoxBasicPage.inc.php");

class _Image_Viewer_Page extends PhyleBoxBasicPage {
		
	public $imageLoaderControl;
	
	public function _Image_Viewer_Page() {
		parent::__construct("File Manager | Image Viewer");
		
		$this->registerStylesheet("_css/file-manager.css");
		
		$this->imageLoaderControl = new ImageLoaderControl($this->getFieldValue("driveSelector"), $this->getFieldValue("currentDirectory"), $this->getFieldValue("fileName"), $this->getFieldValue("type"));
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