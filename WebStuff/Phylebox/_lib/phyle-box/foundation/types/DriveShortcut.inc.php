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
	
	/**
	 * Drive ID in the database.
	 * @var int
	 */
	public $id;
	/**
	 * Name of the drive shortcut.
	 * @var string
	 */
	public $name;
	/**
	 * Location of the drive.
	 * @var string
	 */
	public $location;
	/**
	 * Total allotted disk space.
	 * @var number
	 */
	public $totalDiskspace;
	/**
	 * Drive type ID.
	 * @var int
	 */
	public $type;
	
	/**
	 * Constructor
	 * @param int $id
	 * @param string $name
	 * @param string $location
	 * @param number $totalDiskspace
	 * @param int $type
	 * @return DriveShortcut
	 */
	public function DriveShortcut(/*string*/ $id, /*string*/ $name, /*string*/ $location, /*number*/ $totalDiskspace, /*int*/ $type) {
		ArgumentTypeValidator::isInteger($id, "Id must be an integer.");
		ArgumentTypeValidator::isString($name, "Name must be a string.");
		ArgumentTypeValidator::isString($location, "Location must be a string.");
		ArgumentTypeValidator::isNumeric($totalDiskspace, "TotalDiskspace must be a Number.");
		ArgumentTypeValidator::isInteger($type, "Type must be an integer.");
		
		$this->id = $id;
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
		return sprintf('Id: %1$s, Name: %2$s, Location: %3$s, TotalDiskspace: %4$s, Type: %5$s', $this->id, $this->name, $this->location, $this->totalDiskspace, $this->type);
	}
	
}

?>