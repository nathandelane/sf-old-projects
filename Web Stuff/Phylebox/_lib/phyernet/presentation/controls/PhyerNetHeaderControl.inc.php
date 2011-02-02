<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(dirname(__FILE__) . "/PhyerNetLogo.inc.php");
require_once(dirname(__FILE__) . "/PhyerNetNavigationControl.inc.php");
require_once(PhyerNet_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(PhyerNet_Config::getFrameworkRoot() . "phyle-box/presentation/controls/PhyleBoxLoginForm.inc.php");

/**
 * PhyerNetHeaderControl
 * This class renders the PhyerNet header.
 * @author lanathan
 *
 */
final class PhyerNetHeaderControl implements IRenderable {
	
	private $_phyerNetLogo;
	private $_phyleBoxLoginForm;
	private $_phyerNetNavigationControl;
	
	/**
	 * Constructor
	 * @param IPage $page
	 * @return PhyerNetHeaderControl
	 */
	public function PhyerNetHeaderControl(IPage $page) {
		$this->_phyerNetLogo = new PhyerNetLogo();
		$this->_phyleBoxLoginForm = new PhyleBoxLoginForm(PhyerNet_Config::getPhyerNetRoot() . "/phyle-box/login.php");
		$this->_phyerNetNavigationControl = new PhyerNetNavigationControl();
		
		$page->registerStylesheet("_css/header.css");
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div id="header">
	<div class="top">
<?php

		$this->_phyerNetLogo->render();
		$this->_phyleBoxLoginForm->render();
		
?>
	</div>
	<?php $this->_phyerNetNavigationControl->render(); ?>
</div>
<?php
		
	}
	
}

?>