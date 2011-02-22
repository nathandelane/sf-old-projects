<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/collections/HashCollection.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/collections/IEnumerable.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/collections/IReadable.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandler.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IPage.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "types/DriveShortcut.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "types/DriveType.inc.php");

class DrivesModel implements IEnumerable, IReadable {
	
	private static $__queryHandler;
	
	private $_userName;
	private $_model;
	
	/**
	 * Constructor
	 * @param IPage $page
	 * @return DrivesModel
	 */
	public function DrivesModel(IPage $page) {
		$this->_userName = $page->getSessionFieldValue("userName");
		$this->_model = array();
		
		if (!isset(self::$__queryHandler)) {
			self::$__queryHandler = QueryHandler::getInstance(QueryHandlerType::MYSQL);
		}
		
		$this->_collectPersonalDrives();
		$this->_collectPersonalStorage();
		$this->_collectGroupDrives();
		
		$page->getLogger()->sendMessage(LOG_DEBUG, print_r($this->_model, true));
	}

	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IReadable::get()
	 */
	public function get(/*string*/ $key) {
		ArgumentTypeValidator::isString($key, "Key must be a string.");
		
		$value = null;
		
		if (array_key_exists($key, $this->_model)) {
			$value = $this->_model[$key];
		}
		
		return $value;
	}

	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IReadable::getFirstOrDefault()
	 */
	public function getFirstOrDefault() {
		$firstOrDefault = null;
		
		if (count($this->_model) > 0) {
			$values = array_values($this->_model);
			$firstOrDefault = $values[0];
		}
		
		return $firstOrDefault;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IEnumerable::getEnumerator()
	 */
	public function getEnumerator() {
		$modelHashCollection = new HashCollection($this->_model);
		
		return $modelHashCollection->getEnumerator();
	}
	
	/**
	 * _collectPersonalDrives
	 * Collects information about personal drives.
	 * @return void
	 */
	private function _collectPersonalDrives() {
		$personalDriveQuery = "select pd.personal_drive_id, pd.drive_location, pd.additional_space, atdd.alotted_space from `pbox`.`personal_drives` pd inner join `pbox`.`people` p on p.person_id = pd.person_id inner join `pbox`.`account_types` a on a.account_type_id = p.account_type_id left outer join `pbox`.`account_type_folders` atf on atf.account_type_id = p.account_type_id inner join `pbox`.`account_type_default_data` atdd on atdd.account_type_default_data_id = atf.account_type_default_data_id inner join `pbox`.`storage_types` st on st.storage_type_id = atf.storage_type_id where p.user_name = '{$this->_userName}' and st.name = 'Webspace'";
		$rows = self::$__queryHandler->executeQuery($personalDriveQuery);
		
		if (count($rows) > 0) {
			foreach ($rows as $nextRow) {
				$nextShortcut = new DriveShortcut(intval($nextRow["personal_drive_id"]), "Personal Drive", $nextRow["drive_location"], (doubleval($nextRow["alotted_space"]) + doubleval($nextRow["additional_space"])), DriveType::PERSONAL);
				
				$newKey = sprintf('%d-%d', DriveType::PERSONAL, $nextRow["personal_drive_id"]);
				$this->_model[$newKey] = $nextShortcut;
			}
		}
	}
	
	/**
	 * _collectPersonalStorage
	 * Collects information about personal storage.
	 * @return void
	 */
	private function _collectPersonalStorage() {
		$storageQuery = "select ps.personal_storage_id, ps.storage_location, ps.additional_space, atdd.alotted_space from `pbox`.`personal_storage` ps inner join `pbox`.`people` p on p.person_id = ps.person_id inner join `pbox`.`account_types` a on a.account_type_id = p.account_type_id left outer join `pbox`.`account_type_folders` atf on atf.account_type_id = p.account_type_id inner join `pbox`.`account_type_default_data` atdd on atdd.account_type_default_data_id = atf.account_type_default_data_id inner join `pbox`.`storage_types` st on st.storage_type_id = atf.storage_type_id where p.user_name = '{$this->_userName}' and st.name = 'Personal Storage'";
		$rows = self::$__queryHandler->executeQuery($storageQuery);
		
		if (count($rows) > 0) {
			foreach ($rows as $nextRow) {
				$nextShortcut = new DriveShortcut(intval($nextRow["personal_storage_id"]), "Personal Storage", $nextRow["storage_location"], doubleval($nextRow["alotted_space"]), DriveType::STORAGE);
				
				$newKey = sprintf('%d-%d', DriveType::STORAGE, $nextRow["personal_storage_id"]);
				$this->_model[$newKey]= $nextShortcut;
			}
		}
	}
	
	/**
	 * _collectGroupDrives
	 * Collects information about group drives that the user is associated with.
	 * @return void
	 */
	private function _collectGroupDrives() {
		$storageQuery = "select gd.group_drive_id, gd.drive_location, gd.allotted_space, g.name from `pbox`.`group_drives` gd inner join `pbox`.`groups` g on g.group_id = gd.group_id inner join `pbox`.`people_groups` pg on pg.group_id = g.group_id inner join `pbox`.`people` p on p.person_id = pg.person_id where p.user_name = '{$this->_userName}'";
		$rows = self::$__queryHandler->executeQuery($storageQuery);
		
		if (count($rows) > 0) {
			foreach ($rows as $nextRow) {
				$nextShortcut = new DriveShortcut(intval($nextRow["group_drive_id"]), "{$nextRow["name"]} Group Drive", $nextRow["drive_location"], doubleval($nextRow["allotted_space"]), DriveType::GROUP);
				
				$newKey = sprintf('%d-%d', DriveType::GROUP, $nextRow["group_drive_id"]);
				$this->_model[$newKey]= $nextShortcut;
			}
		}
	}
	
}
