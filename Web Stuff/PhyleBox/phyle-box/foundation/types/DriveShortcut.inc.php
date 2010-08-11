<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");

/**
 * DriveShortcut
 * This class represents the data required to render a shortcut for a drive location.
 * @author lanathan
 *
 */
final class DriveShortcut {
	
	public $name;
	public $location;
	public $totalDiskspace;
	public $type;
	
	/**
	 * Constructor
	 * @param string $name
	 * @param string $location
	 * @param number $totalDiskspace
	 * @param int $type
	 * @return DriveShortcut
	 */
	public function DriveShortcut(/*string*/ $name, /*string*/ $location, /*number*/ $totalDiskspace, /*int*/ $type) {
		ArgumentTypeValidator::isString($name, "Name must be a string.");
		ArgumentTypeValidator::isString($location, "Location must be a string.");
		ArgumentTypeValidator::isNumeric($totalDiskspace, "TotalDiskspace must be a Number.");
		ArgumentTypeValidator::isInteger($type, "Type must be an integer.");
		
		$this->name = $name;
		$this->location = $location;
		$this->totalDiskspace = $totalDiskspace;
		$this->type = $type;
	}
	
	/**
	 * __toString
	 * Returns a string representation of DriveShortcut.
	 * @return string
	 */
	public function __toString() {
		return sprintf('Name: %1$s, Location: %2$s, TotalDiskspace: %3$s, Type: %4$s', $this->name, $this->location, $this->totalDiskspace, $this->type);
	}
	
}

?>