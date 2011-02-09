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

/**
 * RegistrationService
 * This service provides methods useful during registration.
 * @author lanathan
 *
 */
class RegistrationService extends JsonWebServiceBase {
	
	private static $__instance;
	private static $__queryHandler;
	
	/**
	 * Constructor
	 * @return RegistrationService
	 */
	protected function RegistrationService() {
		parent::__construct("RegistrationService");
		
		if (!isset(self::$__queryHandler)) {
			self::$__queryHandler = QueryHandler::getInstance(QueryHandlerType::MYSQL);
		}

//		$this->registerServiceMethod("getDiskSpaceUsedForDrive", array("name: string", "location: string", "type: int"), "Gets the total amount of disk space used by files and messages at a specific location.");
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

}

$registrationService = RegistrationService::getInstance();
$registrationService->dispatch();

?>
