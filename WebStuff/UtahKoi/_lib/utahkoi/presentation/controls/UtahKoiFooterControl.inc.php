<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(UtahKoi_Config::getFrameworkRoot() . "presentation/IPage.inc.php");
require_once(UtahKoi_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * UtahKoiFooterControl
 * This class represents the UtahKoi.com footer on every page.
 * @author lanathan
 *
 */
class UtahKoiFooterControl implements IRenderable {
	
	/**
	 * Constructor
	 * @param IPage $page
	 * @return UtahKoiFooterControl
	 */
	public function UtahKoiFooterControl(IPage $page) {
		// TODO: Whatever needs to be done.
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div class="footer">
	<span id="copyrightNotice">&copy; 2011 UtahKoi.com, &copy; 2011 Nathandelane.com. All Rights Reserved.</span>
</div>
<?php
		
	}
	
}

?>