<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/Logger.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/Strings.inc.php");

/**
 * FileInfo
 * Gets information about a file or directory.
 * @author lanathan
 *
 */
class FileInfo {
	
	private $_absolutePath;
	private $_permissions;
	private $_directoryName;
	private $_baseName;
	private $_extension;
	private $_name;
	private $_size;
	private $_isFile;
	private $_isDirectory;
	private $_modifiedTime;
	private $_logger;
	
	/**
	 * Constructor
	 * @param string $fileOrDirectoryPath
	 * @return FileInfo
	 */
	public function FileInfo(/*string*/ $fileOrDirectoryPath) {
		ArgumentTypeValidator::isString($fileOrDirectoryPath, "FileOrDirectoryPath must be a string.");
		
		$this->_logger = Logger::getInstance();
		$this->_logger->sendMessage(LOG_DEBUG, "File or directory path: {$fileOrDirectoryPath}");
		
		if (file_exists($fileOrDirectoryPath)) {
			$this->_absolutePath = $fileOrDirectoryPath;
			
			$this->_getPermissions($fileOrDirectoryPath);
			$this->_getPathInfo($fileOrDirectoryPath);
			$this->_getSize($fileOrDirectoryPath);
			$this->_getModifiedTime($fileOrDirectoryPath);
		} else {
			throw new Exception("File or directory {$fileOrDirectoryPath} does not exist or cannot be found.");
		}
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
		
		if (Strings::equals($propertyName, "premissions")) {
			$result = $this->_permissions;
		} else if (Strings::equals($propertyName, "directoryName")) {
			$result = $this->_directoryName;
		} else if (Strings::equals($propertyName, "baseName")) {
			$result = $this->_baseName;
		} else if (Strings::equals($propertyName, "extension")) {
			$result = $this->_extension;
		} else if (Strings::equals($propertyName, "name")) {
			$result = $this->_name;
		} else if (Strings::equals($propertyName, "size")) {
			$result = $this->_size;
		} else if (Strings::equals($propertyName, "isDirectory")) {
			$result = $this->_isDirectory;
		} else if (Strings::equals($propertyName, "isFile")) {
			$result = $this->_isFile;
		} else if (Strings::equals($propertyName, "isReadonly")) {
			$result = !(is_writable($this->_absolutePath));
		} else if (Strings::equals($propertyName, "absolutePath")) {
			$result = $this->_absolutePath;
		} else if (Strings::equals($propertyName, "modifiedTime")) {
			$result = $this->_modifiedTime;
		} else if (Strings::equals($propertyName, "permissions")) {
			$result = $this->_permissions;
		}
		
		return $result;
	}
	
	/**
	 * _getPermissions
	 * Gets the permissions of the file or directory.
	 * @param string $fileOrDirectoryPath
	 * @return void
	 */
	private function _getPermissions(/*string*/ $fileOrDirectoryPath) {
		ArgumentTypeValidator::isString($fileOrDirectoryPath, "FileOrDirectoryPath must be a string.");
		
		$perms = fileperms($fileOrDirectoryPath);
		$info = "";
		
		if (($perms & 0xC000) == 0xC000) {
			// Socket
			$info = 's';
		} elseif (($perms & 0xA000) == 0xA000) {
			// Symbolic Link
			$info = 'l';
		} elseif (($perms & 0x8000) == 0x8000) {
			// Regular
			$info = '-';
		} elseif (($perms & 0x6000) == 0x6000) {
			// Block special
			$info = 'b';
		} elseif (($perms & 0x4000) == 0x4000) {
			// Directory
			$info = 'd';
		} elseif (($perms & 0x2000) == 0x2000) {
			// Character special
			$info = 'c';
		} elseif (($perms & 0x1000) == 0x1000) {
			// FIFO pipe
			$info = 'p';
		} else {
			// Unknown
			$info = 'u';
		}
		
		// Owner
		$info .= (($perms & 0x0100) ? 'r' : '-');
		$info .= (($perms & 0x0080) ? 'w' : '-');
		$info .= (($perms & 0x0040) ? (($perms & 0x0800) ? 's' : 'x' ) : (($perms & 0x0800) ? 'S' : '-'));
		
		// Group
		$info .= (($perms & 0x0020) ? 'r' : '-');
		$info .= (($perms & 0x0010) ? 'w' : '-');
		$info .= (($perms & 0x0008) ? (($perms & 0x0400) ? 's' : 'x' ) : (($perms & 0x0400) ? 'S' : '-'));
		
		// World
		$info .= (($perms & 0x0004) ? 'r' : '-');
		$info .= (($perms & 0x0002) ? 'w' : '-');
		$info .= (($perms & 0x0001) ? (($perms & 0x0200) ? 't' : 'x' ) : (($perms & 0x0200) ? 'T' : '-'));
		
		$this->_permissions = $info;
	}
	
	/**
	 * _getPathInfo
	 * Gets information about the file or directory path.
	 * @param string $fileOrDirectoryPath
	 * @return void
	 */
	private function _getPathInfo(/*string*/ $fileOrDirectoryPath) {
		ArgumentTypeValidator::isString($fileOrDirectoryPath, "FileOrDirectoryPath must be a string.");
		
		$pathInfo = pathinfo($fileOrDirectoryPath);
		
		$this->_directoryName = $pathInfo["dirname"];
		
		if (is_dir($fileOrDirectoryPath)) {
			$this->_name = dirname($fileOrDirectoryPath);
		} else {
			$this->_baseName = $pathInfo["basename"];
			$this->_extension = $pathInfo["extension"];
			$this->_name = $pathInfo["filename"];
		}
	}
	
	/**
	 * _getSize
	 * Gets the size of the file or directory.
	 * @param string $fileOrDirectoryPath
	 * @return void
	 */
	private function _getSize(/*string*/ $fileOrDirectoryPath) {
		ArgumentTypeValidator::isString($fileOrDirectoryPath, "FileOrDirectoryPath must be a string.");
		
		if (is_dir($fileOrDirectoryPath)) {
			$this->_size = round(($this->_calculateUsage($fileOrDirectoryPath) / 1024), 2);
			$this->_isDirectory = true;
			$this->_isFile = false;
		} else {
			$this->_size = round((filesize($fileOrDirectoryPath) / 1024), 2);
			$this->_isDirectory = false;
			$this->_isFile = true;
		}
	}
	
	/**
	 * _getModifiedTime
	 * Gets the file or directory's last modified time.
	 * @param string $fileOrDirectoryPath
	 * @return void
	 */
	private function _getModifiedTime(/*string*/ $fileOrDirectoryPath) {
		$this->_modifiedTime = filemtime($fileOrDirectoryPath);
	}
	
	/**
	 * _calculateUsage
	 * Calculates the usage of a directory.
	 * @param string $directory
	 * @throws Exception
	 * @return int
	 */
	private function _calculateUsage(/*string*/ $directory) {
		ArgumentTypeValidator::isString($directory, "Directory must be a string.");
		
		$bytes = 0;
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

?>