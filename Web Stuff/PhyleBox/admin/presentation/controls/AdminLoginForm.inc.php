<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Admin_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(Admin_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * AdminLoginForm
 * This class renders the maintainable Admin login form for PhyerNet.
 * @author lanathan
 *
 */
final class AdminLoginForm implements IRenderable {
	
	private $_actionUrl;
	
	/**
	 * Constructor
	 * @param string $actionUrl
	 * @return AdminLoginForm
	 */
	public function AdminLoginForm(/*string*/ $actionUrl) {
		ArgumentTypeValidator::isString($actionUrl, "ActionUrl must be a string.");
		
		$this->_actionUrl = $actionUrl;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
			<div id="phyerNetAdminLoginForm">
				<form id="loginForm" action="<?php echo "$this->_actionUrl"; ?>" method="post">
					<div class="formRow">
						<label for="userName">User Name:</label>
						<input id="userName" name="userName" value="<?php echo $_REQUEST["userName"]; ?>" type="text" />
					</div>
					<div class="formRow">
						<label for="password">Password:</label>
						<input id="password" name="password" value="" type="password" />
					</div>
					<div class="formRow">
						<input type="submit" class="submit" value="Log In" />
					</div>
				</form>
			</div>
<?php
		
	}
	
}

?>