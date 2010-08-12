<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Assert.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandler.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandlerType.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/business/JsonWebServiceBase.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "data/DirectoryModel.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "data/DriveUsageModel.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "data/FileModel.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "types/DriveType.inc.php");

/**
 * FileManagementService
 * This service provides means to manage files.
 * @author lanathan
 *
 */
class FileManagementService extends JsonWebServiceBase {
	
	const DIRECTORY_NAME = "directory";
	const DRIVE_NAME = "name";
	const DRIVE_LOCATION = "location";
	const DRIVE_TYPE = "type";
	const USER_NAME = "userName";
	
	private static $__instance;
	private static $__queryHandler;
	
	/**
	 * Constructor
	 * @return FileManagementService
	 */
	protected function FileManagementService() {
		parent::__construct("FileManagementService");
		
		if (!isset(self::$__queryHandler)) {
			self::$__queryHandler = QueryHandler::getInstance(QueryHandlerType::MYSQL);
		}

		$this->registerServiceMethod("getDiskSpaceUsedForDrive", array("name: string", "location: string", "type: int"), "Gets the total amount of disk space used by files and messages at a specific location.");
	}
	
	/**
	 * getDiskSpaceUsedForDrive
	 * Gets the disk space used on a particular drive.
	 * @param object $jsonObject
	 */
	public function getDiskSpaceUsedForDrive(/*object*/ $jsonObject) {
		ArgumentTypeValidator::isObject($jsonObject, "JsonObject must be an object.");
		
		$userName = $_SESSION["userName"];
		$driveName = $jsonObject->{FileManagementService::DRIVE_NAME};
		list($type, $driveId) = Strings::split(($jsonObject->{FileManagementService::DRIVE_LOCATION}), "-");
		$driveId = intval($driveId);
		$driveType = intval($jsonObject->{FileManagementService::DRIVE_TYPE});
		$allottedSpace = 0;
		$driveQuery = "";
		$driveUsageModel = null;
		
		Assert::isTrue(!is_null($driveId), "A string value named location was expected but not found.");
		Assert::isTrue(!is_null($driveType), "An integer value named type was expected but not found.");
		
		if (!is_null($userName)) {
			if ($driveType === DriveType::PERSONAL) {
				$driveQuery = "select pd.drive_location, pd.additional_space, at.allotted_space from `pbox`.`personal_drives` pd, `pbox`.`account_types` at, `pbox`.`people` p where p.user_name = '{$userName}' and pd.id = '{$driveId}' and pd.person = p.id";
			} else if ($driveType === DriveType::STORAGE) {
				$driveQuery = "select ps.storage_location as drive_location, ps.allotted_space from `pbox`.`personal_storage` ps, `pbox`.`people` p where p.user_name = '{$userName}' and ps.id = '{$driveId}' and ps.person = p.id";
			} else if ($driveType === DriveType::GROUP) {
				$driveQuery = "select gd.drive_location, gd.allotted_space from `pbox`.`group_drives` gd, `pbox`.`groups` g, `pbox`.`people_groups` pg, `pbox`.`people` p where p.user_name = '{$userName}' and g.person = p.id and g.group = pg.id and gd.group = pg.id and gd.id = '{$driveId}'";
			}
			
			if (!Strings::isNullOrEmpty($driveQuery)) {
				$rows = self::$__queryHandler->executeQuery($driveQuery);
				
				$this->_logger->sendMessage(LOG_DEBUG, "Number of rows: " . count($rows));
				
				if (count($rows) > 0) {
					$allottedSpace = intval($rows[0]["allotted_space"]);
					
					if (array_key_exists("additional_space", $rows[0])) {
						$allottedSpace += intval($rows[0]["additional_space"]);
					}
					
					$this->_logger->sendMessage(LOG_DEBUG, "AllottedSpace: {$allottedSpace}");
				}else {
					$this->echoJsonError("Drive specified could not be found or does not exist.", "The drive that was specified either does not exist or it could not be found in the system.");
				}
			
				$driveUsageModel = new DriveUsageModel($rows[0]["drive_location"], $allottedSpace);
			} else {
				$this->echoJsonError("Unrecognized drive type", "The drive type {$driveType} was not recognized as a valid drive type.");
			}
			
			$this->echoJson(json_encode(array("kilobytes" => $driveUsageModel->kilobytes, "percentage" => $driveUsageModel->percentage, "allotted" => $driveUsageModel->allotted)));
		} else {
			$this->echoJsonError("No user is authenticated.", "You must be authenticated to use this service.");
		}
	}
	
	/**
	 * listFoldersAndFilesForDriveAndDirectory
	 * Returns a list of folders and files for a specific drive and directory.
	 * @param object $jsonObject
	 */
	public function listFoldersAndFilesForDriveAndDirectory(/*object*/ $jsonObject) {
		ArgumentTypeValidator::isObject($jsonObject, "JsonObject must be an object.");
		
		$userName = $_SESSION["userName"];
		$locationComponents = Strings::split(($jsonObject->{FileManagementService::DRIVE_LOCATION}), "-");
		$driveType = intval($locationComponents[0]);
		$driveId = intval($locationComponents[1]);
		$directoryName = $jsonObject->{FileManagementService::DIRECTORY_NAME};
		$foldersAndFiles = array();
		
		Assert::isTrue(!is_null($driveId) && !is_null($driveType), "A string value named location was expected but not found.");
		Assert::isTrue(!is_null($directoryName), "A string value named directory was expected but not found.");
		
		if (!is_null($userName)) {
			if ($driveType === DriveType::PERSONAL) {
				$driveQuery = "select pd.drive_location from `pbox`.`personal_drives` pd, `pbox`.`account_types` at, `pbox`.`people` p where p.user_name = '{$userName}' and pd.id = '{$driveId}' and pd.person = p.id";
			} else if ($driveType === DriveType::STORAGE) {
				$driveQuery = "select ps.storage_location as drive_location from `pbox`.`personal_storage` ps, `pbox`.`people` p where p.user_name = '{$userName}' and ps.id = '{$driveId}' and ps.person = p.id";
			} else if ($driveType === DriveType::GROUP) {
				$driveQuery = "select gd.drive_location from `pbox`.`group_drives` gd, `pbox`.`groups` g, `pbox`.`people_groups` pg, `pbox`.`people` p where p.user_name = '{$userName}' and g.person = p.id and g.group = pg.id and gd.group = pg.id and gd.id = '{$driveId}'";
			}
			
			if (!Strings::isNullOrEmpty($driveQuery)) {
				$rows = self::$__queryHandler->executeQuery($driveQuery);
				
				$this->_logger->sendMessage(LOG_DEBUG, "Number of rows: " . count($rows));
				
				if (count($rows) > 0) {
					$driveLocation = $rows[0]["drive_location"];
					$absoluteDirectoryLocation = $driveLocation . (Strings::startsWith($directoryName, "/") ? $directoryName : ("/" . $directoryName));
					
					if (is_dir($absoluteDirectoryLocation)) {
						$directoryHandle = opendir($absoluteDirectoryLocation);
						
						if (is_resource($directoryHandle)) {							
							$fileCounter = 0;
							$directoryCounter = 0;
							
							while ($nextFile = readdir($directoryHandle)) {
								$nextAbsolutePath = $absoluteDirectoryLocation . $nextFile;
								
								if (!Strings::equals($nextFile, ".")) {
									if (is_dir($nextAbsolutePath)) {
										$directoryCounter++;
										$directoryModel = new DirectoryModel($nextAbsolutePath);
										$modifiedTime = date("r", $directoryModel->modifiedTime);
										$foldersAndFiles["dir{$directoryCounter}"] = array("type" => "Directory", "name" => $nextFile, "permissions" => $directoryModel->permissions, "size" => $directoryModel->size, "modifiedTime" => $modifiedTime);
									} else {
										$fileCounter++;
										$fileModel = new FileModel($nextAbsolutePath);
										$modifiedTime = date("r", $fileModel->modifiedTime);
										$foldersAndFiles["file{$fileCounter}"] = array("type" => $fileModel->extension, "name" => $nextFile, "permissions" => $fileModel->permissions, "size" => $fileModel->size, "modifiedTime" => $modifiedTime);
									}
								}
							}
						
							$this->echoJson(json_encode(array("directories" => $directoryCounter, "files" => $fileCounter, "contents" => $foldersAndFiles)));
						} else {
							$this->echoJsonError("Directory could not be read.", "The direcotry specified, {$absoluteDirectoryLocation} could not be read on this system.");
						}
					} else {
						$this->echoJsonError("Directory specified is not a direcotry", "The directory that was specified, {$absoluteDirectoryLocation}, is not a directory on this system.");
					}
				} else {
					$this->echoJsonError("Drive specified could not be found or does not exist.", "The drive that was specified either does not exist or it could not be found in the system.");
				}
			} else {
				$this->echoJsonError("Unrecognized drive type", "The drive type {$driveType} was not recognized as a valid drive type.");
			}
		} else {
			$this->echoJsonError("No user is authenticated.", "You must be authenticated to use this service.");
		}
	}
	
	/**
	 * Dispatch method required so that the parent class knows about this class's methods.
	 * @return void
	 */
	public function dispatch() {
		parent::dispatch($this);
	}

	/**
	 * Gets the singleton instance.
	 * @return {BannerManagementService}
	 */
	public static function getInstance() {
		if (!self::$__instance) {
			self::$__instance = new FileManagementService();
		}
		
		return self::$__instance;
	}
	
}

$fileManagementService = FileManagementService::getInstance();
$fileManagementService->dispatch();

?>