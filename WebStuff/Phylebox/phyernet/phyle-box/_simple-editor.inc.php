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
		list($driveType, $driveId) = Strings::split($driveSelector, "-");
		$driveType = intval($driveType);
		$driveId = intval($driveId);
		$driveQuery = "";
		$driveLocation = "/";
		
		$this->_logger->sendMessage(LOG_DEBUG, "Drive Selector: {$driveSelector}, Current Directory: {$currentDirectory}, File Name: {$fileName}, Drive Type: {$driveType}");
		
		if (!is_null($userName)) {
			if ($driveType === DriveType::PERSONAL) {
				$driveQuery = "select pd.drive_location from `pbox`.`personal_drives` pd, `pbox`.`account_types` at, `pbox`.`people` p where p.user_name = '{$userName}' and pd.personal_drive_id = '{$driveId}' and pd.person_id = p.person_id";
			} else if ($driveType === DriveType::STORAGE) {
				$driveQuery = "select ps.storage_location as drive_location from `pbox`.`personal_storage` ps, `pbox`.`people` p where p.user_name = '{$userName}' and ps.personal_storage_id = '{$driveId}' and ps.person_id = p.person_id";
			} else if ($driveType === DriveType::GROUP) {
				$driveQuery = "select gd.drive_location from `pbox`.`group_drives` gd, `pbox`.`groups` g, `pbox`.`people_groups` pg, `pbox`.`people` p where p.user_name = '{$userName}' and pg.person_id = p.person_id and pg.group_id = g.group_id and gd.group_id = g.group_id and gd.group_drive_id = '{$driveId}'";
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