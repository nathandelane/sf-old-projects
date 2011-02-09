<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/AuthenticationPage.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/GoogleAnalytics.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/PhyleBoxHeaderControl.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/PhyleBoxFooterControl.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/business/Functions.inc.php");

/**
 * PhyleBoxAuthenticationPage
 * This class represents a phyle-box authentication page.
 * @author lanathan
 *
 */
abstract class PhyleBoxAuthenticationPage extends AuthenticationPage {
	
	private static $__phyleBoxHeaderControl;
	private static $__phyleBoxFooterControl;
	private static $__googleAnalyticsControl;
	
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
		
		if (!isset(self::$__googleAnalyticsControl)) {
			self::$__googleAnalyticsControl = new GoogleAnalytics("www.phyer.net");
		}
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/AuthenticationPage::getAuthenticationQuery()
	 */
	public function getAuthenticationQuery() {
		$this->_logger->sendMessage(LOG_DEBUG, "Inside of GetAuthenticationQuery");
		
		return 'select p.* from `pbox`.`people` p inner join `pbox`.`people_roles` pr on pr.person_id = p.person_id left outer join `pbox`.`roles` r on r.role_id = pr.role_id left outer join `pbox`.`roles_functions` rf on rf.role_id = r.role_id inner join `pbox`.`functions` f on f.function_id = rf.function_id where p.user_name = \'%1$s\' and p.password = \'%2$s\' and p.is_disabled_by_phyer = \'false\' and p.is_locked = \'false\' and (p.date_deleted > NOW() || p.date_deleted = \'0000-00-00 00:00:00\') and f.name = \'' . Functions::PHYLE_BOX . '\'';
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
		self::$__googleAnalyticsControl->render();
		
?>
</div>
<?php

		parent::closeDocument();
	}
	
}

?>