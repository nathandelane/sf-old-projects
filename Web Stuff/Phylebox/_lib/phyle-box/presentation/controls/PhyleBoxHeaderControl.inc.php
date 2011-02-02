<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IPage.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/PhyleBoxLogo.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/PhyleBoxFooterSubNavigation.inc.php");

/**
 * PhyleBoxHeaderControl
 * This class represents and renders the PhyleBox header.
 * @author lanathan
 *
 */
class PhyleBoxHeaderControl implements IRenderable {
	
	private $_phyleBoxLogo;
	private $_subNavigation;
	
	/**
	 * Constructor
	 * @param IPage $page
	 * @return PhyleBoxHeaderControl
	 */
	public function PhyleBoxHeaderControl(IPage $page) {
		$this->_phyleBoxLogo = new PhyleBoxLogo();
		$this->_subNavigation = new PhyleBoxFooterSubNavigation();
		
		$page->registerStylesheet("_css/header.css");
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div class="header">
<?php

	$this->_phyleBoxLogo->render();
	$this->_subNavigation->render();
	
?>
</div>
<?php
		
	}
	
}

?>