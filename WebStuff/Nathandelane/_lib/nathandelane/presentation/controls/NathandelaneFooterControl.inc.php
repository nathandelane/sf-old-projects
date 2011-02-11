<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Nathandelane_Config::getFrameworkRoot() . "presentation/IPage.inc.php");
require_once(Nathandelane_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * NathandelaneFooterControl
 * This class represents the Nathandealne footer on every page.
 * @author lanathan
 *
 */
class NathandelaneFooterControl implements IRenderable {
	
	/**
	 * Constructor
	 * @param IPage $page
	 * @return NathandelaneFooterControl
	 */
	public function NathandelaneFooterControl(IPage $page) {
		// Do Nothing.
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div class="footer">
	<span id="copyrightNotice">&copy; 2008-<?php echo date("Y"); ?> Nathandelane.com, Nathan Lane. All Rights Reserved.</span>
</div>
<?php
		
	}
	
}

?>