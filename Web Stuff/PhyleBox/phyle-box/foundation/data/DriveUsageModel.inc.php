<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "types/DriveType.inc.php");

/**
 * DriveUsageModel
 * This class represents the drive usage for a particular drive.
 * @author lanathan
 *
 */
class DriveUsageModel {
	
	private $_allottedSpace;
	private $_kilobytes;
	private $_percentage;
	
	/**
	 * Constructor
	 * @param string $absoluteDirectoryPath
	 * @param int $allottedSpace
	 * @return DriveUsageModel
	 */
	public function DriveUsageModel(/*string*/ $absolutDirectoryPath, /*int*/ $allottedSpace) {
		ArgumentTypeValidator::isString($absolutDirectoryPath, "AbsoluteDirectoryPath must be a string.");
		ArgumentTypeValidator::isInteger($allottedSpace, "AllottedSpace must be an integer.");
		
		$this->_allottedSpace = $allottedSpace;		
		
		if (file_exists($absolutDirectoryPath)) {
			$directoryHandle = opendir($absolutDirectoryPath);
			
			if (is_resource($directoryHandle)) {
				$this->_calculateUsage($directoryHandle);
				
				closedir($directoryHandle);
			} else {
				throw new Exception("Directory {$absolutDirectoryPath} does not exist or cannot be found.");
			}
		}
	}
	
	/**
	 * __get
	 * Gets the value of usage for the DriveUsageModel in kilobytes.
	 * @return double
	 */
	public function __get(/*string*/ $key) {
		$result = null;
		
		if (Strings::equals($key, "kilobytes")) {
			$result = $this->_kilobytes;
		} else 	if (Strings::equals($key, "percentage")) {
			$result = $this->_percentage;
		}
		
		return $result;
	}
	
	/**
	 * _calculateUsage
	 * Calculates the usage in a directory and sets the internal usage variable.
	 * @param resource $directoryHandle
	 * @return void
	 */
	private function _calculateUsage(/*resource*/ $directoryHandle) {
		$this->_kilobytes = 0;
		
		while ($nextFile = readdir($directoryHandle)) {
			if ($nextFile != "." && $nextFile != "..") 	{
				if (is_dir($directoryHandle . "/" . $nextFile)) {
					$this->_kilobytes += directory_size($directoryHandle . "/" . $nextFile);
				} else {
					$this->_kilobytes += filesize($directoryHandle . "/" . $nextFile);
				}
			}
		}
		
		$this->_kilobytes /= 1024;
		
		if ($this->_allottedSpace > 0) {
			$this->_percentage = ($this->_kilobytes / $this->_allottedSpace);
		} else {
			$this->_percentage = 100;
		}
	} 
	
}