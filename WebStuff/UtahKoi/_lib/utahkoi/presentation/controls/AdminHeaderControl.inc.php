<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Admin_Config::getFrameworkRoot() . "presentation/IPage.inc.php");
require_once(Admin_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(Admin_Config::getLocalPresentationLocation() . "controls/AdminLogo.inc.php");

/**
 * AdminHeaderControl
 * This class represents and renders the PhyerNet admin header.
 * @author lanathan
 *
 */
class AdminHeaderControl implements IRenderable {
	
	private $_adminLogo;
	
	/**
	 * Constructor
	 * @param IPage $page
	 * @return AdminHeaderControl
	 */
	public function AdminHeaderControl(IPage $page) {
		$this->_adminLogo = new AdminLogo();
		
		$page->registerStylesheet("_css/header.css");
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div class="header">
	<?php $this->_adminLogo->render(); ?>
</div>
<?php
		
	}
	
}

?>