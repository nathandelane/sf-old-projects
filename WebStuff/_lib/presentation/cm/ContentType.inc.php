<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/data/QueryHandler.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/data/QueryHandlerType.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/cm/ContentMap.inc.php");

/**
 * ContentType
 * This class represents a content type which is associated with a ContenManageable type.
 * @author lanathan
 *
 */
abstract class ContentType {

	private static $__queryHandler;
	
	private $_contentMap;
	
	/**
	 * Constructor
	 * @param ContentMap $contentMap
	 * @return ContentType
	 */
	public function ContentType(ContentMap $contentMap) {
		if (!isset(self::$__queryHandler)) {
			self::$__queryHandler = QueryHandler::getInstance(QueryHandlerType::MYSQL);
		}
		
		$this->_contentMap = $contentMap;
	}
	
	/**
	 * __get
	 * Gets an item from the content map.
	 * @param string $name
	 * @return string
	 */
	public function __get(/*string*/ $name) {
		$result = null;
		
		if ($this->_contentMap->containsKey($name)) {
			$result = $this->_contentMap[$name];
		}
		
		return $result;
	}
	
}

?>