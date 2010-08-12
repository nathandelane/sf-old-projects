<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/io/FileInfo.inc.php");

/**
 * FileModel
 * Model of a file.
 * @author lanathan
 *
 */
class FileModel {

	private $_fileInfo;
	
	/**
	 * Constructor
	 * @param string $directoryName
	 * @return FileModel
	 */
	public function FileModel(/*string*/ $fileName) {
		ArgumentTypeValidator::isString($fileName, "FileName must be a string.");
		
		$this->_fileInfo = new FileInfo($fileName);
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
		} else if (Strings::equals($propertyName, "extension")) {
			$result = $this->_fileInfo->extension;
		}
		
		return $result;
	}
		
}

?>