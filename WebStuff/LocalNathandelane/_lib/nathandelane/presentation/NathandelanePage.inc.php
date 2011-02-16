<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../Config.inc.php");
require_once(Nathandelane_Config::getFrameworkRoot() . "presentation/Page.inc.php");
require_once(Nathandelane_Config::getLocalPresentationLocation() . "controls/NathandelaneHeaderControl.inc.php");
require_once(Nathandelane_Config::getLocalPresentationLocation() . "controls/NathandelaneFooterControl.inc.php");

/**
 * NathandelanePage
 * This class represents every page on Nathandelane.
 * @author lanathan
 *
 */
abstract class NathandelanePage extends Page {
	
	private $_nathandelaneHeaderControl;
	private $_nathandelaneFooterControl;
	
	/**
	 * Constructor
	 * @param string $title
	 * @return NathandelanePage
	 */
	public function NathandelanePage(/*string*/ $title) {
		parent::__construct($title);
		
		$this->_nathandelaneHeaderControl = new NathandelaneHeaderControl($this);
		$this->_nathandelaneFooterControl = new NathandelaneFooterControl($this);
		
		$this->registerStylesheet("/_css/main.css");		
		$this->registerScript("/_js/jquery.js");
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

		$this->_nathandelaneHeaderControl->render();
		
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
		$this->_nathandelaneFooterControl->render();
		
?>
		</div>
<?php
		parent::closeDocument();
	}
	
}

?>