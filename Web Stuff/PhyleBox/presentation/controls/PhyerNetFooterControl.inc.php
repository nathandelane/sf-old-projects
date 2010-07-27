<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyerNet_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(PhyerNet_Config::getLocalPresentationLocation() . "controls/LogoControl.inc.php");

/**
 * PhyerNetFooterControl
 * This class renders the PhyerNet footer.
 * @author lanathan
 *
 */
final class PhyerNetFooterControl implements IRenderable {
	
	private $_logoControlsArray;
	
	/**
	 * Constructor
	 * @param IPage $page
	 * @return PhyerNetFooterControl
	 */
	public function PhyerNetFooterControl(IPage $page) {
		$page->registerStylesheet("_css/footer.css");
			
		$this->_initializeLogos();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() 	{
		$year = date("Y");
		
?>
		<div id="footer">
			<fieldset id="technology">
				<legend>PhyerNet is Powered by:</legend>
				<div id="technologyStack">
<?php

		foreach ($this->_logoControlsArray as $nextLogo) {
			$nextLogo->render();
		}

?>
				</div>
			</fieldset>
			<div class="root">&copy; PhyerNet 2008-<?php echo "$year"; ?>. All Rights Reserved.</div>
		</div>
<?php
		
	}	
	
	/**
	 * _initializeLogos
	 * Initializes the logo controls.
	 * @return void
	 */
	private function _initializeLogos() {
		$this->_logoControlsArray = array();
		$this->_logoControlsArray[] = new LogoControl("http://www.aprelium.com/", "_img/abysslogo.png", "abyssLogo", "Abyss Web Server");
		$this->_logoControlsArray[] = new LogoControl("http://www.pclinuxos.com/", "_img/pcloslogo.png", "pclosLogo", "PC Linux OS");
		$this->_logoControlsArray[] = new LogoControl("http://www.igniterealtime.org/projects/openfire/index.jsp", "_img/openfirelogo.png", "openfireLogo", "Open Fire");
		$this->_logoControlsArray[] = new LogoControl("http://validator.w3.org/check?uri=referer", "//www.w3.org/Icons/valid-xhtml10-blue", "validXhtml", "W3C Valid XHTML 1.0");
		$this->_logoControlsArray[] = new LogoControl("", "_img/_grafx_valid-rss.png", "validRss", "Valid RSS");
//		$this->_logoControlsArray[] = new LogoControl("", "_img/securedByRapidSSL.png", "reapidSslLogo", "Secured by RapidSSL");
	}
	
}

?>