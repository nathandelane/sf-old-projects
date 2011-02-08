<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/io/FileInfo.inc.php");

/**
 * DirectoryModel
 * Model of a directory.
 * @author lanathan
 *
 */
class DirectoryModel {

	private $_fileInfo;
	
	/**
	 * Constructor
	 * @param string $directoryName
	 * @return DirectoryModel
	 */
	public function DirectoryModel(/*string*/ $directoryName) {
		ArgumentTypeValidator::isString($directoryName, "DirectoryName must be a string.");
		
		$this->_fileInfo = new FileInfo($directoryName);
	}
	
	/**
	 * __get
	 * Gets the value of a property.
	 * @param string $propertyName
	 * @return mixed
	 */
	public function __get(/*string*/ $propertyName) {
		ArgumentTypeValidator::isString($propertyName, "PropertyName must be a string.");
		
		$result = null;
		
		if (Strings::equals($propertyName, "name")) {
			$result = $this->_fileInfo->name;
		} else if (Strings::equals($propertyName, "permissions")) {
			$result = $this->_fileInfo->permissions;
		} else if (Strings::equals($propertyName, "size")) {
			$result = $this->_fileInfo->size;
		} else if (Strings::equals($propertyName, "modifiedTime")) {
			$result = $this->_fileInfo->modifiedTime;
		}
		
		return $result;
	}
		
}

?>