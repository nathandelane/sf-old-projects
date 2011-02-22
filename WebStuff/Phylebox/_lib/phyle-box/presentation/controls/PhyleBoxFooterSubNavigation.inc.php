<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IPage.inc.php");

/**
 * PhyleBoxFooterSubNavigation
 * This class represents the footer sub navigation in PhyleBOx
 * @author lanathan
 *
 */
class PhyleBoxFooterSubNavigation implements IRenderable {
	
	private $_page;
	
	/**
	 * Constructor
	 * @param IPage $page
	 * @return PhyleBoxFooterSubNavigation
	 */
	public function PhyleBoxFooterSubNavigation(IPage $page) {
		$this->_page = $page;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div id="footerSubNavigation">
	<ul>
		<li>
			<a href="<?php echo PhyleBox_Config::getPhyleBoxRoot(); ?>/../">Return to PhyerNet</a>
		</li>
<?php

		if (isset($_SESSION[$this->_page->getAuthenticationKey()]) && Strings::equals($_SESSION[$this->_page->getAuthenticationKey()], session_id())) {

?>
		<li>
			<a href="<?php echo PhyleBox_Config::getPhyleBoxRoot(); ?>/file-manager.php">File Manager</a>
		</li>
<!--		<li>
			<a href="<?php echo PhyleBox_Config::getPhyleBoxRoot(); ?>/profile.php">Profile</a>
		</li>
-->		<li>
			<a href="<?php echo PhyleBox_Config::getPhyleBoxRoot(); ?>/logout.php">Logout</a>
		</li>
<?php

		}

?>
	</ul>
</div>
<?php
		
	}
	
}

?>