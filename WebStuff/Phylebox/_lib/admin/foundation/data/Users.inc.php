<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Admin_Config::getFrameworkFoundation() . "data/QueryHandler.inc.php");
require_once(Admin_Config::getFrameworkFoundation() . "data/QueryHandlerType.inc.php");
require_once(Admin_Config::getFrameworkFoundation() . "collections/IEnumerable.inc.php");
require_once(Admin_Config::getFrameworkFoundation() . "collections/IEnumerator.inc.php");

/**
 * Users class
 * Represents a model of users from the database.
 * @author nalane
 *
 */
class Users implements IEnumerable {
	
	private static $__queryHandler;
	
	private $_users;
	
	/**
	 * Constructor
	 * Creates an instance of Users.
	 * @return Users
	 */
	public function Users() {
		if (!isset(self::$__queryHandler)) {
			self::$__queryHandler = QueryHandler::getInstance(QueryHandlerType::MYSQL);
		}
		
		$query = "select p.user_name, p.first_real_name, p.last_real_name, p.is_disabled_by_phyer, p.is_locked, e.name as content_type, p.merits, aty.name as account_type, dr.name as disabled_reason, p.merits, p.date_of_birth, p.date_created, p.signature, p.bio from `pbox`.`people` p inner join `pbox`.`account_types` aty on aty.account_type_id = p.account_type_id left outer join `pbox`.`explicity` e on e.explicity_id = p.explicity_id left outer join `pbox`.`disabled_reasons` dr on dr.disabled_reason_id = p.disabled_reason";
		
		$this->_users = self::$__queryHandler->executeQuery($query);
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IEnumerable::getEnumerator()
	 */
	public function getEnumerator() {
		return new UsersIterator($this->_users);
	}
	
}

/**
 * UsersIterator
 * Wraps the users collection.
 * @author nalane
 *
 */
class UsersIterator implements IEnumerator {
	
	private $_users;
	private $_enumeratorIndex;
	
	/**
	 * Constructor
	 * Creates an instance of UsersIterator.
	 * @param array $users
	 * @return UsersIterator
	 */
	public function UsersIterator(array $users) {
		$this->_users = $users;
		
		$this->reset();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IEnumerator::moveNext()
	 */
	public function moveNext() {
		$result = null;
		
		if ($this->_enumeratorIndex < count($this->_users)) {
			$result = $this->_users[$this->_enumeratorIndex];
			
			$this->_enumeratorIndex++;
		}
		
		return $result;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IEnumerator::reset()
	 */
	public function reset() {
		$this->_enumeratorIndex = 0;
	}
	
}

?>