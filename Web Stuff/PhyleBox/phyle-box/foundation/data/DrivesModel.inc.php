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
		$personalDriveQuery = "select pd.id, pd.drive_location, pd.additional_space, at.allotted_space from `pbox`.`personal_drives` pd, `pbox`.`account_types` at, `pbox`.`people` p where p.user_name = '{$this->_userName}' and at.id = p.account_type and pd.person = p.id";
		$rows = self::$__queryHandler->executeQuery($personalDriveQuery);
		
		if (count($rows) > 0) {
			foreach ($rows as $nextRow) {
				$nextShortcut = new DriveShortcut(intval($nextRow["id"]), "Personal Drive", $nextRow["drive_location"], (doubleval($nextRow["allotted_space"]) + doubleval($nextRow["additional_space"])), DriveType::PERSONAL);
				
				$this->_model[sprintf('%1$s-%2$s', DriveType::PERSONAL, $nextRow["id"])] = $nextShortcut;
			}
		}
	}
	
	/**
	 * _collectPersonalStorage
	 * Collects information about personal storage.
	 * @return void
	 */
	private function _collectPersonalStorage() {
		$storageQuery = "select ps.id, ps.storage_location, ps.allotted_space from `pbox`.`personal_storage` ps, `pbox`.`people` p where p.user_name = '{$this->_userName}' and ps.person = p.id";
		$rows = self::$__queryHandler->executeQuery($storageQuery);
		
		if (count($rows) > 0) {
			foreach ($rows as $nextRow) {
				$nextShortcut = new DriveShortcut(intval($nextRow["id"]), "Personal Storage", $nextRow["storage_location"], doubleval($nextRow["allotted_space"]), DriveType::STORAGE);
				
				$this->_model[sprintf('%1$s-%2$s', DriveType::STORAGE, $nextRow["id"])] = $nextShortcut;
			}
		}
	}
	
	/**
	 * _collectGroupDrives
	 * Collects information about group drives that the user is associated with.
	 * @return void
	 */
	private function _collectGroupDrives() {
		$storageQuery = "select gd.id, gd.drive_location, gd.allotted_space, pg.name from `pbox`.`group_drives` gd, `pbox`.`groups` g, `pbox`.`people_groups` pg, `pbox`.`people` p where p.user_name = '{$this->_userName}' and g.person = p.id and g.group = pg.id and gd.group = pg.id";
		$rows = self::$__queryHandler->executeQuery($storageQuery);
		
		if (count($rows) > 0) {
			foreach ($rows as $nextRow) {
				$nextShortcut = new DriveShortcut(intval($nextRow["id"]), "{$nextRow["name"]} Group Drive", $nextRow["drive_location"], doubleval($nextRow["allotted_space"]), DriveType::GROUP);
				
				$this->_model[sprintf('%1$s-%2$s', DriveType::GROUP, $nextRow["id"])] = $nextShortcut;
			}
		}
	}
	
}