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
require_once(PhyleBox_Config::getLocalFoundationLocation() . "data/DriveUsageModel.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "types/DriveType.inc.php");

/**
 * FileManagementService
 * This service provides means to manage files.
 * @author lanathan
 *
 */
class FileManagementService extends JsonWebServiceBase {
	
	const USER_NAME = "userName";
	const DRIVE_NAME = "name";
	const DRIVE_LOCATION = "location";
	const DRIVE_TYPE = "type";
	
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
		
		Assert::isTrue(isset($jsonObject->{FileManagementService::DRIVE_NAME}), "A string value named name was expected but not found.");
		Assert::isTrue(isset($jsonObject->{FileManagementService::DRIVE_LOCATION}), "A string value named location was expected but not found.");
		Assert::isTrue(isset($jsonObject->{FileManagementService::DRIVE_TYPE}), "An integer value named type was expected but not found.");
		
		$userName = $_SESSION["userName"];
		$driveLocation = $jsonObject->{FileManagementService::DRIVE_LOCATION};
		$driveType = intval($jsonObject->{FileManagementService::DRIVE_TYPE});
		$allottedSpace = 0;
		$driveQuery = "";
		
		if (!is_null($userName)) {
			if ($driveType === DriveType::PERSONAL) {
				$driveQuery = "select pd.additional_space, at.allotted_space from `pbox`.`personal_drives` pd, `pbox`.`account_types` at, `pbox`.`people` p where p.user_name = '{$userName}' and pd.drive_location = '{$driveLocation}' and pd.person = p.id";
			} else if ($driveType === DriveType::STORAGE) {
				$driveQuery = "select ps.allotted_space from `pbox`.`personal_storage` ps, `pbox`.`people` p where p.user_name = '{$userName}' and ps.storage_location = '{$driveLocation}' and ps.person = p.id";
			} else if ($driveType === DriveType::GROUP) {
				$driveQuery = "select gd.allotted_space from `pbox`.`group_drives` gd, `pbox`.`groups` g, `pbox`.`people_groups` pg, `pbox`.`people` p where p.user_name = '{$userName}' and g.person = p.id and g.group = pg.id and gd.group = pg.id and gd.drive_location = '{$driveLocation}'";
			}
			
			if (!Strings::isNullOrEmpty($driveQuery)) {
				$rows = self::$__queryHandler->executeQuery($driveQuery);
				
				if (count($rows) > 0) {
					$allottedSpace = intval($rows[0]["allottedSpace"]);
					
					if (array_key_exists("additional_space", $rows[0])) {
						$allottedSpace += intval($rows[0]["additional_space"]);
					}
				}
			}
			
			$driveUsageModel = new DriveUsageModel($driveLocation, $allottedSpace);
			
			$this->echoJson(json_encode(array("kilobytes" => $driveUsageModel->kilobytes, "percentage" => $driveUsageModel->percentage)));
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