<?php

require_once(dirname(__FILE__) . "/../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/Logger.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/LogStatus.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/IPage.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/collections/StylesheetsCollection.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/collections/ScriptsCollection.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/collections/KeywordsCollection.inc.php");

/**
 * presentation/Page
 * This class represents an XHTML page.
 * @author lanathan
 *
 */
abstract class Page implements IPage {
	
	protected $_logger;
	
	private $_title;
	private $_headerComponent;
	private $_stylesheets;
	private $_scripts;
	private $_keywords;
	private $_description;
	
	/**
	 * Page constructor
	 * @param string $title
	 * @param IRenderable $headerComponent
	 * @return Page
	 */
	public function Page(/*string*/ $title, IRenderable $headerComponent) {
		ArgumentTypeValidator::isString($title, "Title must be a string.");
		
		$this->_logger = Logger::getInstance();
		$this->_title = $title;
		$this->_headerComponent = $headerComponent;
		$this->_stylesheets = new StylesheetsCollection();
		$this->_scripts = new ScriptsCollection();
		$this->_keywords = new KeywordsCollection();
		$this->_description = null;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IPage::openDocument()
	 */
	public function openDocument() {
		
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en" dir="ltr">
<?php

		$this->_renderXhtmlHead();

?>
	<body>
<?php

	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IPage::closeDocument()
	 */
	public function closeDocument() {
		
?>
	</body>
</html>
<?php
		
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IPage::registerStylesheet()
	 */
	public function registerStylesheet(/*string*/ $stylesheetHref, /*int*/ $stylesheetMedia = 1) {
		ArgumentTypeValidator::isString($stylesheetHref, "StylesheetHref must be a string.");
		ArgumentTypeValidator::isInteger($stylesheetMedia, "StylesheetMedia must be an integer. See presentation/StylesheetMedia.");
		
		try {
			$this->_stylesheets->addStylesheet($stylesheetHref, $stylesheetMedia);
		} catch(Exception $e) {
			$this->_logger->logException($e);
		}
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IPage::registerScript()
	 */
	public function registerScript(/*string*/ $scriptSrc) {
		ArgumentTypeValidator::isString($scriptSrc, "ScriptSrc must be a string.");
		
		try {
			$this->_scripts->addScript($scriptSrc);
		} catch(Exception $e) {
			$this->_logger->logException($e);
		}
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IPage::registerKeyword()
	 */
	public function registerKeyword($keyword) {
		ArgumentTypeValidator::isString($keyword, "Keyword must be a string");
		
		try {
			$this->_keywords->addKeyword($keyword);
		} catch(Exception $e) {
			$this->_logger->logException($e);
		}
	}
	
	/**
	 * registerKeywords
	 * Extension method for register multiple keywords at a time.
	 * @param array $keywords
	 */
	public function registerKeywords(array $keywords) {
		foreach ($keywords as $nextKeyword) {
			$this->registerKeyword($nextKeyword);
		}
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IPage::setDescription()
	 */
	public function setDescription($description) {
		ArgumentTypeValidator::isString($description, "Description must be a string");
		
		$this->_description = $description;
	}
	
	/**
	 * renderXhtmlHead
	 * Renders the XHTML head tag and contents.
	 * @return void
	 */
	private function _renderXhtmlHead() {
		
?>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<title><?php echo "$this->_title"; ?></title>
<?php

		$this->_stylesheets->render();
		$this->_scripts->render();
		$this->_keywords->render();

		if (isset($this->_description)) {
?>
		<meta name="description" content="<?php echo "$this->_description"; ?>" />
<?php

		}

?>
	</head>
<?php
		
	}
	
	
	
}

?>