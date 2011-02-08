<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "types/DriveType.inc.php");

/**
 * DriveUsageModel
 * This class represents the drive usage for a particular drive. Percentage is what's available. Kilobytes is what's used.
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
	public function DriveUsageModel(/*string*/ $absoluteDirectoryPath, /*int*/ $allottedSpace) {
		ArgumentTypeValidator::isString($absoluteDirectoryPath, sprintf('AbsoluteDirectoryPath must be a string. (%s)', $absoluteDirectoryPath));
		ArgumentTypeValidator::isInteger($allottedSpace, "AllottedSpace must be an integer.");
		
		$this->_allottedSpace = $allottedSpace;	
		
		if (file_exists($absoluteDirectoryPath)) {
			$this->_kilobytes = $this->_calculateUsage($absoluteDirectoryPath) / 1024;
			
			if ($this->_allottedSpace > 0) {
				$decimalUsage = $this->_kilobytes / $this->_allottedSpace;
				
				$this->_percentage = round(($decimalUsage * 100), 0);
			} else {
				$this->_percentage = 0;
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
		} else if (Strings::equals($key, "allotted")) {
			$result = $this->_allottedSpace;
		}
		
		return $result;
	}
	
	/**
	 * _calculateUsage
	 * Calculates the usage in a directory and sets the internal usage variable.
	 * @param string $directory
	 * @throws Exception
	 * @return void
	 */
	private function _calculateUsage(/*string*/ $directory) {
		$directoryHandle = opendir($directory);
			
		if (is_resource($directoryHandle)) {
			while ($nextFile = readdir($directoryHandle)) {
				if ($nextFile != "." && $nextFile != "..") 	{
					if (is_dir($directory . "/" . $nextFile)) {
						$bytes += $this->_calculateUsage($directory . "/" . $nextFile);
					} else {
						$bytes += filesize($directory . "/" . $nextFile);
					}
				}
			}
			
			closedir($directoryHandle);
		} else {
			throw new Exception("Directory {$absolutDirectoryPath} does not exist or cannot be found.");
		}
		
		return $bytes;
	} 
	
}
