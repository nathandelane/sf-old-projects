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
		$personalDriveQuery = "select p.person_id, pd.personal_drive_id, Concat(pd.drive_location, ifnull(ps.directory, '')) as drive_location, pd.additional_space, atdd.alotted_space, ps.name as shortcut_name, ps.personal_shortcut_id  from `pbox`.`personal_drives` pd inner join `pbox`.`people` p on p.person_id = pd.person_id inner join `pbox`.`account_types` a on a.account_type_id = p.account_type_id left outer join `pbox`.`account_type_folders` atf on atf.account_type_id = p.account_type_id inner join `pbox`.`account_type_default_data` atdd on atdd.account_type_default_data_id = atf.account_type_default_data_id inner join `pbox`.`storage_types` st on st.storage_type_id = atf.storage_type_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 1) ps on (ps.person_id = p.person_id and ps.drive_id = pd.personal_drive_id) where p.user_name = '{$this->_userName}' and st.name = 'Webspace'";
		$rows = self::$__queryHandler->executeQuery($personalDriveQuery);
		
		if (count($rows) > 0) {
			foreach ($rows as $nextRow) {
				$shortcutId = Strings::isNullOrEmpty($nextRow["personal_shortcut_id"]) ? 0 : intval($nextRow["personal_shortcut_id"]);
				$driveShortcutName = Strings::isNullOrEmpty($nextRow["shortcut_name"]) ? "Personal Drive" : $nextRow["shortcut_name"];
				$nextShortcut = new DriveShortcut(intval($nextRow["personal_drive_id"]), $driveShortcutName, $nextRow["drive_location"], (doubleval($nextRow["alotted_space"]) + doubleval($nextRow["additional_space"])), DriveType::PERSONAL, $shortcutId);
				
				$newKey = sprintf('%d-%d-%d', DriveType::PERSONAL, $nextRow["personal_drive_id"], $shortcutId);
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
		$storageQuery = "select ps.personal_storage_id, Concat(ps.storage_location, ifnull(ps2.directory, '')) as storage_location, ps.additional_space, atdd.alotted_space, ps2.name as shortcut_name, ps2.personal_shortcut_id  from `pbox`.`personal_storage` ps inner join `pbox`.`people` p on p.person_id = ps.person_id inner join `pbox`.`account_types` a on a.account_type_id = p.account_type_id left outer join `pbox`.`account_type_folders` atf on atf.account_type_id = p.account_type_id inner join `pbox`.`account_type_default_data` atdd on atdd.account_type_default_data_id = atf.account_type_default_data_id inner join `pbox`.`storage_types` st on st.storage_type_id = atf.storage_type_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 2) ps2 on (ps2.person_id = p.person_id and ps2.drive_id = ps.personal_storage_id) where p.user_name = '{$this->_userName}' and st.name = 'Personal Storage'";
		$rows = self::$__queryHandler->executeQuery($storageQuery);
		
		if (count($rows) > 0) {
			foreach ($rows as $nextRow) {
				$shortcutId = Strings::isNullOrEmpty($nextRow["personal_shortcut_id"]) ? 0 : intval($nextRow["personal_shortcut_id"]);
				$driveShortcutName = Strings::isNullOrEmpty($nextRow["shortcut_name"]) ? "Personal Storage" : $nextRow["shortcut_name"];
				$nextShortcut = new DriveShortcut(intval($nextRow["personal_storage_id"]), $driveShortcutName, $nextRow["storage_location"], doubleval($nextRow["alotted_space"]), DriveType::STORAGE, $shortcutId);
				
				$newKey = sprintf('%d-%d-%d', DriveType::STORAGE, $nextRow["personal_storage_id"], $shortcutId);
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
		$storageQuery = "select gd.group_drive_id, Concat(gd.drive_location, ifnull(ps.directory, '')) as drive_location, gd.allotted_space, g.name, ps.name as shortcut_name, ps.personal_shortcut_id  from `pbox`.`group_drives` gd inner join `pbox`.`groups` g on g.group_id = gd.group_id inner join `pbox`.`people_groups` pg on pg.group_id = g.group_id inner join `pbox`.`people` p on p.person_id = pg.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 3) ps on (ps.person_id = p.person_id and ps.drive_id = gd.group_drive_id) where p.user_name = '{$this->_userName}'";
		$rows = self::$__queryHandler->executeQuery($storageQuery);
		
		if (count($rows) > 0) {
			foreach ($rows as $nextRow) {
				$shortcutId = Strings::isNullOrEmpty($nextRow["personal_shortcut_id"]) ? 0 : intval($nextRow["personal_shortcut_id"]);
				$driveShortcutName = Strings::isNullOrEmpty($nextRow["shortcut_name"]) ? "{$nextRow["name"]} Group Drive" : $nextRow["shortcut_name"];
				$nextShortcut = new DriveShortcut(intval($nextRow["group_drive_id"]), $driveShortcutName, $nextRow["drive_location"], doubleval($nextRow["allotted_space"]), DriveType::GROUP, $shortcutId);
				
				$newKey = sprintf('%d-%d-%d', DriveType::GROUP, $nextRow["group_drive_id"], $shortcutId);
				$this->_model[$newKey]= $nextShortcut;
			}
		}
	}
	
}
