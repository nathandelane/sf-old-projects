<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyerNet_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * PhyerNetLogo
 * This class represents the PhyerNet logo.
 * @author lanathan
 *
 */
class PhyerNetLogo implements IRenderable {
	
	/**
	 * Constructor
	 * @return PhyerNetLogo
	 */
	public function PhyerNetLogo() {
		// TODO: Whatever needs to be done.
	}
	
	public function render() {
		
?>
<div id="phyerNetLogo">
	<a href="<?php echo PhyerNet_Config::getPhyerNetRoot(); ?>/"></a>
</div>
<?php
		
	}
	
}

?>