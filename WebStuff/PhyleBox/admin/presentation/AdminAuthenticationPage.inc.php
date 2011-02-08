<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Admin_Config::getFrameworkRoot() . "presentation/AuthenticationPage.inc.php");
require_once(Admin_Config::getLocalPresentationLocation() . "controls/AdminHeaderControl.inc.php");
require_once(Admin_Config::getLocalPresentationLocation() . "controls/AdminFooterControl.inc.php");

/**
 * AdminAuthenticationPage
 * This class represents a PhyerNet admin authentication page.
 * @author lanathan
 *
 */
abstract class AdminAuthenticationPage extends AuthenticationPage {
	
	private static $__adminHeaderControl;
	private static $__adminFooterControl;

	/**
	 * Constructor
	 * @return AdminAuthenticationPage
	 */
	public function AdminAuthenticationPage(/*string*/ $title, /*string*/ $redirectUrl, /*string*/ $salt) {
		parent::__construct($title, $redirectUrl, $salt);
		
		$this->registerStylesheet("_css/main.css");
		$this->registerStylesheet("_css/login.css");
		
		if (!isset(self::$__adminHeaderControl)) {
			self::$__adminHeaderControl = new AdminHeaderControl($this);
		}
		
		if (!isset(self::$__adminFooterControl)) {
			self::$__adminFooterControl = new AdminFooterControl($this);
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

		self::$__adminHeaderControl->render();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/AuthenticationPage::closeDocument()
	 */
	public function closeDocument() {
		self::$__adminFooterControl->render();
		
?>
</div>
<?php

		parent::closeDocument();
	}
	
}

?>