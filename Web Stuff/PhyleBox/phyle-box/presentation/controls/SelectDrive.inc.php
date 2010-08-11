<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/collections/HashCollection.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IPage.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "data/DrivesModel.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "types/DriveShortcut.inc.php");

/**
 * SelectDrive
 * This class represents the drive selector for the file manager toolbar.
 * @author lanathan
 *
 */
class SelectDrive implements IRenderable {
	
	private $_selectedDrive;
	private $_drivesModel;
	private $_formId;
	private $_page;
	
	/**
	 * Constructor
	 * @param IPage $page
	 * @param string $formId
	 * @return SelectDrive
	 */
	public function SelectDrive(IPage $page, /*string*/ $formId) {
		ArgumentTypeValidator::isString($formId, "FormID must be a string.");
		
		$this->_drivesModel = new DrivesModel($page);
		$this->_formId = $formId;
		$this->_page = $page;
		
		if ($page->getFieldValue("driveSelector")) {
			$driveSelectorValue = $this->_page->getFieldValue("driveSelector");
			
			$this->_page->getLogger()->sendMessage(LOG_DEBUG, "Drive Selector Value: {$driveSelectorValue}");
			
			$this->_selectedDrive = $this->_drivesModel->get($driveSelectorValue);
		} else {
			$this->_selectedDrive = $this->_drivesModel->getFirstOrDefault();
		}
		
		$this->_page->getLogger()->sendMessage(LOG_DEBUG, "Selected Drive: {$this->_selectedDrive}");
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div id="favorites">
	<ul>
		<li>
			<div id="driveInfo">
				<a id="selectedDrive" href="#"><?php echo "{$this->_selectedDrive->name}"; ?></a>
				<div class="smallDriveSpace">
					<div class="smallUsedSpace" style="width: 15%;"></div>
				</div>
				<div class="driveSpacePercentage">85% Free</div>
			</div>
			<div id="driveSelectorContainer" class="hide">
				<select id="driveSelector" name="driveSelector">
<?php

		$drivesEnumerator = $this->_drivesModel->getEnumerator();

		while ($drivesEnumerator->moveNext()) {
			$nextDriveShortcut = $drivesEnumerator->getNextValue();
			
			$this->_page->getLogger()->sendMessage(LOG_DEBUG, "Next Drive Shortcut: {$nextDriveShortcut}.");
			
			if ($nextDriveShortcut instanceof DriveShortcut) {
			
?>
					<option value="<?php echo "{$nextDriveShortcut->location}"; ?>"><?php echo"{$nextDriveShortcut->name}"; ?></option>
<?php
			
			}
		}

?>
				</select>
				<button id="driveSelectorButton" name="driveSelectorButton">Go</button>
			</div>
		</li>
	</ul>
	<script type="text/javascript">
		$("#selectedDrive").bind("click", function() {
			$("#driveInfo").attr("class", "hide");
			$("#driveSelectorContainer").attr("class", "");
		});
		$("#driveSelectorButton").bind("click", function() {
			$("#<?php echo "$this->_formId"; ?>").submit();
		});
	</script>
</div>
<?php
		
	}
	
}

?>