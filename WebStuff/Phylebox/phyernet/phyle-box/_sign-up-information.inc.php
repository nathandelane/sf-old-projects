<?php

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandler.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandlerType.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/controls/Content.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "controls/BetaDisclaimer.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "PhyleBoxNonAuthenticationPage.inc.php");

/**
 * _Sign_Up_Information_Page
 * This class represents the login page.
 * @author lanathan
 *
 */
class _Sign_Up_Information_Page extends PhyleBoxNonAuthenticationPage {
	
	private static $__queryHandler;
	private static $__explicitness;
	private static $__countries;
	private static $__whyWeTrackExplicitness;
	private static $__whyWeTrackYourBirthdate;
	private static $__betaDisclaimer;
	
	/**
	 * Constructor
	 * @return _Sign_Up_Information_Page
	 */
	public function _Sign_Up_Information_Page() {
		parent::__construct("Sign Up - Information | PhyleBox");
		
		$this->registerStylesheet("/_css/jquery.fancybox.css");
		$this->registerScript("/_js/jquery.fancybox.js");
		$this->registerScript("/_js/jquery.maskedinput.js");
		$this->registerScript("/_js/jquery.watermark.js");
		$this->registerScript("/_js/Phyer.js");
		$this->registerScript("_js/Registration.js");
		
		if (!isset(self::$__queryHandler)) {
			self::$__queryHandler = QueryHandler::getInstance(QueryHandlerType::MYSQL);
		}
		
		if (!isset(self::$__explicitness)) {
			$query = "select e.explicity_id, e.name, e.start_age from `pbox`.`explicity` e order by start_age asc";
			
			self::$__explicitness = self::$__queryHandler->executeQuery($query);
		}
		
		if (!isset(self::$__countries)) {
			$query = "select c.numcode, c.printable_name from `pbox`.`countries` c order by printable_name asc";
			
			self::$__countries = self::$__queryHandler->executeQuery($query);
		}
		
		if (!isset(self::$__whyWeTrackExplicitness)) {
			self::$__whyWeTrackExplicitness = new Content("why_do_we_track_explicity");
		}
		
		if (!isset(self::$__whyWeTrackYourBirthdate)) {
			self::$__whyWeTrackYourBirthdate = new Content("why_do_we_need_your_birthdate");
		}
		
		if (!isset(self::$__betaDisclaimer)) {
			self::$__betaDisclaimer = new BetaDisclaimer();
		}
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/Page::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/Page::closeDocument()
	 */
	public function closeDocument() {
		
?>
<script type="text/javascript">
	$.watermark.options = {
		className: "watermark",
		useNative: false,
		hideBeforeUnload: true
	};

	$(document).ready(function() {
		$("#password").watermark("Password");
		$("#repeatPassword").watermark("Repeat Password");
		$("#dateOfBirth").mask("99/99/9999");
		$("#dateOfBirth").watermark("mm/dd/yyyy");
		$("#firstRealName").watermark("First Name");
		$("#lastRealName").watermark("Last Name");
		$("#emailAddress").watermark("Email Address");
		$("#repeatEmailAddress").watermark("Repeat Email Address");
		$("#bio").watermark("Please enter something about yourself. This information will appear on your public profile. All HTML will be converted to text.");
		$("#otherCountry").watermark("Enter unlisted country name here");

		$("#country").change(function(e) {
			if ($("#country").val() == "10001") {
				$("#otherCountry").attr("style", "");
			} else {
				$("#otherCountry").attr("style", "display: none;");
			}
		});

		$("#repeatPassword").blur(function(e) {
			alert("");
			if ($("#password").val() != $("#repeatPassword").val()) {
				$("#repeatPassword").attr("style", "background-color: #800000;");
			} else {
				$("#repeatPassword").attr("style", "background-color: #ffffff;");
			}
		});

		$("#whyExplicityLink").click(function(e) {
			$.fancybox(
				"<?php self::$__whyWeTrackExplicitness->render(); ?>",
				{
					"autoDimensions": true,
					"transitionIn": "none",
					"transitionOut": "none"
				}
			);
		});

		$("#whyDateOfBirthIsRequiredLink").click(function(e) {
			$.fancybox(
				"<?php self::$__whyWeTrackYourBirthdate->render(); ?>",
				{
					"autoDimensions": true,
					"transitionIn": "none",
					"transitionOut": "none"
				}
			);
		});

		$("#checkUserNameLink").click(function(e) {
			$Phyer.Registration.checkUserNameAvailability();
		});
	});
</script>
<?php
		
		parent::closeDocument();
	}
	
	/**
	 * renderCountryOptions
	 * Renders the country options from the database.
	 */
	public function renderCountryOptions() {
		foreach (self::$__countries as $nextCountry) {
				
?>
<option value="<?php echo $nextCountry["numcode"]; ?>">
	<?php echo $nextCountry["printable_name"]; ?>
</option>
<?php
				
		}
	}
	
	/**
	 * renderExplicitness
	 * Renders the explicitness options.
	 */
	public function renderExplicitness() {
		foreach (self::$__explicitness as $nextExplicitness) {
			
?>
<div class="formRow">
	<input class="radio" type="radio" name="explicitness" id="explicitness<?php echo $nextExplicitness["explicity_id"]; ?>" value="<?php echo $nextExplicitness["explicity_id"]; ?>" />
	<label for="explicitness<?php echo $nextExplicitness["explicity_id"]; ?>"><?php echo $nextExplicitness["name"]; ?> (<?php if (Strings::equals($nextExplicitness["start_age"], "0")) { echo "All Ages"; } else { echo "Age " . $nextExplicitness["start_age"] . "+"; } ?>)</label>
</div>
<?php
			
		}
	}
	
	/**
	 * renderBetaDisclaimer
	 * Renders the beta disclaimer.
	 */
	public function renderBetaDisclaimer() {
		self::$__betaDisclaimer->render();
	}
	
}

?>