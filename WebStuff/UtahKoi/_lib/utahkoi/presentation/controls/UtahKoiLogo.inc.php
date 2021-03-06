<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(UtahKoi_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * UtahKoiLogo
 * This class represents the UtahKoi.com logo.
 * @author lanathan
 *
 */
class UtahKoiLogo implements IRenderable {
	
	/**
	 * Constructor
	 * @return UtahKoiLogo
	 */
	public function UtahKoiLogo() {
		// TODO: Whatever needs to be done.
	}
	
	public function render() {
		
?>
<div id="adminLogo">
	<a href="<?php echo UtahKoi_Config::getUtahKoiRoot(); ?>/">
		<img src="/_img/Utah_Koi_Header__Flash_Images_.png" alt="UtahKoi.com" />
	</a>
</div>
<?php
		
	}
	
}

?>