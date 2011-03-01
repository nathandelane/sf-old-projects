<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandler.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/data/QueryHandlerType.inc.php");
require_once(PhyleBox_Config::getFrameworkFoundation() . "Logger.inc.php");
require_once(PhyleBox_Config::getLocalFoundationLocation() . "types/DriveType.inc.php");

/**
 * ImageLoaderControl
 * Represents image data from an image.
 * @author nalane
 *
 */
class ImageLoaderControl implements IRenderable {
	
	private static $__queryHandler;
	
	private $_logger;	
	private $_internalImage;
	private $_type;
	
	/**
	 * Constructor
	 * Creates an instance of ImageLoaderControl
	 * @param string $driveSelector
	 * @param string $currentDirectory
	 * @param string $fileName
	 * @param string $type
	 * @return ImageLoaderControl
	 */
	public function ImageLoaderControl(/*string*/ $driveSelector, /*string*/ $currentDirectory, /*string*/ $fileName, /*string*/ $type) {
		if (!isset(self::$__queryHandler)) {
			self::$__queryHandler = QueryHandler::getInstance(QueryHandlerType::MYSQL);
		}
		
		$this->_logger = Logger::getInstance();
		$this->_type = $type;
		
		$this->_loadImage($driveSelector, $currentDirectory, $fileName, $type);
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		if (Strings::equals($this->_type, "png", true)) {
			echo "data:image/png;base64,{$this->_internalImage}";
		} else if (Strings::equals($this->_type, "jpg", true) || Strings::equals($type, "jpeg", true)) {
			echo "data:image/jpg;base64,{$this->_internalImage}";
		} else if (Strings::equals($this->_type, "gif", true)) {
			echo "data:image/gif;base64,{$this->_internalImage}";
		} else if (Strings::equals($this->_type, "bmp", true)) {
			echo "data:image/bmp;base64,{$this->_internalImage}";
		}
	}

	
	/**
	 * _loadImage
	 * Loads an image.
	 * @param string $driveSelector
	 * @param string $currentDiirectory
	 * @param string $fileName
	 * @param string $type
	 * @return string
	 */
	private function _loadImage(/*string*/ $driveSelector, /*string*/ $currentDirectory, /*string*/ $fileName, /*string*/ $type) {
		$userName = $_SESSION["userName"];
		$relativePath = $currentDirectory;
		list($driveType, $driveId, $shortcutId) = Strings::split($driveSelector, "-");
		$driveType = intval($driveType);
		$driveId = intval($driveId);
		$driveQuery = "";
		$driveLocation = "/";
		$shortcutClause = $shortcutId == 0 ? "" : " and ps.personal_shortcut_id = {$shortcutId}";
		
		$this->_logger->sendMessage(LOG_DEBUG, "Drive Selector: {$driveSelector}, Current Directory: {$currentDirectory}, File Name: {$fileName}, Drive Type: {$driveType}");
		
		if (!is_null($userName)) {
			if ($driveType === DriveType::PERSONAL) {
				$driveQuery = "select Concat(pd.drive_location, ifnull(ps.directory, '')) as drive_location from `pbox`.`personal_drives` pd inner join `pbox`.`people` p on p.person_id = pd.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 1) ps on (ps.person_id = p.person_id and ps.drive_id = pd.personal_drive_id) where p.user_name = '{$userName}' and pd.personal_drive_id = {$driveId}{$shortcutClause}";
			} else if ($driveType === DriveType::STORAGE) {
				$driveQuery = "select Concat(ps.storage_location, ifnull(ps2.directory, '')) as drive_location from `pbox`.`personal_storage` ps inner join `pbox`.`people` p on p.person_id = ps.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 2) ps2 on (ps2.person_id = p.person_id and ps2.drive_id = ps.personal_storage_id) where p.user_name = '{$userName}' and ps.personal_storage_id = {$driveId}{$shortcutClause}";
			} else if ($driveType === DriveType::GROUP) {
				$driveQuery = "select Concat(gd.drive_location, ifnull(ps.directory, '')) as drive_location from `pbox`.`group_drives` gd left outer join `pbox`.`groups` g on g.group_id = gd.group_id left outer join `pbox`.`people_groups` pg on pg.group_id = g.group_id left outer join `pbox`.`people` p  on p.person_id = pg.person_id left outer join (select * from `pbox`.`personal_shortcuts` ps where drive_type = 3) ps on (ps.person_id = p.person_id and ps.drive_id = gd.group_drive_id) where p.user_name = '{$userName}' and gd.group_drive_id = {$driveId}{$shortcutClause}";
			}
									
			$this->_logger->sendMessage(LOG_DEBUG, "Drive Query: {$driveQuery}");
			
			if (!Strings::isNullOrEmpty($driveQuery)) {
				$rows = self::$__queryHandler->executeQuery($driveQuery);
				
				$this->_logger->sendMessage(LOG_DEBUG, "Number of rows: " . count($rows));
				
				if (count($rows) > 0) {
					$driveLocation = $rows[0]["drive_location"];
					
					$this->_logger->sendMessage(LOG_DEBUG, "DriveLocation: {$driveLocation}");
					
					$absoluteFilePath = $driveLocation . $currentDirectory . $fileName;
					
					$this->_logger->sendMessage(LOG_DEBUG, "Absolute file location: {$absoluteFilePath}.");
					
					if (file_exists($absoluteFilePath)) {
						$binaryImageData = fread(fopen($absoluteFilePath, "r"), filesize($absoluteFilePath));
						
						$this->_internalImage = base64_encode($binaryImageData);
					}
				}
			}
		} else {
			$this->_logger->sendMessage(LOG_DEBUG, "Could not access file. No authentication found.");
		}
	}
	
}

?>