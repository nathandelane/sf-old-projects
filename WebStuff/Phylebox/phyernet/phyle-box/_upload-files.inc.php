<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Assert.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandler.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandlerType.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "types/DriveType.inc.php");
require_once(PhyleBox_Config::getLocalPresentationLocation() . "PhyleBoxBasicPage.inc.php");

/**
 * _Upload_Files_Page
 * This page is the file upload page.
 * @author lanathan
 *
 */
class _Upload_Files_Page extends PhyleBoxBasicPage {
	
	const DRIVE_SELECTOR = "driveSelector";
	const DIRECTORY_NAME = "currentDirectory";
	
	public $successMessage;

	private static $__queryHandler;
	
	/**
	 * Creates an instance of _Upload_Files_Page.
	 * @return _Upload_Files_Page
	 */
	public function _Upload_Files_Page() {
		parent::__construct("Upload Files | PhyleBox");
		
		if (!isset(self::$__queryHandler)) {
			self::$__queryHandler = QueryHandler::getInstance(QueryHandlerType::MYSQL);
		}
		
		if ($this->getFieldValue("token")) {
			if (count($_FILES["filesBeingUploaded"]) > 0) {
				if ($this->moveUploadedFiles()) {
					$this->successMessage = "Uploaded: " . $_FILES["filesBeingUploaded"]["name"];
				} else {
					$this->successMessage = "Upload failed.";
				}
			} else {
				$this->successMessage = "Upload failed.";
			}
		}
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/phyle-box/presentation/PhyleBoxPage::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/phyle-box/presentation/PhyleBoxPage::closeDocument()
	 */
	public function closeDocument() {
		parent::closeDocument();
	}
	
	/**
	 * moveUploadedFiles
	 * Takes care of moving the uploaded files to the correct location.
	 * @return bool
	 */
	public function moveUploadedFiles() {
		$result = false;
		$userName = $_SESSION["userName"];
		$locationComponents = Strings::split($this->getFieldValue(self::DRIVE_SELECTOR), "-");
		$driveType = intval($locationComponents[0]);
		$driveId = intval($locationComponents[1]);
		$directoryName = $this->getFieldValue(self::DIRECTORY_NAME);
		
		Assert::isTrue(!is_null($driveId) && !is_null($driveType), "A string value named location was expected but not found.");
		Assert::isTrue(!is_null($directoryName), "A string value named directory was expected but not found.");

		if (!is_null($userName)) {
			if ($driveType === DriveType::PERSONAL) {
				$driveQuery = "select pd.drive_location from `pbox`.`personal_drives` pd, `pbox`.`account_types` at, `pbox`.`people` p where p.user_name = '{$userName}' and pd.personal_drive_id = '{$driveId}' and pd.person_id = p.person_id";
			} else if ($driveType === DriveType::STORAGE) {
				$driveQuery = "select ps.storage_location as drive_location from `pbox`.`personal_storage` ps, `pbox`.`people` p where p.user_name = '{$userName}' and ps.personal_storage_id = '{$driveId}' and ps.person_id = p.person_id";
			} else if ($driveType === DriveType::GROUP) {
				$driveQuery = "select gd.drive_location from `pbox`.`group_drives` gd, `pbox`.`groups` g, `pbox`.`people_groups` pg, `pbox`.`people` p where p.user_name = '{$userName}' and pg.person_id = p.person_id and pg.group_id = g.group_id and gd.group_id = g.group_id and gd.group_drive_id = '{$driveId}'";
			}
			
			if (!Strings::isNullOrEmpty($driveQuery)) {
				$rows = self::$__queryHandler->executeQuery($driveQuery);
				
				$this->_logger->sendMessage(LOG_DEBUG, "Number of rows: " . count($rows));
				
				if (count($rows) > 0) {
					$driveLocation = $rows[0]["drive_location"];
					$absoluteDirectoryLocation = $driveLocation . (Strings::startsWith($directoryName, "/") ? $directoryName : ("/" . $directoryName));
					
					$testPath = dirname($absoluteDirectoryLocation);
					
					$this->_logger->sendMessage(LOG_DEBUG, "Test Path: {$testPath}");
					
					if (!Strings::startsWith($testPath, $driveLocation) || strcmp($testPath, $driveLocation) < 0) {
						$absoluteDirectoryLocation = $driveLocation . "/";
					}
					
					if (is_dir($absoluteDirectoryLocation)) {
						$newFileLocation = $absoluteDirectoryLocation . "/" . basename( $_FILES['filesBeingUploaded']['name']);
					
						if (move_uploaded_file($_FILES['filesBeingUploaded']['tmp_name'], $newFileLocation)) {
							$result = true;
						}
					}
				}
			}			
		}
		
		return $result;
	}
	
}

?>
