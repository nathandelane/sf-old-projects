<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Assert.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandler.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandlerType.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "types/DriveType.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "PhyleBoxBasicPage.inc.php");

/**
 * _File_Properties_Page
 * This class presents the file properties for a specific file and allows the user to change them.
 * @author nalane
 *
 */
class _File_Properties_Page extends PhyleBoxBasicPage {
	
	public function _File_Properties_Page() {
		parent::__construct("Edit File Properties | PhyleBox");
		
		$this->registerStylesheet("_css/mime-types.css");
		$this->registerStylesheet("_css/file-properties.css");
	}
	
	public function openDocument() {
		parent::openDocument();
	}
	
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>