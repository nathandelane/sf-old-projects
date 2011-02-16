<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(UtahKoi_Config::getFrameworkRoot() . "presentation/IPage.inc.php");
require_once(UtahKoi_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(UtahKoi_Config::getLocalPresentationLocation() . "controls/UtahKoiLogo.inc.php");

/**
 * UtahKoiHeaderControl
 * This class represents and renders the UtahKoi.com header.
 * @author lanathan
 *
 */
class UtahKoiHeaderControl implements IRenderable {
	
	private $_utahKoiLogo;
	
	/**
	 * Constructor
	 * @param IPage $page
	 * @return UtahKoiHeaderControl
	 */
	public function UtahKoiHeaderControl(IPage $page) {
		$this->_utahKoiLogo = new UtahKoiLogo();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div class="header">
	<?php $this->_utahKoiLogo->render(); ?>
</div>
<?php
		
	}
	
}

?>