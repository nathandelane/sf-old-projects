<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/collections/ArrayList.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/collections/IEnumerable.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandler.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IPage.inc.php");

class RolesModel implements IEnumerable {
	
	private static $__queryHandler;
	
	private $_userName;
	private $_model;
	
	/**
	 * Constructor
	 * @param IPage $page
	 * @return RolesModel
	 */
	public function RolesModel(IPage $page) {
		$this->_userName = $page->getSessionFieldValue("userName");
		
		if (!isset(self::$__queryHandler)) {
			self::$__queryHandler = QueryHandler::getInstance(QueryHandlerType::MYSQL);
		}
		
		$query = "select pr.name from `pbox`.`people_roles` pr, `pbox`.`roles` r, `pbox`.`people` p where pr.id = r.role and r.person = p.id and p.user_name = '$this->_userName'";		
		$rows = self::$__queryHandler->executeQuery($query);
		
		if (count($rows) > 0) {
			$this->_model = $rows;
		} else {
			$this->_model = array();
		}
		
		$page->getLogger()->sendMessage(LOG_DEBUG, print_r($this->_model, true));
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IEnumerable::getEnumerator()
	 */
	public function getEnumerator() {
		$modelArrayList = new ArrayList($this->_model);
		
		return $modelArrayList->getEnumerator();
	}
	
}