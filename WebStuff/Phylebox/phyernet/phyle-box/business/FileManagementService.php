<?php

if (!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../../../_lib/phyle-box/Config.inc.php");
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
	const FILE_NAME = "fileName";
	const FILE_NAMES = "fileNames";
	const FILE_CONTENTS = "fileContents";
	const FOLDER_NAME = "folderName";
	const USER_NAME = "userName";
	const RELATIVE_PATH = "relativePath";
	const ORDER_BY_COLUMN_NAME = "orderByColumnName";
	const ORDER_BY_ORDER = "orderByOrder";
		
	private static $__instance;
	private static $__queryHandler;
	private static $__orderByColumnName;
	private static $__orderByOrder;
	private static $__knownFileTypes = array("png", "gif", "jpeg", "jpg", "bmp", "svg", "html", "htm", "php", "py", "pl", "pm", "aspx", "cs", "c", "cpp", "hpp", "h", "txt");
	
	private $_sort;
	
	/**
	 * Constructor
	 * @return FileManagementService
	 */
	protected function FileManagementService() {
		parent::__construct("FileManagementService");
		
		if (!isset(self::$__queryHandler)) {
			self::$__queryHandler = QueryHandler::getInstance(QueryHandlerType::MYSQL);
		}
		
		if (!isset(self::$__orderByColumnName)) {
			self::$__orderByColumnName = "name";
		}
		
		if (!isset(self::$__orderByOrder)) {
			self::$__orderByOrder = "asc";
		}

		$this->registerServiceMethod("getDiskSpaceUsedForDrive", array("name: string", "location: string", "type: int"), "Gets the total amount of disk space used by files and messages at a specific location.");
		$this->registerServiceMethod("listFoldersAndFilesForDriveAndDirectory", array("location: string", "directory: string", "orderByColumnName: string (default: name)", "orderByOrder: string (default: asc)"), "Gets the files and folders at a specific location.");
		$this->registerServiceMethod("getAbsolutePath", array("type: int", "relativePath: string", "location: string"), "Gets the absolute path of a relative location.");
		$this->registerServiceMethod("editFile", array("location: string", "directory: string", "fileName: string", "fileContents: string"), "Creates or edits a file.");
	}
	
	/**
	 * getAbsolutePath
	 * Gets the absolute path for a relative path.
	 * @param object $jsonObject
	 */
	public function getAbsolutePath(/*object*/ $jsonObject) {
		ArgumentTypeValidator::isObject($jsonObject, "JsonObject must be an object.");
		
		$userName = $_SESSION["userName"];
		$relativePath = $jsonObject->{FileManagementService::RELATIVE_PATH};
		list($type, $driveId, $shortcutId) = Strings::split(($jsonObject->{FileManagementService::DRIVE_LOCATION}), "-");
		$driveId = intval($driveId);
		$driveQuery = "";
		$driveLocation = "/";
		$shortcutId = intval($locationComponents[2]);
		$shortcutClause = $shortcutId == 0 ? "" : " and ps.personal_shortcut_id = {$shortcutId}";
		
		if (!is_null($userName)) {
			if ($driveType === DriveType::PERSONAL) {
				$driveQuery = "select Concat(pd.drive_location, ifnull(ps.directory, '')) as drive_location from `pbox`.`personal_drives` pd inner join `pbox`.`people` p on p.person_id = pd.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 1) ps on (ps.person_id = p.person_id and ps.drive_id = pd.personal_drive_id) where p.user_name = '{$userName}' and pd.personal_drive_id = {$driveId}{$shortcutClause}";
			} else if ($driveType === DriveType::STORAGE) {
				$driveQuery = "select Concat(ps.storage_location, ifnull(ps2.directory, '')) as drive_location from `pbox`.`personal_storage` ps inner join `pbox`.`people` p on p.person_id = ps.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 2) ps2 on (ps2.person_id = p.person_id and ps2.drive_id = ps.personal_storage_id) where p.user_name = '{$userName}' and ps.personal_storage_id = {$driveId}{$shortcutClause}";
			} else if ($driveType === DriveType::GROUP) {
				$driveQuery = "select Concat(gd.drive_location, ifnull(ps.directory, '')) as drive_location from `pbox`.`group_drives` gd left outer join `pbox`.`groups` g on g.group_id = gd.group_id left outer join `pbox`.`people_groups` pg on pg.group_id = g.group_id left outer join `pbox`.`people` p  on p.person_id = pg.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 3) ps on (ps.person_id = p.person_id and ps.drive_id = gd.group_drive_id) where p.user_name = '{$userName}' and gd.group_drive_id = {$driveId}{$shortcutClause}";
			}
															
			if (!Strings::isNullOrEmpty($driveQuery)) {
				$rows = self::$__queryHandler->executeQuery($driveQuery);
				
				$this->_logger->sendMessage(LOG_DEBUG, "Number of rows: " . count($rows));
				
				if (count($rows) > 0) {
					$driveLocation = intval($rows[0]["drive_location"]);
					
					$this->_logger->sendMessage(LOG_DEBUG, "DriveLocation: {$driveLocation}");
				}else {
					$this->echoJsonError("Drive specified could not be found or does not exist.", "The drive that was specified either does not exist or it could not be found in the system.");
				}
			} else {
				$this->echoJsonError("Unrecognized drive type", "The drive type {$driveType} was not recognized as a valid drive type.");
			}
			
			$absolutePath = Strings::substring(dirname($driveLocation . $relativePath), (Strings::length($driveLocation) - 1));
			
			if (!is_null($relativePath)) {
				$this->echoJson(json_encode(array("absolutePath" => $absolutePath)));
			} else {
				$this->echoJson(json_encode(array("absolutePath" =>  $absolutePath)));
			}
		} else {
			$this->echoJsonError("No user is authenticated.", "You must be authenticated to use this service.");
		}
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
		list($type, $driveId, $shortcutId) = Strings::split(($jsonObject->{FileManagementService::DRIVE_LOCATION}), "-");
		$driveId = intval($driveId);
		$driveType = intval($jsonObject->{FileManagementService::DRIVE_TYPE});
		$allottedSpace = 0;
		$driveQuery = "";
		$driveUsageModel = null;
		$shortcutClause = $shortcutId == 0 ? "" : " and ps.personal_shortcut_id = {$shortcutId}";
		
		Assert::isTrue(!is_null($driveId), "A string value named location was expected but not found.");
		Assert::isTrue(!is_null($driveType), "An integer value named type was expected but not found.");
		
		if (!is_null($userName)) {
			if ($driveType === DriveType::PERSONAL) {
				$driveQuery = "select Concat(pd.drive_location, ifnull(ps.directory, '')) as drive_location from `pbox`.`personal_drives` pd inner join `pbox`.`people` p on p.person_id = pd.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 1) ps on (ps.person_id = p.person_id and ps.drive_id = pd.personal_drive_id) where p.user_name = '{$userName}' and pd.personal_drive_id = {$driveId}{$shortcutClause}";
			} else if ($driveType === DriveType::STORAGE) {
				$driveQuery = "select Concat(ps.storage_location, ifnull(ps2.directory, '')) as drive_location from `pbox`.`personal_storage` ps inner join `pbox`.`people` p on p.person_id = ps.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 2) ps2 on (ps2.person_id = p.person_id and ps2.drive_id = ps.personal_storage_id) where p.user_name = '{$userName}' and ps.personal_storage_id = {$driveId}{$shortcutClause}";
			} else if ($driveType === DriveType::GROUP) {
				$driveQuery = "select Concat(gd.drive_location, ifnull(ps.directory, '')) as drive_location from `pbox`.`group_drives` gd left outer join `pbox`.`groups` g on g.group_id = gd.group_id left outer join `pbox`.`people_groups` pg on pg.group_id = g.group_id left outer join `pbox`.`people` p  on p.person_id = pg.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 3) ps on (ps.person_id = p.person_id and ps.drive_id = gd.group_drive_id) where p.user_name = '{$userName}' and gd.group_drive_id = {$driveId}{$shortcutClause}";
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
			
				$absoluteDirectoryPath = sprintf('%s', $rows[0]["drive_location"]);

				$this->_logger->sendMessage(LOG_DEBUG, "Absolute Drive Location: {$absoluteDirectoryPath}");

				$driveUsageModel = new DriveUsageModel($absoluteDirectoryPath, $allottedSpace);
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
		
		self::$__orderByColumnName = $jsonObject->{FileManagementService::ORDER_BY_COLUMN_NAME};
		self::$__orderByOrder = $jsonObject->{FileManagementService::ORDER_BY_ORDER};
		
		$userName = $_SESSION["userName"];
		$locationComponents = Strings::split(($jsonObject->{FileManagementService::DRIVE_LOCATION}), "-");
		$driveType = intval($locationComponents[0]);
		$driveId = intval($locationComponents[1]);
		$directoryName = $jsonObject->{FileManagementService::DIRECTORY_NAME};
		$directories = array();
		$files = array();
		$shortcutId = intval($locationComponents[2]);
		$shortcutClause = $shortcutId == 0 ? "" : " and ps.personal_shortcut_id = {$shortcutId}";
				
		Assert::isTrue(!is_null($driveId) && !is_null($driveType), "A string value named location was expected but not found.");
		Assert::isTrue(!is_null($directoryName), "A string value named directory was expected but not found.");
		
		if (!is_null($userName)) {
			if ($driveType === DriveType::PERSONAL) {
				$driveQuery = "select Concat(pd.drive_location, ifnull(ps.directory, '')) as drive_location from `pbox`.`personal_drives` pd inner join `pbox`.`people` p on p.person_id = pd.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 1) ps on (ps.person_id = p.person_id and ps.drive_id = pd.personal_drive_id) where p.user_name = '{$userName}' and pd.personal_drive_id = {$driveId}{$shortcutClause}";
			} else if ($driveType === DriveType::STORAGE) {
				$driveQuery = "select Concat(ps.storage_location, ifnull(ps2.directory, '')) as drive_location from `pbox`.`personal_storage` ps inner join `pbox`.`people` p on p.person_id = ps.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 2) ps2 on (ps2.person_id = p.person_id and ps2.drive_id = ps.personal_storage_id) where p.user_name = '{$userName}' and ps.personal_storage_id = {$driveId}{$shortcutClause}";
			} else if ($driveType === DriveType::GROUP) {
				$driveQuery = "select Concat(gd.drive_location, ifnull(ps.directory, '')) as drive_location from `pbox`.`group_drives` gd left outer join `pbox`.`groups` g on g.group_id = gd.group_id left outer join `pbox`.`people_groups` pg on pg.group_id = g.group_id left outer join `pbox`.`people` p  on p.person_id = pg.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 3) ps on (ps.person_id = p.person_id and ps.drive_id = gd.group_drive_id) where p.user_name = '{$userName}' and gd.group_drive_id = {$driveId}{$shortcutClause}";
			}
												
			if (!Strings::isNullOrEmpty($driveQuery)) {
				$rows = self::$__queryHandler->executeQuery($driveQuery);
				
				$this->_logger->sendMessage(LOG_DEBUG, "Number of rows: " . count($rows));
				
				if (count($rows) > 0) {
					$driveLocation = $rows[0]["drive_location"];
					$absoluteDirectoryLocation = $driveLocation . (Strings::startsWith($directoryName, "/") ? $directoryName : ("/" . $directoryName));
					
					$testPath = dirname($absoluteDirectoryLocation);
					
					$this->_logger->sendMessage(LOG_DEBUG, "Test Path: {$testPath}, Absolute: {$absoluteDirectoryLocation}");
					
					if (!Strings::startsWith($testPath, $driveLocation) || strcmp($testPath, $driveLocation) < 0) {
						$absoluteDirectoryLocation = $driveLocation . "/";
					}
					
					$this->_logger->sendMessage(LOG_DEBUG, "New Abs Dir: {$absoluteDirectoryLocation}");
					
					if (is_dir($absoluteDirectoryLocation)) {
						$directoryHandle = opendir($absoluteDirectoryLocation);
						
						if (is_resource($directoryHandle)) {
							if (!Strings::equals($directoryName, "/")) {
								$this->_logger->sendMessage(LOG_DEBUG, "DirName: " . $directoryName);
								
								$directories[] = array("type" => "Directory", "name" => "..", "permissions" => 0, "size" => 0, "modifiedTime" => "");
							}
							
							while ($nextFile = readdir($directoryHandle)) {
								$nextAbsolutePath = $absoluteDirectoryLocation . $nextFile;
								
								if (!Strings::equals($nextFile, ".") && !Strings::equals($nextFile, "..") && !Strings::equals($nextFile, ".htpasswd") && !Strings::equals($nextFile, ".trash")) {
									if (is_dir($nextAbsolutePath)) {
										$directoryModel = new DirectoryModel($nextAbsolutePath);
										$modifiedTime = date("r", $directoryModel->modifiedTime);
										$directories[] = array("type" => "Directory", "name" => $nextFile, "permissions" => $directoryModel->permissions, "size" => $directoryModel->size, "modifiedTime" => $modifiedTime);
									} else {
										$fileModel = new FileModel($nextAbsolutePath);
										$modifiedTime = date("r", $fileModel->modifiedTime);
										
										$this->_logger->sendMessage(LOG_DEBUG, "File extension ({$nextFile}): {$fileModel->extension}");
										
										if (in_array(Strings::toLower($fileModel->extension), self::$__knownFileTypes, true)) {
											$files[] = array("type" => Strings::toLower($fileModel->extension), "name" => $nextFile, "permissions" => $fileModel->permissions, "size" => $fileModel->size, "modifiedTime" => $modifiedTime);
										} else {
											$files[] = array("type" => "Unknown", "name" => $nextFile, "permissions" => $fileModel->permissions, "size" => $fileModel->size, "modifiedTime" => $modifiedTime);
										}
									}
								}
							}
							
							$this->_sort = "none";
							
							if (Strings::equals(self::$__orderByOrder, "asc", true)) {
								usort($directories, array($this, "compareFileInfoModelsAsc"));
								usort($files, array($this, "compareFileInfoModelsAsc"));
		
								$this->_sort = self::$__orderByColumnName . "-" . self::$__orderByOrder;
							} else if (Strings::equals(self::$__orderByOrder, "desc", true)) {
								usort($directories, array($this, "compareFileInfoModelsDesc"));
								usort($files, array($this, "compareFileInfoModelsDesc"));
		
								$this->_sort = self::$__orderByColumnName . "-" . self::$__orderByOrder;
							}
							
							$path = Strings::substring($absoluteDirectoryLocation, Strings::length($driveLocation));
							
							$this->echoJson(json_encode(array("directoryCount" => count($directories), "fileCount" => count($files), "directories" => $directories, "files" => $files, "sort" => $this->_sort, "path" => $path)));
						} else {
							$this->echoJsonError("Directory could not be read.", "The directory specified, {$directoryName} could not be read on this system.");
						}
					} else {
						$this->echoJsonError("Directory specified is not a directory", "The directory that was specified, {$directoryName}, is not a directory on this system.");
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
	 * @return FileManagementService
	 */
	public static function getInstance() {
		if (!self::$__instance) {
			self::$__instance = new FileManagementService();
		}
		
		return self::$__instance;
	}
	
	/**
	 * Compares two directory or two file models - ascending.
	 * @param array $first
	 * @param array $second
	 * @return int
	 */
	public function compareFileInfoModelsAsc($first, $second) {
		Assert::isTrue(is_array($first), "First must be an array.");
		Assert::isTrue(is_array($second), "Second must be an array.");
		
		$actualFirst = $first["name"];
		$actualSecond = $second["name"];
		$result = 0;
			
		if (Strings::equals(self::$__orderByColumnName, "size", true)) {
			$actualFirst = $first["size"];
			$actualSecond = $second["size"];
		} else if (Strings::equals(self::$__orderByColumnName, "type", true)) {
			$actualFirst = $first["type"];
			$actualSecond = $second["type"];
		} else if (Strings::equals(self::$__orderByColumnName, "modifiedTime", true)) {
			$actualFirst = $first["modifiedTime"];
			$actualSecond = $second["modifiedTime"];
		}

		$result = strcasecmp($actualFirst, $actualSecond);
		
		return $result;
	}	
	
	/**
	 * Compares two directory or two file models - descending.
	 * @param array $first
	 * @param array $second
	 * @return int
	 */
	public function compareFileInfoModelsDesc($first, $second) {
		Assert::isTrue(is_array($first), "First must be an array.");
		Assert::isTrue(is_array($second), "Second must be an array.");
		
		$actualFirst = $first["name"];
		$actualSecond = $second["name"];
		$result = 0;
			
		if (Strings::equals(self::$__orderByColumnName, "size", true)) {
			$actualFirst = $first["size"];
			$actualSecond = $second["size"];
		} else if (Strings::equals(self::$__orderByColumnName, "type", true)) {
			$actualFirst = $first["type"];
			$actualSecond = $second["type"];
		} else if (Strings::equals(self::$__orderByColumnName, "modifiedTime", true)) {
			$actualFirst = $first["modifiedTime"];
			$actualSecond = $second["modifiedTime"];
		}

		$result = (strcasecmp($actualFirst, $actualSecond) * (-1));
		
		return $result;
	}
	
	/**
	 * editFile
	 * Creates or edits file in the current directory.
	 * @param object $jsonObject
	 */
	public function editFile(/*object*/ $jsonObject) {
		ArgumentTypeValidator::isObject($jsonObject, "JsonObject must be an object.");
		
		$userName = $_SESSION["userName"];
		$locationComponents = Strings::split(($jsonObject->{FileManagementService::DRIVE_LOCATION}), "-");
		$driveType = intval($locationComponents[0]);
		$driveId = intval($locationComponents[1]);
		$directoryName = $jsonObject->{FileManagementService::DIRECTORY_NAME};
		$fileName = $jsonObject->{FileManagementService::FILE_NAME};
		$fileContents = $jsonObject->{FileManagementService::FILE_CONTENTS};
		$shortcutId = intval($locationComponents[2]);
		$shortcutClause = $shortcutId == 0 ? "" : " and ps.personal_shortcut_id = {$shortcutId}";
		
		$fileContents = Strings::replace($fileContents, "\\r", "\r");
		$fileContents = Strings::replace($fileContents, "\\n", "\n");
		$fileContents = Strings::replace($fileContents, "\\t", "\t");
		$fileContents = Strings::replace($fileContents, "\\\"", "\"");
		
		$this->_logger->sendMessage(LOG_DEBUG, "Saving: {$fileContents}");
		
		Assert::isTrue(!is_null($driveId) && !is_null($driveType), "A string value named location was expected but not found.");
		Assert::isTrue(!is_null($directoryName), "A string value named directory was expected but not found.");

		if (!is_null($userName)) {
			if ($driveType === DriveType::PERSONAL) {
				$driveQuery = "select Concat(pd.drive_location, ifnull(ps.directory, '')) as drive_location from `pbox`.`personal_drives` pd inner join `pbox`.`people` p on p.person_id = pd.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 1) ps on (ps.person_id = p.person_id and ps.drive_id = pd.personal_drive_id) where p.user_name = '{$userName}' and pd.personal_drive_id = {$driveId}{$shortcutClause}";
			} else if ($driveType === DriveType::STORAGE) {
				$driveQuery = "select Concat(ps.storage_location, ifnull(ps2.directory, '')) as drive_location from `pbox`.`personal_storage` ps inner join `pbox`.`people` p on p.person_id = ps.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 2) ps2 on (ps2.person_id = p.person_id and ps2.drive_id = ps.personal_storage_id) where p.user_name = '{$userName}' and ps.personal_storage_id = {$driveId}{$shortcutClause}";
			} else if ($driveType === DriveType::GROUP) {
				$driveQuery = "select Concat(gd.drive_location, ifnull(ps.directory, '')) as drive_location from `pbox`.`group_drives` gd left outer join `pbox`.`groups` g on g.group_id = gd.group_id left outer join `pbox`.`people_groups` pg on pg.group_id = g.group_id left outer join `pbox`.`people` p  on p.person_id = pg.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 3) ps on (ps.person_id = p.person_id and ps.drive_id = gd.group_drive_id) where p.user_name = '{$userName}' and gd.group_drive_id = {$driveId}{$shortcutClause}";
			}
												
			if (!Strings::isNullOrEmpty($driveQuery)) {
				$rows = self::$__queryHandler->executeQuery($driveQuery);
				
				$this->_logger->sendMessage(LOG_DEBUG, "Number of rows: " . count($rows));
				
				if (count($rows) > 0) {
					$driveLocation = $rows[0]["drive_location"];
					$absoluteDirectoryLocation = $driveLocation . (Strings::startsWith($directoryName, "/") ? $directoryName : ("/" . $directoryName));
					
					$testPath = dirname($absoluteDirectoryLocation);
					
					$this->_logger->sendMessage(LOG_DEBUG, "Test Path: {$testPath}; Drive Location: {$driveLocation}");
					
					if (!Strings::startsWith($testPath, $driveLocation) || strcmp($testPath, $driveLocation) < 0) {
						$absoluteDirectoryLocation = $driveLocation . "/";
					}
					
					if (is_dir($absoluteDirectoryLocation)) {
						$newFilePath = $absoluteDirectoryLocation . "/" . $fileName;
						
						$this->_logger->sendMessage(LOG_DEBUG, "New file path: {$newFilePath}");

						$newFileHandle = fopen($newFilePath, "w");
						
						if (is_resource($newFileHandle)) {
							$this->_logger->sendMessage(LOG_DEBUG, "Writing file {$newFilePath}.");
							
							fwrite($newFileHandle, $fileContents);
							fclose($newFileHandle);
							
							$newFileHandle = null;
						} else {
							$this->_logger->sendMessage(LOG_DEBUG, "File could not be written to.");
						}
					}
				}
			}
		}
	}
	
	/**
	 * createNewFolder
	 * Creates a new folder in the current directory if it doesn't already exist.
	 * @param object $jsonObject
	 */
	public function createNewFolder(/*object*/ $jsonObject) {
		ArgumentTypeValidator::isObject($jsonObject, "JsonObject must be an object.");
		
		$userName = $_SESSION["userName"];
		$locationComponents = Strings::split(($jsonObject->{FileManagementService::DRIVE_LOCATION}), "-");
		$driveType = intval($locationComponents[0]);
		$driveId = intval($locationComponents[1]);
		$directoryName = $jsonObject->{FileManagementService::DIRECTORY_NAME};
		$folderName = $jsonObject->{FileManagementService::FOLDER_NAME};
		$shortcutId = intval($locationComponents[2]);
		$shortcutClause = $shortcutId == 0 ? "" : " and ps.personal_shortcut_id = {$shortcutId}";
		
		Assert::isTrue(!is_null($driveId) && !is_null($driveType), "A string value named location was expected but not found.");
		Assert::isTrue(!is_null($directoryName), "A string value named directory was expected but not found.");

		if (!is_null($userName)) {
			if ($driveType === DriveType::PERSONAL) {
				$driveQuery = "select Concat(pd.drive_location, ifnull(ps.directory, '')) as drive_location from `pbox`.`personal_drives` pd inner join `pbox`.`people` p on p.person_id = pd.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 1) ps on (ps.person_id = p.person_id and ps.drive_id = pd.personal_drive_id) where p.user_name = '{$userName}' and pd.personal_drive_id = {$driveId}{$shortcutClause}";
			} else if ($driveType === DriveType::STORAGE) {
				$driveQuery = "select Concat(ps.storage_location, ifnull(ps2.directory, '')) as drive_location from `pbox`.`personal_storage` ps inner join `pbox`.`people` p on p.person_id = ps.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 2) ps2 on (ps2.person_id = p.person_id and ps2.drive_id = ps.personal_storage_id) where p.user_name = '{$userName}' and ps.personal_storage_id = {$driveId}{$shortcutClause}";
			} else if ($driveType === DriveType::GROUP) {
				$driveQuery = "select Concat(gd.drive_location, ifnull(ps.directory, '')) as drive_location from `pbox`.`group_drives` gd left outer join `pbox`.`groups` g on g.group_id = gd.group_id left outer join `pbox`.`people_groups` pg on pg.group_id = g.group_id left outer join `pbox`.`people` p  on p.person_id = pg.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 3) ps on (ps.person_id = p.person_id and ps.drive_id = gd.group_drive_id) where p.user_name = '{$userName}' and gd.group_drive_id = {$driveId}{$shortcutClause}";
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
						// TODO: Create the file or don't
						$newFolderPath = $absoluteDirectoryLocation . "/" . $folderName;

						if (!file_exists($newFilePath)) {
							mkdir($newFolderPath);
						}
					}
				}
			}
		}
	}
	
	/**
	 * deleteFile
	 * Deletes a file.
	 * @param object $jsonObject
	 */
	public function deleteFilesAndFolders(/*object*/ $jsonObject) {
		ArgumentTypeValidator::isObject($jsonObject, "JsonObject must be an object.");
		
		$userName = $_SESSION["userName"];
		$locationComponents = Strings::split(($jsonObject->{FileManagementService::DRIVE_LOCATION}), "-");
		$driveType = intval($locationComponents[0]);
		$driveId = intval($locationComponents[1]);
		$directoryName = $jsonObject->{FileManagementService::DIRECTORY_NAME};
		$fileNames = $jsonObject->{FileManagementService::FILE_NAMES};
		$shortcutId = intval($locationComponents[2]);
		$shortcutClause = $shortcutId == 0 ? "" : " and ps.personal_shortcut_id = {$shortcutId}";
		
		Assert::isTrue(!is_null($driveId) && !is_null($driveType), "A string value named location was expected but not found.");
		Assert::isTrue(!is_null($directoryName), "A string value named directory was expected but not found.");

		if (!is_null($userName)) {
			if ($driveType === DriveType::PERSONAL) {
				$driveQuery = "select Concat(pd.drive_location, ifnull(ps.directory, '')) as drive_location from `pbox`.`personal_drives` pd inner join `pbox`.`people` p on p.person_id = pd.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 1) ps on (ps.person_id = p.person_id and ps.drive_id = pd.personal_drive_id) where p.user_name = '{$userName}' and pd.personal_drive_id = {$driveId}{$shortcutClause}";
			} else if ($driveType === DriveType::STORAGE) {
				$driveQuery = "select Concat(ps.storage_location, ifnull(ps2.directory, '')) as drive_location from `pbox`.`personal_storage` ps inner join `pbox`.`people` p on p.person_id = ps.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 2) ps2 on (ps2.person_id = p.person_id and ps2.drive_id = ps.personal_storage_id) where p.user_name = '{$userName}' and ps.personal_storage_id = {$driveId}{$shortcutClause}";
			} else if ($driveType === DriveType::GROUP) {
				$driveQuery = "select Concat(gd.drive_location, ifnull(ps.directory, '')) as drive_location from `pbox`.`group_drives` gd left outer join `pbox`.`groups` g on g.group_id = gd.group_id left outer join `pbox`.`people_groups` pg on pg.group_id = g.group_id left outer join `pbox`.`people` p  on p.person_id = pg.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 3) ps on (ps.person_id = p.person_id and ps.drive_id = gd.group_drive_id) where p.user_name = '{$userName}' and gd.group_drive_id = {$driveId}{$shortcutClause}";
			}
												
			if (!Strings::isNullOrEmpty($driveQuery)) {
				$rows = self::$__queryHandler->executeQuery($driveQuery);
				
				$this->_logger->sendMessage(LOG_DEBUG, "Number of rows: " . count($rows));
				
				if (count($rows) > 0) {
					$driveLocation = $rows[0]["drive_location"];
					$absoluteDirectoryLocation = $driveLocation . (Strings::startsWith($directoryName, "/") ? $directoryName : ("/" . $directoryName));
					
					$testPath = dirname($absoluteDirectoryLocation);
					
					$this->_logger->sendMessage(LOG_DEBUG, "Test Path: {$testPath}; Drive Location: {$driveLocation}");
					
					if (!Strings::startsWith($testPath, $driveLocation) || strcmp($testPath, $driveLocation) < 0) {
						$absoluteDirectoryLocation = $driveLocation . "/";
					}
					
					if (is_dir($absoluteDirectoryLocation)) {
						$newFilePath = $absoluteDirectoryLocation . "/" . $fileName;
						
						$this->_logger->sendMessage(LOG_DEBUG, "New file path: {$newFilePath}");

						$newFileHandle = fopen($newFilePath, "w");
						
						if (is_resource($newFileHandle)) {
							$this->_logger->sendMessage(LOG_DEBUG, "Deleting file {$newFilePath}.");
							
							fclose($newFileHandle);
							
							if (unlink($newFilePath)) {
								$this->echoJson(array("success" => true));
							} else {
								$this->echoJson(array("success" => false));
							}
							
							$newFileHandle = null;
						} else {
							$this->_logger->sendMessage(LOG_DEBUG, "File could not be written to.");
						}
					}
				}
			}
		}
	}
	
}

$fileManagementService = FileManagementService::getInstance();
$fileManagementService->dispatch();

?>
