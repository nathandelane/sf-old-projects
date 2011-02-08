<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");

/**
 * Condition
 * This class represents a condition for an SQL query.
 * @author lanathan
 *
 */
class Condition {
	
	const LIKE = 1;
	const NOT_LIKE = 2;
	const EQUALS = 3;
	const LESS_THAN = 4;
	const LESS_THAN_OR_EQUAL = 5;
	const GREATER_THAN = 6;
	const GREATER_THAN_OR_EQUAL = 7;
	const NOT_EQUALS = 8;
	const IS_NULL = 9;
	const NOT_NULL = 10;
	
	private $_columnName;
	private $_operator;
	private $_value;
	
	/**
	 * Constructor
	 * @param string $columnName
	 * @param int $operator
	 * @param mixed $value
	 * @return Condition
	 */
	public function Condition(/*string*/ $columnName, /*int*/ $operator, /*mixed*/ $value) {
		$this->_columnName = $columnName;
		$this->_operator = $operator;
		$this->_value = $value;
	}
	
	/**
	 * __toString
	 * Returns a string representation of this Condition.
	 * @return string
	 */
	public function __toString() {
		$result = sprintf('$columnName %1$s %2$s', $this->_getOperator(), $this->_getValue());
		
		return $result;
	}
	
	/**
	 * _getOperator
	 * Returns a string representation of the operator.
	 * @return string
	 */
	private function _getOperator() {
		$result = "=";
		
		if ($this->_operator == Condition::LIKE) {
			$result = "like";
		} else if ($this->_operator == Condition::NOT_LIKE) {
			$result = "not like";
		} else if ($this->_operator == Condition::LESS_THAN) {
			$result = "<";
		} else if ($this->_operator == Condition::LESS_THAN_OR_EQUAL) {
			$result = "<=";
		} else if ($this->_operator == Condition::GREATER_THAN) {
			$result = ">";
		} else if ($this->_operator == Condition::GREATER_THAN_OR_EQUAL) {
			$result = ">=";
		} else if ($this->_operator == Condition::NOT_EQUALS) {
			$result = "<>";
		} else if ($this->_operator == Condition::IS_NULL) {
			$result = "is null";
		} else if ($this->_operator == Condition::NOT_NULL) {
			$result = "is not null";
		}
		
		return $result;
	}
	
	/**
	 * _getValue
	 * Returns a string representation of the value.
	 * @return string
	 */
	private function _getValue() {
		$result = "";
		
		if (is_numeric($this->_value)) {
			$result = "$this->_value";
		} else if (is_string($this->_value)) {
			$result = "'$this->_value'";
		} else if (is_array($this->_value)) {
			$result = "'" . implode(",", $this->_value) . "'";
		}
		
		return $result;
	}
	
}

?>