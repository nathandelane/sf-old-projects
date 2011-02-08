<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandler.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IPage.inc.php");

/**
 * ProfileModel
 * This class represents the profile model, which is both readable and writable.
 * @author lanathan
 *
 */
class ProfileModel {
	
	private static $__queryHandler;
	
	private $_userName;
	private $_model;
	
	/**
	 * Constructor
	 * @param IPage $page
	 * @return ProfileModel
	 */
	public function ProfileModel(IPage $page) {
		$this->_userName = $page->getSessionFieldValue("userName");
		
		if (!isset(self::$__queryHandler)) {
			self::$__queryHandler = QueryHandler::getInstance(QueryHandlerType::MYSQL);
		}
		
		$query = "select p.real_name, p.explicity, p.bio, p.date_of_birth from `pbox`.`people` p where user_name = '$this->_userName'";		
		$rows = self::$__queryHandler->executeQuery($query);
		
		if (count($rows) > 0) {
			$this->_model = $rows[0];
		} else {
			$this->_model = array();
		}
	}
	
	/**
	 * __get
	 * Gets a field value if the field exists.
	 * @param string $fieldName
	 * @return string
	 */
	public function __get(/*string*/ $fieldName) {
		$result = null;
		
		if (array_key_exists($fieldName, $this->_model)) {
			$result = $this->_model[$fieldName];
		}
		
		return $result;
	}
	
	/**
	 * __set
	 * Sets a field value if the field exists.
	 * @param string $fieldName
	 * @param string $value
	 * @return void
	 */
	public function __set(/*string*/ $fieldName, /*mixed*/ $value) {
		$update = null;
		
		if (array_key_exists($fieldName, $this->_model)) {
			if (Strings::equals($fieldName, "real_name") || Strings::equals($fieldName, "personal_drive_space") || Strings::equals($fieldName, "explicit") || Strings::equals($fieldName, "email") || Strings::equals($fieldName, "state") || Strings::equals($fieldName, "city") || Strings::equals($fieldName, "bio")) {
				$update = "update `pbox`.`people` set `$fieldName` = " . (is_numeric($value) ? "$value" : "'$value'") . " where user_name = '$this->_userName'"; 
			}
			
			if (!is_null($update)) {
				self::$__queryHandler->executeQuery($update);
			}
		}
	}
	
}

?>