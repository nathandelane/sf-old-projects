if (Phyer && $Phyer) {
	
	var FileManager = function() { }
	
	FileManager.prototype = {
		/**
		 * Static variables
		 */
		__rowCounter: 0,
		/**
		 * Constants
		 */
		DRIVE_NAME: "name",
		DRIVE_LOCATION: "location",
		DRIVE_TYPE: "type",
		DIRECTORY: "directory",
		FILE_LIST_TABLE: "fileListTable",
		
		/**
		 * getDiskUsage
		 * Gets the disk usage for the currently selected drive.
		 * @param string name
		 * @param string location
		 * @param int type
		 * @return void
		 */
		getDiskUsage: function(name, location, type) {
			var data = "{ \"" + $Phyer.FileManager.DRIVE_NAME + "\": \"" + name + "\", \"" + $Phyer.FileManager.DRIVE_LOCATION + "\": \"" + location + "\", \"" + $Phyer.FileManager.DRIVE_TYPE + "\": " + type + " }";
			
			$Phyer.postJson("http://www.localhost.phyer.net/phyle-box/business/FileManagementService.php?getDiskSpaceUsedForDrive", data, 
				null, 
				function(json) {
					$Phyer.setJson(json);
					
					$("#selectedDriveUsedSpace").attr("style", "width: " + json.percentage + "px;");
					$("#driveSpacePercentage").text((100 - json.percentage) + "% Free");
				}
			);
		},
		
		/**
		 * populateFileList
		 * Populates the file list for the current directory.
		 * @param string location
		 * @param string directory
		 * @return void
		 */
		populateFileList: function(location, directory) {
			var data = "{ \"" + $Phyer.FileManager.DRIVE_LOCATION + "\": \"" + location + "\", \"" + $Phyer.FileManager.DIRECTORY + "\": \"" + directory + "\" }";
			
			$Phyer.postJson("http://www.localhost.phyer.net/phyle-box/business/FileManagementService.php?listFoldersAndFilesForDriveAndDirectory", data, 
				null, 
				function(json) {
					$Phyer.setJson(json);
					$Phyer.FileManager.__rowCounter = 0;

					var directoryCount = json.directories;
					var fileCount = json.files;
					
					if (directoryCount > 0) {
						for (var directoryIndex = 1; directoryIndex <= directoryCount; directoryIndex++) {
							var nextDir = json.contents["dir" + directoryIndex];
							
							$Phyer.FileManager.createFileListRow(nextDir.type, nextDir.name, nextDir.modifiedTime, nextDir.size, nextDir.permissions);
							
							$Phyer.FileManager.__rowCounter++;
						}
					}
					
					if (fileCount > 0) {
						for (var fileIndex = 1; fileIndex <= fileCount; fileIndex++) {
							var nextDir = json.contents["file" + fileIndex];
							
							$Phyer.FileManager.createFileListRow(nextDir.type, nextDir.name, nextDir.modifiedTime, nextDir.size, nextDir.permissions);
							
							$Phyer.FileManager.__rowCounter++;
						}						
					}
					
					$("#" + $Phyer.FileManager.FILE_LIST_TABLE + " tbody tr").hover(
						function() {
							$(this).children().addClass("selected");
						},
						function() {
							$(this).children().removeClass("selected");
						}
					);
				}
			);			
		},
		
		/**
		 * createFileListRow
		 * Creates and places a file list row on the file list dynamically.
		 * @param int type
		 * @param string name
		 * @param string modifiedTime
		 * @param double size
		 * @param int permissions
		 * @return void
		 */
		createFileListRow: function(type, name, modifiedTime, size, permissions) {
			var fileListTable = $("#" + $Phyer.FileManager.FILE_LIST_TABLE + " > tbody");
			var row = "<td class=\"checkBox\"><input type=\"checkbox\" id=\"checkBox" + $Phyer.FileManager.__rowCounter + "\" name=\"checkBox" + $Phyer.FileManager.__rowCounter + "\" /></td><td class=\"icon\"><div class=\"type" + type + "\"></div></td><td class=\"fileOrDirName\">" + name + "</td><td class=\"modifiedTime\">" + modifiedTime + "</td><td class=\"size\">" + size + " Kb</td><td class=\"permissions\">" + permissions + "</td><td class=\"actions\"></td>";
			
			if (($Phyer.FileManager.__rowCounter % 2) == 0) {
				$("<tr>" + row + "</tr>").appendTo(fileListTable);
			} else {
				$("<tr class=\"oddRow\">" + row + "</tr>").appendTo(fileListTable);
			}
		}
			
	}
	
	Phyer.prototype.FileManager = new FileManager();
	
}

window.$Phyer = new Phyer();