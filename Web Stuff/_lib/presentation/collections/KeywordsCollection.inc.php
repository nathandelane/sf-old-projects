<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/collections/ArrayList.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * KeywordsCollection
 * This class represents a collection of keywords
 * @author lanathan
 *
 */
class KeywordsCollection extends ArrayList implements IRenderable {
	
	/**
	 * KeywordsCollection constructor
	 * @return KeywordsCollection
	 */
	public function KeywordsCollection() {
		parent::__construct();
	}
	
	/**
	 * addKeyword
	 * Adds a keyword to the keyword collection.
	 * @param string $src
	 */
	public function addKeyword(/*string*/ $word) {
		$this->add($src);
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		$array = $this->toArray();
		
		if (count($array) > 0) {
			$stringifiedList = implode(", ", $array);
		
?>
		<meta name="keywords" content="<?php echo "$stringifiedList"; ?>" />
<?php

		}
	}
	
}

?>