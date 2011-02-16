<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/data/QueryHandlerType.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/data/QueryHandler.inc.php");

/**
 * Content
 * This class renders a CMS section.
 * @author lanathan
 *
 */
class Content implements IRenderable {
	
	private static $__queryHandler;
	
	private $_content;
	
	/**
	 * Constructor
	 * @return Content
	 */
	public function Content(/*string*/ $contentLabel) {	
		if (!isset(self::$__queryHandler)) {
			self::$__queryHandler = QueryHandler::getInstance(QueryHandlerType::MYSQL);
		}
		
		$query = "select c.content from `pbox`.`content` c where c.content_label = '{$contentLabel}'";
		$rows = self::$__queryHandler->executeQuery($query);
		
		$this->_content = $rows[0]["content"];
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		echo "{$this->_content}";
	}
	
}

?>