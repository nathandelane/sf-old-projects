<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyerNet_Config::getFrameworkRoot() . "/presentation/IRenderable.inc.php");

/**
 * LogoControl
 * This class represents linkable logos.
 * @author lanathan
 *
 */
class LogoControl implements IRenderable {
	
	private $_linkHref;
	private $_imageSrc;
	private $_logoId;
	private $_imageAltText;
	
	/**
	 * Constructor
	 * @return LogoControl
	 */
	public function LogoControl(/*string*/ $linkHref, /*string*/ $imageSrc, /*string*/ $logoId, /*string*/ $imageAltText) {
		$this->_linkHref = $linkHref;
		$this->_imageSrc = $imageSrc;
		$this->_logoId = $logoId;
		$this->_imageAltText =$imageAltText;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div class="footerLogo" id="<?php echo "$this->_logoId"; ?>">
	<a href="<?php echo "$this->_linkHref"; ?>">
		<img src="<?php echo "$this->_imageSrc"; ?>" alt="<?php echo "$this->_imageAltText"; ?>" />
	</a>
</div>
<?php
		
	}
	
}

?>