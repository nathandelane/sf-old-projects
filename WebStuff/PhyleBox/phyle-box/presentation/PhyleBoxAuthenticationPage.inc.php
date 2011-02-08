<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/AuthenticationPage.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/PhyleBoxHeaderControl.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/PhyleBoxFooterControl.inc.php");

/**
 * PhyleBoxAuthenticationPage
 * This class represents a phyle-box authentication page.
 * @author lanathan
 *
 */
abstract class PhyleBoxAuthenticationPage extends AuthenticationPage {
	
	private static $__phyleBoxHeaderControl;
	private static $__phyleBoxFooterControl;

	/**
	 * Constructor
	 * @return PhyleBoxAuthenticationPage
	 */
	public function PhyleBoxAuthenticationPage(/*string*/ $title, /*string*/ $redirectUrl, /*string*/ $salt) {
		parent::__construct($title, $redirectUrl, $salt);
		
		$this->registerStylesheet("_css/main.css");
		$this->registerStylesheet("_css/login.css");
		
		if (!isset(self::$__phyleBoxHeaderControl)) {
			self::$__phyleBoxHeaderControl = new PhyleBoxHeaderControl($this);
		}
		
		if (!isset(self::$__phyleBoxFooterControl)) {
			self::$__phyleBoxFooterControl = new PhyleBoxFooterControl($this);
		}
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/AuthenticationPage::getAuthenticationQuery()
	 */
	public function getAuthenticationQuery() {
		$this->_logger->sendMessage(LOG_DEBUG, "Inside of GetAuthenticationQuery");
		
		return 'select * from `pbox`.`people` where user_name = \'%1$s\' and password = \'%2$s\'';
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/AuthenticationPage::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
		
?>
<div class="root">
<?php

		self::$__phyleBoxHeaderControl->render();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/AuthenticationPage::closeDocument()
	 */
	public function closeDocument() {
		self::$__phyleBoxFooterControl->render();
		
?>
</div>
<?php

		parent::closeDocument();
	}
	
}

?>