<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandler.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandlerType.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "data/ProfileModel.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "data/GroupsModel.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "data/RolesModel.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "PhyleBoxPage.inc.php");

/**
 * _Profile_Page
 * This page has to do with the administration of the user's profile.
 * @author lanathan
 *
 */
class _Profile_Page extends PhyleBoxPage {

	private static $__queryHandler;
	private static $__countries;
	private static $__explicity;
	
	private $_profileModel;
	private $_groupsModel;
	private $_rolesModel;
	
	/**
	 * Constructor
	 * @return _Profile_Page
	 */
	public function _Profile_Page() {
		parent::__construct("Profile | PhyleBox");
		
		$this->_breadcrumb->setBreadcrumb(array("Home" => PhyleBox_Config::getPhyleBoxRoot(), "Profile" => null));
		$this->_profileModel = new ProfileModel($this);
		$this->_groupsModel = new GroupsModel($this);
		$this->_rolesModel = new RolesModel($this);
		
		if (!isset(self::$__queryHandler)) {
			self::$__queryHandler = QueryHandler::getInstance(QueryHandlerType::MYSQL);
		}
		
		if (!isset(self::$__countries)) {
			$query = "select c.numcode, c.printable_name from `pbox`.`country` c";
			
			self::$__countries = self::$__queryHandler->executeQuery($query);
		}
		
		if (!isset(self::$__explicity)) {
			$query = "select e.explicity_id, e.name from `pbox`.`explicity` e";
			
			self::$__explicity = self::$__queryHandler->executeQuery($query);
		}
		
		$this->registerStylesheet("_css/profile.css");
		$this->registerScript("../_js/jquery.maskedinput.js");
		
		if ($this->getFieldValue("session")) {
			if ($this->_isValid()) {
				
			}
		}
	}
	
	/**
	 * (non-PHPdoc)
	 * @see phyle-box/presentation/PhyleBoxPage::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
		
?>
<form id="profileForm" action="<?php echo $_SERVER["REQUEST_URI"]; ?>" method="post" enctype="application/x-www-form-urlencoded">
	<input type="hidden" name="session" id="session" value="<?php echo session_id(); ?>" />
	<div class="column">
		<div class="contentBox" id="avatar">
			<h3>Your Avatar</h3>
			<p>Your avatar must be 48x48 pixels in size. It should be a PNG file, but may also be a GIF or JPEG.</p>
			<div class="avatar">
				<img alt="" src="<?php echo PhyleBox_Config::getPhyleBoxRoot(); ?>/_img/avatars/<?php echo $this->getAvatar(); ?>" />
			</div>
			<div class="formRow">
				<input class="fileInput" type="file" name="avatarFileUpload" id="avatarFileUpload" />
			</div>
			<div class="formRow">
				<input class="profileSubmit" type="submit" value="Upload Avatar Image" />
			</div>
		</div>
		<div class="contentBox" id="info">
			<h3>Your Information</h3>
			<p>This information will appear on your profile when people view it.</p>
			<div class="formRow">
				<label for="username">User Name</label>
				<input type="text" name="username" id="username" value="<?php echo $this->getSessionFieldValue("userName"); ?>" readonly="readonly" />
			</div>
			<div class="formRow">
				<label for="dateOfBirth">Date of Birth*</label>
				<input type="text" name="dateOfBirth" id="dateOfBirth" value="<?php echo date("m/d/Y", intval($this->_profileModel->date_of_birth)); ?>" />
			</div>
			<div class="formRow">
				<label for="realName">Real Name*</label>
				<input type="text" name="realName" id="realName" value="<?php echo $this->_profileModel->real_name; ?>" />
			</div>
			<div class="formRow">
				<label for="country">Country*</label>
				<select name="country" id="country">
					<option value="0">-- Select Country</option>
<?php

			$country = $this->_profileModel->country;

			foreach(self::$__countries as $nextCountry) {
				
?>
					<option value="<?php echo $nextCountry["numcode"]; ?>"<?php if (Strings::equals($nextCountry["numcode"], $country)) { echo "selected=\"selected\""; } ?>>
						<?php echo $nextCountry["printable_name"]; ?>
					</option>
<?php
				
			}

?>
				</select>
			</div>
			<div class="formRow">
				<label for="stateOrProvince">State or Province</label>
				<input type="text" name="stateOrProvince" id="StateOrProvince" value="<?php echo $this->_profileModel->state; ?>" />
			</div>
			<div class="formRow">
				<label for="city">City</label>
				<input type="text" name="city" id="city" value="<?php echo $this->_profileModel->city; ?>" />
			</div>
			<div class="formRow">
				<label for="bio">Bio</label>
				<textarea name="bio" id="bio"><?php echo $this->_profileModel->bio; ?>	</textarea>
			</div>
			<div class="formRow">
				<input class="profileSubmit" type="submit" value="Save Information" />
			</div>
		</div>
		<div class="contentBox" id="password">
			<h3>Set Password</h3>
			<div class="formRow">
				<label for="password">Password*</label>
				<input name="password" id="password" type="password" value="" />
			</div>
			<div class="formRow">
				<label for="repeatPassword">Repeat Password*</label>
				<input name="repeatPassword" id="repeatPassword" type="password" value="" />
			</div>
			<div class="formRow">
				<input class="profileSubmit" type="submit" value="Save Password" />
			</div>
		</div>
	</div>
	<div class="column">
		<div class="contentBox" id="explicity">
			<h3>Set Explicity of Site</h3>
			<div class="formRow">
				<label for="siteExplicity">Explicity*</label>
				<select name="siteExplicity" id="siteExplicity">
<?php

			$explicity = $this->_profileModel->explicit;

			foreach(self::$__explicity as $nextExplicity) {
				
?>
					<option value="<?php echo $nextExplicity["explicity_id"]; ?>"<?php if (Strings::equals($nextExplicity["id"], $explicity)) { echo "selected=\"selected\""; } ?>>
						<?php echo $nextExplicity["name"]; ?>
					</option>
<?php
				
			}

?>
				</select>
			</div>
			<div class="formRow">
				<input class="profileSubmit" type="submit" value="Save Explicity" />
			</div>
		</div>
		<div class="contentBox" id="contactInfo">
			<h3>Contact Information</h3>
			<p>This information will never be shared with anyone unless you allow us to.</p>
			<div class="formRow">
				<label for="emailAddress">Email Address*</label>
				<input type="text" name="emailAddress" id="emailAddress" value="<?php echo $this->_profileModel->email; ?>" />
			</div>
			<div class="formRow">
				<label for="phoneNumber">Phone Number</label>
				<input type="text" name="phoneNumber" id="phoneNumber" value="<?php echo $this->_profileModel->phone; ?>" />
			</div>
			<div class="formRow">
				<input class="profileSubmit" type="submit" value="Save Contact Info" />
			</div>			
		</div>
		<div class="contentBox" id="resources">
			<h3>Your Resources</h3>
			<div class="formRow resources">
				Merits: 24,300
			</div>
<?php

			$usedDiskSpace = round(((4 / intval($this->_profileModel->personal_drive_space)) * 300), 0, PHP_ROUND_HALF_UP);

?>
			<div class="formRow resources">
				Disk Space: (used) 4Kb / (total) 215Mb [<?php echo "$usedDiskSpace%"; ?>]
				<div class="diskRepresentation">
					<div class="diskUsed" style="width: <?php echo "$usedDiskSpace"; ?>%;"></div>
				</div>
			</div>
			<div class="formRow resources">
				Groups You Belong To:
<?php
			
			$groupsEnumerator = $this->_groupsModel->getEnumerator();

?>				
				<ul>
<?php

			$counter = 0;
			
			while ($groupsEnumerator->moveNext()) {
				$nextGroup = $groupsEnumerator->getNextItem();
				
?>
					<li>
						<?php echo $nextGroup["name"]; ?>
					</li>
<?php

				$counter++;
			}

			if ($counter == 0) {
				
?>
					<li>You don't currently belong to any groups.</li>
<?php
				
			}
?>
				</ul>
			</div>
			<div class="formRow resources">
				Your Roles:
<?php
			
			$rolesEnumerator = $this->_rolesModel->getEnumerator();

?>				
				<ul>
<?php

			while ($rolesEnumerator->moveNext()) {
				$nextRole = $rolesEnumerator->getNextItem();
				
?>
					<li>
						<?php echo $nextRole["name"]; ?>
					</li>
<?php
				
			}

?>
				</ul>
			</div>
		</div>
		<div class="contentBox" id="accountInformation">
			<h3>Account Information</h3>
			<div class="formRow">
				Account Type: <?php /* TODO: Echo Account Type */ ?>
			</div>
			<div class="formRow">
				Creative Mind Type: <?php /* TODO: Echo Creative Mind Type */ ?>
			</div>
		</div>
	</div>
</form>
<?php

	}
	
	/**
	 * (non-PHPdoc)
	 * @see phyle-box/presentation/PhyleBoxPage::closeDocument()
	 */
	public function closeDocument() {
		
?>
<script type="text/javascript">
	$("#phoneNumber").mask("(999) 999-9999");
</script>
<?php
		
		parent::closeDocument();
	}
	
}

?>
