<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IPage.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/PhyleBoxFooterSubNavigation.inc.php");

/**
 * PhyleBoxFooterControl
 * This class represents the PhyleBox footer on every page.
 * @author lanathan
 *
 */
class PhyleBoxFooterControl implements IRenderable {
	
	private static $__phyleBoxFooterSubNavigation;
	
	/**
	 * Constructor
	 * @param IPage $page
	 * @return PhyleBoxFooterControl
	 */
	public function PhyleBoxFooterControl(IPage $page) {
		$page->registerStylesheet("_css/footer.css");
		
		self::$__phyleBoxFooterSubNavigation = new PhyleBoxFooterSubNavigation();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div class="footer">
	<span id="copyrightNotice">&copy; 2008-<?php echo date("Y"); ?> PhyerNet. All Rights Reserved.</span>
	<?php self::$__phyleBoxFooterSubNavigation->render(); ?>
</div>
<?php
		
	}
	
}

?>