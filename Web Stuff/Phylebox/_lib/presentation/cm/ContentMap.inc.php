<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/collections/HashCollection.inc.php");

/**
 * ContentMap
 * Map for content-manageable content.
 * @author lanathan
 *
 */
class ContentMap extends HashCollection {
	
	/**
	 * Constructor
	 * @return ContentMap
	 */
	public function ContentMap() {
		parent::__construct();
	}
	
}

?>