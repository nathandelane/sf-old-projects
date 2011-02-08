<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyerNet_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyerNet_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * PhyerNetNavigationControl
 * Renders the phyernet navigation.
 * @author lanathan
 *
 */
final class PhyerNetNavigationControl implements IRenderable {
	
	/**
	 * Constructor
	 * @return PhyerNetNavigationControl
	 */
	public function PhyerNetNavigationControl() {
		
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		$currentScript = "{$_SERVER["PHP_SELF"]}";
		
?>
			<div id="navigation">
				<div id="navigationPlaceHolderFront"></div>
				<ul>
					<li>
						<a <?php if (Strings::startsWith($currentScript, "/services")) { echo "class=\"current\""; } ?> href="/services.php">Services</a>
					</li>
					<li>
						<a <?php if (Strings::startsWith($currentScript, "/support")) { echo "class=\"current\""; } ?> href="/support.php">Support</a>
					</li>
					<li>
						<a <?php if (Strings::startsWith($currentScript, "/about-us")) { echo "class=\"current\""; } ?> href="/about-us.php">About Us</a>
					</li>
					<li>
						<a <?php if (Strings::startsWith($currentScript, "/account")) { echo "class=\"current\""; } ?> href="/account.php">Account</a>
					</li>
				</ul>
				<div id="navigationPlaceHolderBack"></div>
			</div>
<?php
		
	}
	
}

?>