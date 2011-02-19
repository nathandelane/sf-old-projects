<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../Config.inc.php");
require_once(Admin_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(Admin_Config::getFrameworkRoot() . "presentation/Page.inc.php");
require_once(Admin_Config::getFrameworkRoot() . "presentation/AuthenticationPage.inc.php");
require_once(Admin_Config::getLocalPresentationLocation() . "controls/AdminHeaderControl.inc.php");
require_once(Admin_Config::getLocalPresentationLocation() . "controls/AdminFooterControl.inc.php");

/**
 * AdminPage
 * This class represents every page in PhyerNet admin portal.
 * @author lanathan
 *
 */
abstract class AdminPage extends Page {
	
	const AUTHENTICATION_PAGE_URL = "/admin/login.php";
	
	private static $__adminHeaderControl;
	private static $__adminFooterControl;
		
	/**
	 * Constructor
	 * @param string $title
	 * @return AdminPage
	 */
	public function AdminPage(/*string*/ $title) {
		parent::__construct($title);
		
		$this->registerStylesheet("_css/main.css");
		
		if (!isset(self::$__adminHeaderControl)) {
			self::$__adminHeaderControl = new AdminHeaderControl($this);
		}
		
		if (!isset(self::$__adminFooterControl)) {
			self::$__adminFooterControl = new AdminFooterControl($this);
		}
				
		$this->_checkAuthenticated();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/Page::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
		
?>
<div class="root">
	<?php self::$__adminHeaderControl->render(); ?>
	<table id="rootTable">
		<tr>
			<td>
				<ul id="navigation">
					<li>
						<a href="<?php echo Admin_Config::getAdminRoot() . "/users.php" ?>">Users</a>
					</li>
				</ul>
			</td>
			<td>
<?php

	}
	
	public function closeDocument() {
		
?>
			</td>
		</tr>
	</table>
	<?php self::$__adminFooterControl->render(); ?>
</div>
<?php
		
		parent::closeDocument();
	}
	
	/**
	 * _checkAuthenticated
	 * Checks the authentication of the current session.
	 */
	private function _checkAuthenticated() {
		if(!($this->getSessionFieldValue(AuthenticationPage::AUTHENTICATION_KEY) && Strings::equals($this->getSessionFieldValue(AuthenticationPage::AUTHENTICATION_KEY), session_id()))) {
			$url = AdminPage::AUTHENTICATION_PAGE_URL . "?" . AuthenticationPage::REFERRER_KEY . "=" . $_SERVER["REQUEST_URI"];
			
			header("Location: $url");
		}
	}
	
}

?>