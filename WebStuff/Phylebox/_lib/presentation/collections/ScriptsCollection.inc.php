<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/collections/ArrayList.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * ScriptsCollection
 * This class represents a collection of scripts
 * @author lanathan
 *
 */
class ScriptsCollection extends ArrayList implements IRenderable {
	
	/**
	 * ScriptsCollection constructor
	 * @return ScriptsCollection
	 */
	public function ScriptsCollection() {
		parent::__construct();
	}
	
	/**
	 * addScript
	 * Adds a script to the script collection.
	 * @param string $src
	 */
	public function addScript(/*string*/ $src) {
		$this->add($src);
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		$this->_logger->sendMessage(LOG_DEBUG, "Rendering ScriptsCollection.");
		
		$enumerator = $this->getEnumerator();
		
		while ($enumerator->moveNext()) {
			$nextScript = $enumerator->getNextItem();
			
			$this->_logger->sendMessage(LOG_DEBUG, "NextScript: $nextScript");
			
?>
		<script type="text/javascript" src="<?php echo "{$nextScript}"; ?>"></script>
<?php
		
		}
	}
	
}

?>