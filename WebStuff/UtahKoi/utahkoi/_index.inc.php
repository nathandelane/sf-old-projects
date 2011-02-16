<?php

require_once(dirname(__FILE__) . "/../_lib/utahkoi/Config.inc.php");
require_once(UtahKoi_Config::getLocalPresentationLocation() . "UtahKoiPage.inc.php");

/**
 * Index page class codebehind for index.php.
 * @author Nathamberlane
 */
class _Index_Page extends UtahKoiPage {
	
	/**
	 * Constructs an instance of _Index_Page.
	 * @return _Index_Page.
	 */
	public function _Index_Page() {
		parent::__construct("Utah Koi - More than just fish, We Are Koi");
		
		$this->registerKeywords(array("Utah Koi", "Imported Japanese koi, LLC", "Nexus Eazy 310", 
			"Nexus Eazy 210", "Arestian Pro Pump", "Koi Supplies - Koi dealer", "Junior Koi Club for Kids",
			"Koi Ponds", "Koi Pond Supplies", "Aqua Ultraviolet", "Fluidart Filters", "Challenger Filters",
			"sonic solutions ultrasound for alage control", "Sensaphone Montoring your flilter system",
			"sensaphone to set alarms for you filter system for koi ponds", "Savio Skimmers", "EVOUV Ultraviolet Light",
			"Evolution Aqua", "EVO30 30 watt", "EVO55 Ultraviolet Light 55 watt", "EVO110 Ultraviolet Light 110 watt",
			"Nexus Eazy Pond Filtration", "Savio UV Lights", "Aqua Ultraviole Uv Lights", "Aqua Ultraviolet Ultima II Filters",
			"Easy Pro Pumps", "Easy Pro Skimmmers", "Danner Pumps", "Artestain Pumps", "Artestian Pro Pumps",
			"Microbelift", "Prazi", "Praziquentel", "Prazi Pond", "Aquazyme", "Proform C", "Potassium Permagenate",
			"Koizyme", "Aqua Medizyme", "Underwater Lights for Ponds"));
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/utahkoi/presentation/UtahKoiPage::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/utahkoi/presentation/UtahKoiPage::closeDocument()
	 */
	public function closeDocument() {
		parent::closeDocument();
	}
	
}

?>