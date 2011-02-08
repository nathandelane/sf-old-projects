<?php

require_once(dirname(__FILE__) . "/Config.inc.php");
require_once(PhyerNet_Config::getFrameworkRoot() . "presentation/controls/Button.inc.php");
require_once(PhyerNet_Config::getLocalPresentationLocation() . "PhyerNetPage.inc.php");

/**
 * This class represents the Index page for PhyerNet.
 * @author lanathan
 *
 */
final class _Index_Page extends PhyerNetPage {
	
	public $buttons;
	
	/**
	 * Constructor
	 * @return _Index_Page
	 */
	public function _Index_Page() {
		parent::__construct("Welcome to PhyerNet");
		
		$this->buttons = array();
		$this->buttons["signUpNow"] = new Button("signUpNow", "Sign Up Now",	"/services.php");
		
		$this->registerScript("_js/News.js");
	}
	
	/**
	 * (non-PHPdoc)
	 * @see presentation/PhyerNetPage#openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see presentation/PhyerNetPage#closeDocument()
	 */
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>