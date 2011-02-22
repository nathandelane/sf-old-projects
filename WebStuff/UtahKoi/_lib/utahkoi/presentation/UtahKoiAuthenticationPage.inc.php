<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(UtahKoi_Config::getFrameworkRoot() . "presentation/AuthenticationPage.inc.php");
require_once(UtahKoi_Config::getFrameworkRoot() . "presentation/IPage.inc.php");

/**
 * UtahKoiAuthenticationPage
 * This class represents an authentication page for UtahKoi.
 * This is artificial since UtahKoi.com has no authentication pages.
 * @author lanathan
 *
 */
abstract class UtahKoiAuthenticationPage extends AuthenticationPage {
	
	/**
	 * Constructor
	 * @return UtahKoiAuthenticationPage
	 */
	public function UtahKoiAuthenticationPage(/*string*/ $title, /*string*/ $redirectUrl, /*string*/ $salt) {
		parent::__construct($title, $redirectUrl, $salt, "ticket-utahkoi");
		
		$this->registerStylesheet("_css/main.css");
		$this->registerStylesheet("_css/login.css");
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/AuthenticationPage::getAuthenticationQuery()
	 */
	public function getAuthenticationQuery() {
		$this->_logger->sendMessage(LOG_DEBUG, "Inside of GetAuthenticationQuery");
		
		return 'select * from `crm_nathandelane`.`contact_security` ci where ci.username = \'%2$s\' and ci.password = \'%1$s\' and ci.contact_locked_out = \'False\'';
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

	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/AuthenticationPage::closeDocument()
	 */
	public function closeDocument() {
		
?>
</div>
<?php

		parent::closeDocument();
	}
	
}

?>