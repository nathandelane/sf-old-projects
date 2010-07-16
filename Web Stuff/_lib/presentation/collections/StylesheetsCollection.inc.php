<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/collections/HashCollection.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * StylesheetsCollection
 * This class represents a collection of stylesheets
 * @author lanathan
 *
 */
class StylesheetsCollection extends HashCollection implements IRenderable {
	
	/**
	 * StylesheetsCollection constructor
	 * @return StylesheetsCollection
	 */
	public function StylesheetsCollection() {
		parent::__construct();
	}
	
	/**
	 * addStylesheet
	 * Adds a stylesheet to the stylesheet collection.
	 * @param string $href
	 * @param string $media
	 */
	public function addStylesheet(/*string*/ $href, /*string*/ $media = 1) {
		$key = $href;
		$value = array("href" => $href, "media" => $media);
		
		$this->add($key, $value);
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		$enumerator = $this->getEnumerator();
		
		while ($enumerator->moveNext()) {
			$nextStylesheet = $enumerator->getNextValue();
			
?>
		<link rel="stylesheet" href="<?php echo "{$nextStylesheet["href"]}"; ?>" media="<?php echo "{$nextStylesheet["media"]}"; ?>" />
<?php
		
		}
	}
	
}

?>