<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../Config.inc.php");
require_once(PhyerNet_Config::getFrameworkRoot() . "presentation/Page.inc.php");
require_once(PhyerNet_Config::getLocalPresentationLocation() . "controls/PhyerNetHeaderControl.inc.php");
require_once(PhyerNet_Config::getLocalPresentationLocation() . "controls/PhyerNetFooterControl.inc.php");

/**
 * PhyerNetPage
 * This class represents every page on PhyerNet except for Phyle-Box.
 * @author lanathan
 *
 */
abstract class PhyerNetPage extends Page {
	
	private $_phyerNetHeaderControl;
	private $_phyerNetFooterControl;
	
	/**
	 * Constructor
	 * @param string $title
	 * @return PhyerNetPage
	 */
	public function PhyerNetPage(/*string*/ $title) {
		parent::__construct($title);
		
		$this->_phyerNetHeaderControl = new PhyerNetHeaderControl($this);
		$this->_phyerNetFooterControl = new PhyerNetFooterControl($this);
		
		$this->registerStylesheet("_css/main.css");
		
		$this->registerScript("_js/jquery.js");
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/Page::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
		
?>
		<div class="root">
<?php

		$this->_phyerNetHeaderControl->render();
		
?>
			<div class="content">
<?php

	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/Page::closeDocument()
	 */
	public function closeDocument() {

?>
			</div>
<?php	
		$this->_phyerNetFooterControl->render();
		
?>
		</div>
<?php
		parent::closeDocument();
	}
	
}

?>