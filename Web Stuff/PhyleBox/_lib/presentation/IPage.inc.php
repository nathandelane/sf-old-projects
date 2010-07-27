<?php

require_once(dirname(__FILE__) . "/../Config.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/StylesheetMedia.inc.php");

/**
 * This class represents a page. All pages must implement this interface. Follow the page pattern!
 * @author lanathan
 *
 */
interface IPage {
	
	/**
	 * openDocument
	 * Opens an XHTML document.
	 * @return void
	 */
	public function openDocument();
	
	/**
	 * closeDocument
	 * Closes an XHTML document.
	 * @return void
	 */
	public function closeDocument();
	
	/**
	 * registerStylesheet
	 * Registers a stylesheet with the page so that it can be rendered in the XHTML head tags.
	 * @param string $stylesheetHref
	 * @param int $stylesheetMedia Use _lib/presentation/StylesheetMedia to determine the type. StylesheetMedia::MEDIA_ALL is the default.
	 */
	public function registerStylesheet(/*string*/ $stylesheetHref, /*int*/ $stylesheetMedia = 1);
	
	/**
	 * registerScript
	 * Registers a script with the page so that it can be rendered in the XHTML head tags.
	 * @param string $scriptSrc
	 */
	public function registerScript(/*string*/ $scriptSrc);
	
	/**
	 * registerKeyword
	 * Registers a keyword with the page so that it can be rendered in the keywords meta tag.
	 * @param string $keyword
	 */
	public function registerKeyword(/*string*/ $keyword);
	
	/**
	 * setDescription
	 * Sets the page's description.
	 * @param string $description
	 */
	public function setDescription(/*string*/ $description);
	
	/**
	 * getFieldValue
	 * Returns the value of a GET or POST parameter.
	 * @param string $fieldName
	 */
	public function getFieldValue(/*string*/ $fieldName);
	
	/**
	 * getSessionFieldValue
	 * Returns the value of a SESSION variable.
	 * @param string $fieldName
	 */
	public function getSessionFieldValue(/*string*/ $fieldName);
	
}

?>