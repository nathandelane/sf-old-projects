<?php


if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandler.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandlerType.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "PhyleBoxPage.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/FileManagerToolBar.inc.php");

/**
 * _FileManager_Page
 * This page has to do with the administration of the user files and directories.
 * @author lanathan
 *
 */
class _FileManager_Page extends PhyleBoxPage {
	
	private $_toolBar;
	
	/**
	 * Constructor
	 * @return _FileManager_Page
	 */
	public function _FileManager_Page() {
		parent::__construct("File Manager | PhyleBox");
		
		$this->_breadcrumb->setBreadcrumb(array("Home" => PhyleBox_Config::getPhyleBoxRoot(), "File Manager" => null));
		$this->_toolBar = new FileManagerToolBar($this);
		
		$this->registerStylesheet("_css/file-manager.css");
	}
	
	/**
	 * (non-PHPdoc)
	 * @see phyle-box/presentation/PhyleBoxPage::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
		
?>
<form id="fileManagerForm" action="<?php echo $_SERVER["REQUEST_URI"]; ?>" method="post" enctype="multipart/form-data">
<?php
		
		$this->_toolBar->render();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see phyle-box/presentation/PhyleBoxPage::closeDocument()
	 */
	public function closeDocument() {
		
?>
</form>
<?php
		parent::closeDocument();
	}

}

?>