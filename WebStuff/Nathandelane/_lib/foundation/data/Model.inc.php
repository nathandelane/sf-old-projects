<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/data/Condition.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/data/QueryHandler.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");

abstract class Model {
	
	private static $__queryHandler;
	
	private $_sqlTableName;
	private $_columns;
	
	/**
	 * Constructor
	 * @param string $sqlTableName
	 * @return Model
	 */
	protected function Model(/*string*/ $sqlTableName) {
		if (!isset(self::$__queryHandler)) {
			self::$__queryHandler = QueryHandler::getInstance();
		}
		
		$this->_sqlTableName = $sqlTableName;
		$this->_columns = array();
	}
	
	/**
	 * __get
	 * Gets the value of a particular column.
	 * @param string $columnName
	 */
	public function __get(/*string*/ $columnName) {
		ArgumentTypeValidator::isString($columnName, "ColumnName must be a string.");
		
		$result = null;
		
		if (array_key_exists($columnName, $this->_columns)) {
			$result = $this->_columns[$columnName];
		}
		
		return $result;
	}
	
	/**
	 * select
	 * Selects particular data for the model based on a series of conditions.
	 * @param array $conditions
	 * @return Model
	 */
	public function select(array $conditions) {
		$query = sprintf('select * from %1$s where ', $this->_sqlTableName);
		
		$counter = 0;
		foreach ($condition as $nextCondition) {
			if ($counter > 0) {
				$query .= "and ";
			}
			
			if ($nextCondition instanceof Condition) {
				$query = sprintf('%1$s %2$s', $query, $nextCondition);
			}
			
			$counter++;
		}
		
		$rows = self::$__queryHandler->executeQuery($query);
		
		if (count($rows) > 0) {
			$this->_columns = $rows[0];
		}
	}
	
}