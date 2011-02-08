<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyerNet_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * PhyleBoxLoginForm
 * This class renders the maintainable Phyle-Box login form for PhyerNet.
 * @author lanathan
 *
 */
final class PhyleBoxLoginForm implements IRenderable {
	
	/**
	 * Constructor
	 * @return PhyleBoxLoginForm
	 */
	public function PhyleBoxLoginForm() {
		// TODO: Whatever needs to be done.
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
			<div id="phyleBoxLoginForm">
				<form id="loginForm" action="" method="post">
					<div class="formRow">
						<label for="userName">User Name:</label>
						<input id="userName" name="userName" value="" type="text" />
					</div>
					<div class="formRow">
						<label for="password">Password:</label>
						<input id="password" name="password" value="" type="password" />
					</div>
					<div class="formRow">
						<input class="checkBox" type="checkbox" id="goToPhyleBox" name="goToPhyleBox" checked="checked" />
						<label class="checkBoxLabel" for="goToPhyleBox">Go to PhyleBox on Login</label>
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