<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Admin_Config::getFrameworkRoot() . "presentation/IPage.inc.php");
require_once(Admin_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * AdminFooterControl
 * This class represents the PhyerNet admin footer on every page.
 * @author lanathan
 *
 */
class AdminFooterControl implements IRenderable {
	
	/**
	 * Constructor
	 * @param IPage $page
	 * @return AdminFooterControl
	 */
	public function AdminFooterControl(IPage $page) {
		$page->registerStylesheet("_css/footer.css");
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div class="footer">
	<span id="copyrightNotice">&copy; 2008-<?php echo date("Y"); ?> PhyerNet. All Rights Reserved.</span>
</div>
<?php
		
	}
	
}

?>