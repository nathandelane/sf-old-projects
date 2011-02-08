<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IPage.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/SelectDrive.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/FileManagerCommands.inc.php");

/**
 * FileManagerToolBar
 * This class represents the toolbar containing buttons for the file manager.
 * @author lanathan
 *
 */
class FileManagerToolBar implements IRenderable {
	
	private $_page;
	private $_driveSelector;
	private $_fileManagerCommands;
	
	/**
	 * Constructor
	 * @param IPage $page
	 * @return FileManagerToolBar
	 */
	public function FileManagerToolBar(IPage $page) {
		$this->_page = $page;
		$this->_driveSelector = new SelectDrive($this->_page, "fileManagerForm");
		$this->_fileManagerCommands = new FileManagerCommands();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div class="toolbar">
<?php

$this->_driveSelector->render();
$this->_fileManagerCommands->render();

?>
</div>
<?php
		
	}
	
}