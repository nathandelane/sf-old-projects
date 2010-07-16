<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/Logger.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/collections/HashCollection.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/StylesheetMedia.inc.php");

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
		
		$this->_logger = Logger::getInstance();
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
			
			$this->_logger->sendMessage(LOG_DEBUG, "NextStylesheet: $nextStylesheet");
			
			if (is_array($nextStylesheet)) {
				$href = $nextStylesheet["href"];
				$media = $this->_convertStylesheetMediaToString(intval($nextStylesheet["media"]));
			
?>
		<link rel="stylesheet" href="<?php echo "{$href}"; ?>" media="<?php echo "{$media}"; ?>" />
<?php
		
			}
		}
	}
	
	/**
	 * _convertStylesheetMediaToString
	 * Converts the int StylesheetMedia value to a string value.
	 * @param unknown_type $stylesheetMedia
	 */
	private function _convertStylesheetMediaToString(/*int*/ $stylesheetMedia) {
		$result = null;
		
		ArgumentTypeValidator::isInteger($stylesheetMedia, "StylesheetMedia must be an integer.");
		
		if ($stylesheetMedia == StylesheetMedia::MEDIA_ALL) {
			$result = "all";
		} else if ($stylesheetMedia == StylesheetMedia::MEDIA_BRAILLE) {
			$result = "braille";
		} else if ($stylesheetMedia == StylesheetMedia::MEDIA_EMBOSSED) {
			$result = "embossed";
		} else if ($stylesheetMedia == StylesheetMedia::MEDIA_HANDHELD) {
			$result = "handheld";
		} else if ($stylesheetMedia == StylesheetMedia::MEDIA_PRINT) {
			$result = "print";		
		} else if ($stylesheetMedia == StylesheetMedia::MEDIA_PROJECTION) {
			$result = "projection";		
		} else if ($stylesheetMedia == StylesheetMedia::MEDIA_SCREEN) {
			$result = "screen";		
		} else if ($stylesheetMedia == StylesheetMedia::MEDIA_SPEECH) {
			$result = "speech";		
		} else if ($stylesheetMedia == StylesheetMedia::MEDIA_TTY) {
			$result = "tty";		
		} else if ($stylesheetMedia == StylesheetMedia::MEDIA_TV) {
			$result = "tv";		
		}
		
		return $result;
	}
	
}

?>