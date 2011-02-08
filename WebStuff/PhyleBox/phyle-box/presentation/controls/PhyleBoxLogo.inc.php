<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * PhyleBoxLogo
 * This class represents the PhyleBox logo.
 * @author lanathan
 *
 */
class PhyleBoxLogo implements IRenderable {
	
	/**
	 * Constructor
	 * @return PhyleBoxLogo
	 */
	public function PhyleBoxLogo() {
		// TODO: Whatever needs to be done.
	}
	
	public function render() {
		
?>
<div id="phyleBoxLogo">
	<a href="<?php echo PhyleBox_Config::getPhyleBoxRoot(); ?>/">
		<img src="_img/PhyleBoxLogo.png" alt="PhyleBox" />
	</a>
</div>
<?php
		
	}
	
}

?>