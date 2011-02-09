<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/collections/HashCollection.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "data/DrivesModel.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "types/DriveShortcut.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "PhyleBoxPage.inc.php");

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
	 * @param PhyleBoxPage $page
	 * @param string $formId
	 * @return SelectDrive
	 */
	public function SelectDrive(PhyleBoxPage $page, /*string*/ $formId) {
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

			$this->_page->getLogger()->sendMessage(LOG_DEBUG, "Using default drive");
		}
		
		$this->_page->getLogger()->sendMessage(LOG_DEBUG, "Selected Drive: {$this->_selectedDrive}");
		$this->_page->getBreadcrumb()->setBreadcrumb(array("Home" => PhyleBox_Config::getPhyleBoxRoot(), "File Manager" => PhyleBox_Config::getPhyleBoxRoot() . "/file-manager.php", "[{$this->_selectedDrive->name}] /" => null));
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
					<div id="selectedDriveUsedSpace" class="smallUsedSpace"></div>
				</div>
				<div id="driveSpacePercentage" class="driveSpacePercentage"></div>
			</div>
			<div id="driveSelectorContainer" class="hide">
				<select id="driveSelector" name="driveSelector">
<?php

		$drivesEnumerator = $this->_drivesModel->getEnumerator();
		$selectedDrive = $this->_page->getFieldValue("driveSelector");

		while ($drivesEnumerator->moveNext()) {
			$nextDriveShortcut = $drivesEnumerator->getNextValue();
			
			$this->_page->getLogger()->sendMessage(LOG_DEBUG, "Next Drive Shortcut: {$nextDriveShortcut}.");
			
			if ($nextDriveShortcut instanceof DriveShortcut) {
				$nextShortcutToken = "{$nextDriveShortcut->type}-{$nextDriveShortcut->id}";
			
?>
					<option value="<?php echo "{$nextShortcutToken}"; ?>"<?php if (Strings::equals($selectedDrive, $nextShortcutToken)) { echo "selected=\"selected\""; } ?>><?php echo"{$nextDriveShortcut->name}"; ?></option>
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
		$(document).ready(function() {
			$("#selectedDrive").bind("click", function() {
				$("#driveInfo").attr("class", "hide");
				$("#driveSelectorContainer").attr("class", "");
			});
			$("#driveSelectorButton").bind("click", function() {
				$("#<?php echo "$this->_formId"; ?>").submit();
			});

			$Phyer.FileManager.getDiskUsage("<?php echo "{$this->_selectedDrive->name}" ?>", "<?php echo "{$this->_selectedDrive->type}-{$this->_selectedDrive->id}" ?>", <?php echo "{$this->_selectedDrive->type}"?>);
			$Phyer.FileManager.populateFileList("<?php echo "{$this->_selectedDrive->type}-{$this->_selectedDrive->id}" ?>", $("#currentDirectory").val());
		});
	</script>
</div>
<?php
		
	}
	
}

?>
