<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(UtahKoi_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(UtahKoi_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(UtahKoi_Config::getFrameworkRoot() . "presentation/IPage.inc.php");

/**
 * UtahKoiLoginForm
 * This class renders the maintainable login form for UtahKoi.
 * @author lanathan
 *
 */
final class UtahKoiLoginForm implements IRenderable {
	
	private $_page;
	private $_actionUrl;
	
	/**
	 * Constructor
	 * @param string $actionUrl
	 * @return UtahKoiLoginForm
	 */
	public function UtahKoiLoginForm(IPage $page, /*string*/ $actionUrl) {
		ArgumentTypeValidator::isString($actionUrl, "ActionUrl must be a string.");
		
		$this->_page = $page;
		$this->_actionUrl = $actionUrl;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
			<div id="nathandelaneClientLoginForm">
				<form id="loginForm" action="<?php echo "$this->_actionUrl"; ?>" method="post">
					<div class="formRow">
						<label for="userName">User Name:</label>
						<input id="userName" name="userName" value="<?php echo $this->_page->getFieldValue("userName"); ?>" type="text" />
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