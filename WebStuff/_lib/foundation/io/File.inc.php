<?php

require_once(dirname(__FILE__) . "/../Config.inc.php");

class File {
	
	private $_name;
	
	/**
	 * Constructor
	 * Creates an instance of File.
	 * @param string $path
	 * @return File
	 */
	public function File(/*string*/ $path) {
		$this->_name = $path;
	}
	
	/**
	 * create
	 * Creates a file from the given path.
	 * @param string $absolutePath
	 * @param bool $truncate
	 */
	public static function create(/*string*/ $absolutePath, /*bool*/ $truncate = true) {
		$fileHandle = null;
		
		if ($truncate) {
			$fileHandle = fopen($absolutePath, "w");
		} else {
			$fileHandle = fopen($absolutePath, "w+");
		}
		
		if (is_resource($fileHandle)) {
			$fclose($fileHandle);			
		}
	}
	
}

?>