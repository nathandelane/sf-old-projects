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
	
	const USER_NAME = "username";
	
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

		$this->registerServiceMethod("checkUserNameAvailability", array("username: string", ), " Checks the database to determine whether the username chosen already exists or not.");
	}
	
	/**
	 * checkUserNameAvailability
	 * Checks the database to determine whether the username chosen already exists or not.
	 * @param object $jsonObject
	 */
	public function checkUserNameAvailability(/*object*/ $jsonObject) {
		ArgumentTypeValidator::isObject($jsonObject, "JsonObject must be an object.");
		
		$userName = $jsonObject->{RegistrationService::USER_NAME};
		
		if (!Strings::isNullOrEmpty($userName)) {
			$query = "select user_name from `pbox`.`people` where user_name = '{$userName}'";
			
			if (!Strings::isNullOrEmpty($query)) {
				$rows = self::$__queryHandler->executeQuery($query);
				
				if (count($rows) > 0) {
					$this->echoJson(json_encode(array("userNameIsInUse" => true)));
				} else {
					$this->echoJson(json_encode(array("userNameIsInUse" => false)));
				}
			}
		} else {
			$this->echoJson(json_encode(array("userNameIsInUse" => false)));
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
	 * @return RegistrationService
	 */
	public static function getInstance() {
		if (!self::$__instance) {
			self::$__instance = new RegistrationService();
		}
		
		return self::$__instance;
	}

}

$registrationService = RegistrationService::getInstance();
$registrationService->dispatch();

?>
