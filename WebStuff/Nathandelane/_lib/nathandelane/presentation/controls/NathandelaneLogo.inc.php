<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Nathandelane_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * NathandelaneLogo
 * This class represents the Nathandelane logo.
 * @author lanathan
 *
 */
class NathandelaneLogo implements IRenderable {
	
	/**
	 * Constructor
	 * @return NathandelaneLogo
	 */
	public function NathandelaneLogo() {
		// TODO: Whatever needs to be done.
	}
	
	public function render() {
		
?>
<div id="nathandelaneLogo">
	<a href="<?php echo Nathandelane_Config::getNathandelaneRoot(); ?>/">
		<img src="/_img/NathandelaneLogo.png" alt="Nathandelane.com" />
	</a>
</div>
<?php
		
	}
	
}

?>