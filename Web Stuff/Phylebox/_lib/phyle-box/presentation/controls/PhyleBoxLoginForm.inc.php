<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * PhyleBoxLoginForm
 * This class renders the maintainable Phyle-Box login form for PhyerNet.
 * @author lanathan
 *
 */
final class PhyleBoxLoginForm implements IRenderable {
	
	private $_actionUrl;
	private $_showGoToPhyleBox;
	
	/**
	 * Constructor
	 * @param string $actionUrl
	 * @param bool $showGoToPhyleBox
	 * @return PhyleBoxLoginForm
	 */
	public function PhyleBoxLoginForm(/*string*/ $actionUrl, /*bool*/ $showGoToPhyleBox = true) {
		ArgumentTypeValidator::isString($actionUrl, "ActionUrl must be a string.");
		ArgumentTypeValidator::isBool($showGoToPhyleBox, "ShowGoToPhyleBox must be boolean.");
		
		$this->_actionUrl = $actionUrl;
		$this->_showGoToPhyleBox = $showGoToPhyleBox;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
			<div id="phyleBoxLoginForm">
				<form id="loginForm" action="<?php echo "$this->_actionUrl"; ?>" method="post">
					<div class="formRow">
						<label for="userName">User Name:</label>
						<input id="userName" name="userName" value="<?php echo $_REQUEST["userName"]; ?>" type="text" />
					</div>
					<div class="formRow">
						<label for="password">Password:</label>
						<input id="password" name="password" value="" type="password" />
					</div>
<?php

		if ($this->_showGoToPhyleBox) {

?>
					<div class="formRow">
						<input class="checkBox" type="checkbox" id="goToPhyleBox" name="goToPhyleBox" checked="checked" />
						<label class="checkBoxLabel" for="goToPhyleBox">Go to PhyleBox on Login</label>
					</div>
<?php

		}

?>
					<div class="formRow">
						<input type="submit" class="submit" value="Log In" />
					</div>
				</form>
			</div>
<?php
		
	}
	
}

?>