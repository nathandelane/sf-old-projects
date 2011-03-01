<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "PhyleBoxBasicPage.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandler.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandlerType.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "types/DriveType.inc.php");

class _Simple_Editor_Page extends PhyleBoxBasicPage {
	
	private static $__queryHandler;
	
	public function _Simple_Editor_Page() {
		parent::__construct("File Manager | Simple Editor");
		
		if (!isset(self::$__queryHandler)) {
			self::$__queryHandler = QueryHandler::getInstance(QueryHandlerType::MYSQL);
		}
		
		$this->registerStylesheet("_css/file-manager.css");
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
	function saveFile() {
		var location = $("#driveSelector").val();
		var directory = $("#currentDirectory").val();
		var fileName = $("#fileName").val();
		var contents = $("#textFileContents").val();

		contents = contents.replace(new RegExp( "\\r", "g" ), "\\r");
		contents = contents.replace(new RegExp( "\\n", "g" ), "\\n");
		contents = contents.replace(new RegExp( "\\t", "g" ), "\\t");
		contents = contents.replace(new RegExp( "\\\"", "g" ), "\\\"");
		
		$Phyer.FileManager.editFile(location, directory, fileName, contents);		
	}
	
	$(document).ready(function() {
		$("#textFileContents").focus();
		$("#textFileContents").keydown(function(e) {
			e.stopPropagation();

			var TAB_KEY = 9;

			if (e.keyCode == TAB_KEY) {
				e.preventDefault();

				var caretSelection = $("#textFileContents").getSelection();
				var textAreaText = $("#textFileContents").val();
				
				var first = textAreaText.substring(0, caretSelection.start);
				var lastLength = (textAreaText.length - caretSelection.end)
				var last = textAreaText.substring(caretSelection.end);

				$("#textFileContents").val(first + "\t" + last);
			}

			$("#textFileContents").focus();
		});
		$("#saveButton").click(function(e) {
			saveFile();
		});
		$("#saveCloseButton").click(function(e) {
			saveFile();

			parent.$.fancybox.close();
		});
	});
</script>
<?php
		
		parent::closeDocument();
	}
	
	/**
	 * getFileContents
	 * Gets the contents of a file if it exists.
	 * @param string driveSelector
	 * @param string currentDiirectory
	 * @param string fileName
	 * @return string
	 */
	public function getFileContents(/*string*/ $driveSelector, /*string*/ $currentDirectory, /*string*/ $fileName) {
		$contents = "";		
		$userName = $_SESSION["userName"];
		$relativePath = $currentDirectory;
		list($driveType, $driveId, $shortcutId) = Strings::split($driveSelector, "-");
		$driveType = intval($driveType);
		$driveId = intval($driveId);
		$driveQuery = "";
		$driveLocation = "/";
		$shortcutClause = $shortcutId == 0 ? "" : " and ps.personal_shortcut_id = {$shortcutId}";
		
		$this->_logger->sendMessage(LOG_DEBUG, "Drive Selector: {$driveSelector}, Current Directory: {$currentDirectory}, File Name: {$fileName}, Drive Type: {$driveType}");
		
		if (!is_null($userName)) {
			if ($driveType === DriveType::PERSONAL) {
				$driveQuery = "select Concat(pd.drive_location, ifnull(ps.directory, '')) as drive_location from `pbox`.`personal_drives` pd inner join `pbox`.`people` p on p.person_id = pd.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 1) ps on (ps.person_id = p.person_id and ps.drive_id = pd.personal_drive_id) where p.user_name = '{$userName}' and pd.personal_drive_id = {$driveId}{$shortcutClause}";
			} else if ($driveType === DriveType::STORAGE) {
				$driveQuery = "select Concat(ps.storage_location, ifnull(ps2.directory, '')) as drive_location from `pbox`.`personal_storage` ps inner join `pbox`.`people` p on p.person_id = ps.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 2) ps2 on (ps2.person_id = p.person_id and ps2.drive_id = ps.personal_storage_id) where p.user_name = '{$userName}' and ps.personal_storage_id = {$driveId}{$shortcutClause}";
			} else if ($driveType === DriveType::GROUP) {
				$driveQuery = "select Concat(gd.drive_location, ifnull(ps.directory, '')) as drive_location from `pbox`.`group_drives` gd left outer join `pbox`.`groups` g on g.group_id = gd.group_id left outer join `pbox`.`people_groups` pg on pg.group_id = g.group_id left outer join `pbox`.`people` p  on p.person_id = pg.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 3) ps on (ps.person_id = p.person_id and ps.drive_id = gd.group_drive_id) where p.user_name = '{$userName}' and gd.group_drive_id = {$driveId}{$shortcutClause}";
			}
									
			$this->_logger->sendMessage(LOG_DEBUG, "Drive Query: {$driveQuery}");
			
			if (!Strings::isNullOrEmpty($driveQuery)) {
				$rows = self::$__queryHandler->executeQuery($driveQuery);
				
				$this->_logger->sendMessage(LOG_DEBUG, "Number of rows: " . count($rows));
				
				if (count($rows) > 0) {
					$driveLocation = $rows[0]["drive_location"];
					
					$this->_logger->sendMessage(LOG_DEBUG, "DriveLocation: {$driveLocation}");
				}
			}
		} else {
			$this->_logger->sendMessage(LOG_DEBUG, "Could not access file. No authentication found.");
		}
		
		$absoluteFilePath = $driveLocation . $currentDirectory . $fileName;
		
		$this->_logger->sendMessage(LOG_DEBUG, "Absolute file location: {$absoluteFilePath}.");
		
		if (file_exists($absoluteFilePath)) {
			$contents = file_get_contents($absoluteFilePath);
		}
		
		
		
		return $contents;
	}
	
}

?>