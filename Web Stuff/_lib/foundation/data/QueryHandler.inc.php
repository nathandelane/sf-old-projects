<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/Logger.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/data/QueryHandlerType.inc.php");

/**
 * QueryHandler
 * Creates a query handler for accessing the database.
 * @author lanathan
 *
 */
final class QueryHandler {
	
	private static $__instances;
	
	private $_type;
	private $_connection;
	private $_logger;
	private $_rows;
	
	/**
	 * Constructor
	 * Enter description here ...
	 * @param int $type
	 * @return QueryHandler
	 */
	private function QueryHandler(/*int*/ $type = QueryHandlerType::MYSQL) {
		$this->_type = $type;
		$this->_logger = Logger::getInstance();
	}
	
	/**
	 * executeQuery
	 * Executes a query.
	 * @param string $query
	 * @return array
	 */
	public function executeQuery(/*string*/ $query) {
		return $this->_executeQuery($query);
	}
	
	/**
	 * _createConnection
	 * Creates a connection based on the type.
	 * @return bool
	 */
	private function _createConnection() {
		$connected = false;
		$credentials = Config::getDatabaseCredentials();
		
		if (!isset($this->_connection)) {
			if ($this->_type == QueryHandlerType::MYSQL) {
				$this->_connection = mysql_connect($credentials["server"], $credentials["userName"], $credentials["password"]);
				
				if ($this->_connection && is_resource($this->_connection)) {
					$connected = true;
				}
			}
		} else {
			$connected = true;
		}
		
		return $connected;
	}
	
	/**
	 * _closeConnection
	 * Closes the connection.
	 * @return void
	 */
	private function _closeConnection() {
		if (isset($this->_connection) && is_resource($this->_connection)) {
			if ($this->_type == QueryHandlerType::MYSQL) {
				mysql_close($this->_connection);
			}
		}
	}
	
	/**
	 * _executeQuery
	 * Executes a query against a particular database.
	 * @param string $query
	 * @return array
	 */
	private function _executeQuery(/*string*/ $query) {
		if ($this->_type == QueryHandlerType::MYSQL) {
			$this->_rows = $this->_executeMySqlQuery($query);
		}
		
		return $this->_rows;
	}
	
	/**
	 * _executeMySqlQuery
	 * Queries a MySQL database
	 * @param string $query
	 * @return array
	 */
	private function _executeMySqlQuery(/*string*/ $query) {
		$results = array();
		$resultSet = mysql_query($query, $this->_connection);
		
		if (is_resource($resultSet)) {
			if (mysql_num_rows($resultSet) > 0) {
				while ($nextRow = mysql_fetch_assoc($resultSet)) {
					$results[] = $nextRow;
				}
			}
		}
		
		return $results;
	}
	
	/**
	 * getInstance
	 * Gets an instance of the query handler of a particular type.
	 * @param int $type
	 * @return QueryHandler
	 */
	public static function getInstance(/*int*/ $type) {
		$returnType = null;
		$result = null;
		
		if (!isset(self::$__instances)) {
			self::$__instances = array();
		}
		
		if ($type == QueryHandlerType::MYSQL) {
			$returnType = "MYSQL";
			
			if (!array_key_exists($returnType, self::$__instances)) {
				self::$__instances[$returnType] = new QueryHandler($type);
			}
			
			$result = self::$__instances[$returnType];
		}

		return $result;
	}
	
}

?>