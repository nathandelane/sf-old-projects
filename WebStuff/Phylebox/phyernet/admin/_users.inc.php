<?php

require_once(dirname(__FILE__) . "/../../_lib/admin/Config.inc.php");
require_once(Admin_Config::getLocalPresentationLocation() . "AdminPage.inc.php");
require_once(Admin_Config::getLocalFoundationLocation() . "data/Users.inc.php");

class _Users_Page extends AdminPage {
	
	private static $__users;
	
	/**
	 * Constructor
	 * Creates an instance of _Users_Page.
	 * @return _Users_Page::
	 */
	public function _Users_Page() {
		parent::__construct("PhyerNet Admin | Users");
		
		self::$__users = new Users();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see admin/presentation/AdminPage::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see admin/presentation/AdminPage::closeDocument()
	 */
	public function closeDocument() {
		parent::closeDocument();
	}
	
	/**
	 * renderUsers
	 * Renders the users in the Users model as tabular data.
	 */
	public function renderUsers() {
		$usersEnumerator = self::$__users->getEnumerator();
		
		while ($nextUser = $usersEnumerator->moveNext()) {
			
?>
<tr>
	<td>
		<?php echo "{$nextUser['user_name']}"; ?>
	</td>
	<td>
		<?php echo "{$nextUser['first_real_name']}"; ?>
	</td>
	<td>
		<?php echo "{$nextUser['last_real_name']}"; ?>
	</td>
	<td>
		<?php echo "{$nextUser['is_disabled_by_phyer']}"; ?>
	</td>
	<td>
		<?php echo "{$nextUser['disabled_reason']}"; ?>
	</td>
	<td>
		<?php echo "{$nextUser['is_locked']}"; ?>
	</td>
	<td>
		<?php echo "{$nextUser['content_type']}"; ?>
	</td>
	<td>
		<?php echo "{$nextUser['signature']}"; ?>
	</td>
	<td>
		<?php echo "{$nextUser['account_type']}"; ?>
	</td>
	<td>
		<?php echo "{$nextUser['date_of_birth']}"; ?>
	</td>
	<td>
		<?php echo "{$nextUser['merits']}"; ?>
	</td>
	<td>
		<?php echo "{$nextUser['date_created']}"; ?>
	</td>
</tr>
<?php
			
		}		
	}
	
}

?>