<?php

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandler.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandlerType.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/AuthenticationPage.inc.php");
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
	
	const TOKEN = "token";
	const USERNAME = "username";
	const PASSWORD = "password";
	const REPEAT_PASSWORD = "repeatPassword";
	const EXPLICITNESS = "explicitness";
	const DATE_OF_BIRTH = "dateOfBirth";
	const FIRST_REAL_NAME = "firstRealName";
	const LAST_REAL_NAME = "lastRealName";
	const EMAIL_ADDRESS = "emailAddress";
	const REPEAT_EMAIL_ADDRESS = "repeatEmailAddress";
	const COUNTRY = "country";
	const OTHER_COUNTRY = "otherCountry";
	const STATE = "state";
	const CITY = "city";
	const BIO = "bio";
	const I_AGREE_TO_BETA_RULES = "iagreetobetarules";
	
	public $InvalidFields;
	
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
		
		$this->InvalidFields = array();
		
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
		
		if ($this->getFieldValue(self::TOKEN)) {
			if ($this->_registrationInformationIsValid()) {
				if ($this->_createAccount()) {				
					header("Location: " . PhyleBox_Config::getPhyleBoxRoot() . "/sign-up-upload-avatar.php");
				}
			}
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
		$("#dateOfBirth").watermark("mm-dd-yyyy");
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
	
	/**
	 * _registrationInformationIsValid
	 * Ensures that the form data is valid.
	 * @return bool
	 */
	private function _registrationInformationIsValid() {
		$isValid = true;
		
		if (Strings::isNullOrEmpty($this->getFieldValue(self::USERNAME)) || $this->_usernameIsInUse($this->getFieldValue(self::USERNAME))) {
			$this->InvalidFields[] = self::USERNAME;
			
			$isValid = false;
		}
		
		if (Strings::isNullOrEmpty($this->getFieldValue(self::PASSWORD)) || Strings::isNullOrEmpty($this->getFieldValue(self::REPEAT_PASSWORD)) || $this->getFieldValue(self::PASSWORD) != $this->getFieldValue(self::REPEAT_PASSWORD)) {
			$this->InvalidFields[] = self::PASSWORD;
			$this->InvalidFields[] = self::REPEAT_PASSWORD;
			
			$isValid = false;
		}
		
		if (Strings::isNullOrEmpty($this->getFieldValue(self::EXPLICITNESS))) {
			$this->InvalidFields[] = self::EXPLICITNESS;
			
			$isValid = false;
		}
		
		if (Strings::isNullOrEmpty($this->getFieldValue(self::DATE_OF_BIRTH))) {
			$this->InvalidFields[] = self::DATE_OF_BIRTH;
			
			$isValid = false;
		}
		
		if (Strings::isNullOrEmpty($this->getFieldValue(self::FIRST_REAL_NAME))) {
			$this->InvalidFields[] = self::FIRST_REAL_NAME;
			
			$isValid = false;
		}
		
		if (Strings::isNullOrEmpty($this->getFieldValue(self::LAST_REAL_NAME))) {
			$this->InvalidFields[] = self::LAST_REAL_NAME;
			
			$isValid = false;
		}
		
		if (Strings::isNullOrEmpty($this->getFieldValue(self::EMAIL_ADDRESS)) || Strings::isNullOrEmpty($this->getFieldValue(self::REPEAT_EMAIL_ADDRESS)) || ($this->getFieldValue(self::EMAIL_ADDRESS) != $this->getFieldValue(self::REPEAT_EMAIL_ADDRESS))) {
			$this->InvalidFields[] = self::EMAIL_ADDRESS;
			$this->InvalidFields[] = self::REPEAT_EMAIL_ADDRESS;
			
			$isValid = false;
		}
		
		if (Strings::equals($this->getFieldValue(self::COUNTRY), "0") || (Strings::equals($this->getFieldValue(self::COUNTRY), "10001") && Strings::isNullOrEmpty($this->getFieldValue(self::OTHER_COUNTRY)))) {
			$this->InvalidFields[] = self::COUNTRY;
			$this->InvalidFields[] = self::OTHER_COUNTRY;
			
			$isValid = false;
		}
		
		if (Strings::isNullOrEmpty($this->getFieldValue(self::STATE))) {
			$this->InvalidFields[] = self::STATE;
			
			$isValid = false;
		}
		
		if (Strings::isNullOrEmpty($this->getFieldValue(self::CITY))) {
			$this->InvalidFields[] = self::CITY;
			
			$isValid = false;
		}
		
		if (Strings::isNullOrEmpty($this->getFieldValue(self::I_AGREE_TO_BETA_RULES))) {
			$this->InvalidFields[] = self::I_AGREE_TO_BETA_RULES;
			
			$isValid = false;
		}
		
		$this->_logger->sendMessage(LOG_DEBUG, "Invalid Fields: " . var_export($this->InvalidFields, true));
		
		return $isValid;
	}
	
	/**
	 * _usernameIsInUse
	 * Determines whether the username chosen is already in use.
	 * @param string $userName
	 * @return bool
	 */
	private function _usernameIsInUse(/*string*/ $userName) {
		ArgumentTypeValidator::isString($userName, "UserName must be a string.");
		
		$usernameInUse = false;
		
		$query = "select user_name from `pbox`.`people` where user_name = '{$userName}'";
		
		if (!Strings::isNullOrEmpty($query)) {
			$rows = self::$__queryHandler->executeQuery($query);
			
			if (count($rows) > 0) {
				$usernameInUse = true;
			}
		}
	}
	
	/**
	 * _createAccount
	 * Creates the new account for this user.
	 * @return void
	 */
	private function _createAccount() {
		$result = false;
		$username = $this->getFieldValue(self::USERNAME);
		$firstRealName = $this->getFieldValue(self::FIRST_REAL_NAME);
		$lastRealName = $this->getFieldValue(self::LAST_REAL_NAME);
		$salt = "34b14c5e-448e-4992-98a8-5274bb49d125";
		$password = md5($this->getFieldValue(self::PASSWORD . $salt));
		$explicity = $this->getFieldValue(self::EXPLICITNESS);
		$bio = $this->getFieldValue(self::BIO);
		$dateCreated = date("Y/m/d") . " 00:00:00";
		$dateUpdated = date("Y/m/d") . " 00:00:00";
		$dateOfBirth = $this->getFieldValue(self::DATE_OF_BIRTH) . " 00:00:00";
		$query = "insert into `pbox`.`people` (user_name, first_real_name, last_real_name, password, explicity_id, bio, date_created, date_update, date_of_birth) values ('{$username}', '{$firstRealName}', '{$lastRealName}', '{$password}', '{$explicity}', '{$bio}', '{$dateCreated}', '{$dateUpdated}', '{$dateOfBirth}')";
		
		self::$__queryHandler->executeQuery($query);

		if (self::$__queryHandler->getAffectedRows() > 0) {
			$this->setSessionFieldValue(AuthenticationPage::AUTHENTICATION_KEY, session_id());
			$this->setSessionFieldValue(AuthenticationPage::USERNAME_KEY, $this->getFieldValue(AuthenticationPage::USERNAME_KEY));
			$result = true;
		}
		
		return $result;
	}
	
}

?>