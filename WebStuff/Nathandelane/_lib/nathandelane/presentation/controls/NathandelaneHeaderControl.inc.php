<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Nathandelane_Config::getFrameworkRoot() . "presentation/IPage.inc.php");
require_once(Nathandelane_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(Nathandelane_Config::getLocalPresentationLocation() . "controls/NathandelaneLogo.inc.php");

/**
 * NathandelaneHeaderControl
 * This class represents and renders the Nathandelane header.
 * @author lanathan
 *
 */
class NathandelaneHeaderControl implements IRenderable {
	
	private $_nathandelaneLogo;
	
	/**
	 * Constructor
	 * @param IPage $page
	 * @return NathandelaneHeaderControl
	 */
	public function NathandelaneHeaderControl(IPage $page) {
		$this->_nathandelaneLogo = new NathandelaneLogo();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div id="header">
	<?php $this->_nathandelaneLogo->render(); ?>
</div>
<?php
		
	}
	
}

?>