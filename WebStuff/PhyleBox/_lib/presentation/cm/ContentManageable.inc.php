<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/cm/ContentType.inc.php");

/**
 * ContentManageable
 * This class represents the base class interface for all content manageable renderable objects. Also it renders the content controls.
 * @author lanathan
 *
 */
abstract class ContentManageable implements  IRenderable {
	
	protected $_name;
	protected $_type;
	
	/**
	 * Constructor
	 * @param string $name
	 * @param ContentType $type
	 * @return ContentManageable
	 */
	protected function ContentManageable(/*string*/ $name, ContentType $type) {
		$this->_name = $name;
		$this->_type = $type;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
	}
	
}

?>