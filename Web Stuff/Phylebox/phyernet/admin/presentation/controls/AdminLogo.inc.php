<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Admin_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * AdminLogo
 * This class represents the PhyerNet admin logo.
 * @author lanathan
 *
 */
class AdminLogo implements IRenderable {
	
	/**
	 * Constructor
	 * @return AdminLogo
	 */
	public function AdminLogo() {
		// TODO: Whatever needs to be done.
	}
	
	public function render() {
		
?>
<div id="adminLogo">
	<a href="<?php echo Admin_Config::getAdminRoot(); ?>/">
		<img src="_img/AdminLogo.png" alt="PhyerNet Admin" />
	</a>
</div>
<?php
		
	}
	
}

?>