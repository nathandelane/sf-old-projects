<?php

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "PhyleBoxPage.inc.php");

class _Index_Page extends PhyleBoxPage {
	
	public function _Index_Page() {
		parent::__construct("Welcome to PhyleBox, a Service of PhyerNet");
		
		$this->registerStylesheet("_css/index.css");
		$this->registerStylesheet("/_css/jquery.bt.css");
		$this->registerScript("/_js/excanvas.js");
		$this->registerScript("/_js/jquery.bgiframe.js");
		$this->registerScript("/_js/jquery.bt.js");
	}
	
	/**
	 * (non-PHPdoc)
	 * @see phyle-box/presentation/PhyleBoxPage::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see phyle-box/presentation/PhyleBoxPage::closeDocument()
	 */
	public function closeDocument() {
		
?>
<script type="text/javascript">
	$(document).ready(function() {
		$("#fileManager").bt({
			contentSelector: "$('#fileManagerContent')",
			fill: "rgba(80, 119, 157, 0.8)",
			cssStyles: { color: "#ffffff", fontWeight: "bold" },
			shrinkToFit: true,
			cornerRadius: 5,
			spikeLength: 20,
			spikeGirth: 10,
			strokeWidth: 2,
			strokeStyle: "#2A557C"
		});
		$("#profileManager").bt({
			contentSelector: "$('#profileManagerContent')",
			fill: "rgba(80, 119, 157, 0.8)",
			cssStyles: { color: "#ffffff", fontWeight: "bold" },
			shrinkToFit: true,
			cornerRadius: 5,
			spikeLength: 20,
			spikeGirth: 10,
			strokeWidth: 2,
			strokeStyle: "#2A557C"
		});
	});
</script>
<?php
		
		parent::closeDocument();
	}
	
}

?>