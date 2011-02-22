<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_Once(PhyleBox_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_Once(PhyleBox_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

class BetaDisclaimer implements IRenderable {

	/**
	 * Constructor
	 * Creates an instance of BetaDiscalimer.
	 * @return BetaDisclaimer
	 */
	public function BetaDisclaimer() {
		// TODO: Whatever is required to do here.
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div id="betaDisclaimer">
	<p>By checking the "I agree" checkbox and clicking the "Finish Sign Up" button below, you are agreeing to abide by the following terms and conditions governing participation in the PhyerNet beta service.</p>
	<p>This agreement only covers the usage of the working services hosted under PhyerNet. The services include, but are not limited to the ability to interact with the server and manage files within the newly designed user interface ("PhyleBox") and the XMPP chat services. This agreement is for the beta test phase of what will be the new interface for users who wish to use PhyerNet's hosting services.</p>
	<p>In order to sign up and use these and other services provided by PhyerNet, you must fully read and agree to the following guidelines:</p>
	<ol>
		<li>This is only a "beta". The final version of this software may not resemble or retain any of the look and feel of the current setup.</li>
		<li>You confirm that you are at least 13 years old or older and that you have received the consent of a legal guardian if you are not at least 18 years old.</li>
		<li>You validate that the information placed in the signup form is your own and that it is accurate to the best of your knowledge.</li>
		<li>This service comes with no warranty. Therefore, you agree to hold Phyer, PhyerNet and all staff harmless for any technical, personal or emotional damages received while using this service and for any data that becomes corrupted or is lost.
		<li>You will not attempt any form of direct or indirect sabotage of the service.</li>
	</ol>
	<p>If you agree to the above terms. Please check the "I agree" checkbox below and continue the registration process. If you disagree with any of these terms, please <a href="http://www.phyer.net/">exit the registration process now</a>. If you have any questions about the terms or any other aspect of this agreement or PhyerNet services, then please email helpdesk@phyer.net.</p>
	<input type="checkbox" name="iagreetobetarules" id="iagreetobetarules" />
	<label for="iagreetobetarules">I agree to the terms and conditions of the beta test.</label>
</div>
<?php
		
	}
	
}

?>