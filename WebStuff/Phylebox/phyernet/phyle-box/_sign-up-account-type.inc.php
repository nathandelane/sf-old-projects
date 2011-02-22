<?php

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandler.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandlerType.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/AuthenticationPage.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/controls/Content.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/BetaDisclaimer.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "PhyleBoxNonAuthenticationPage.inc.php");

/**
 * _Sign_Up_Account_Type_Page
 * This class represents the account type page in the sign up.
 * @author lanathan
 *
 */
class _Sign_Up_Account_Type_Page extends PhyleBoxNonAuthenticationPage {
	
	public $InvalidFields;
	
	public function _Sign_Up_Account_Type_Page() {
		parent::__construct("Sign Up - Account Type | PhyleBox");
		
		$this->InvalidFields = array();
		
		$this->registerStylesheet("_css/sign-up.css");
		$this->registerScript("/_js/Phyer.js");
		$this->registerScript("_js/Registration.js");
	}
	
	public function openDocument() {
		parent::openDocument();
	}
	
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>