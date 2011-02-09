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
		FILE_NAME: "fileName",
		FILE_CONTENTS: "fileContents",
		FOLDER_NAME: "folderName",
		ORDER_BY_COLUMN_NAME: "orderByColumnName",
		ORDER_BY_ORDER: "orderByOrder",
		
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
			
			$Phyer.postJson($Phyer.PHYER_ROOT + "phyle-box/business/FileManagementService.php?getDiskSpaceUsedForDrive", data, 
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
		 * @param string orderByColumnName
		 * @param string orderByOrder
		 * @return void
		 */
		populateFileList: function(location, directory, orderByColumnName, orderByOrder) {
			if (!orderByColumnName) { orderByColumnName = "name"; }
			if (!orderByOrder) { orderByOrder = "asc"; }
			
			var data = "{ \"" + $Phyer.FileManager.DRIVE_LOCATION + "\": \"" + location + "\", \"" + $Phyer.FileManager.DIRECTORY + "\": \"" + directory + "\", \"" + $Phyer.FileManager.ORDER_BY_COLUMN_NAME + "\": \"" + orderByColumnName + "\", \"" + $Phyer.FileManager.ORDER_BY_ORDER + "\": \"" + orderByOrder + "\" }";
			
			$Phyer.postJson($Phyer.PHYER_ROOT + "phyle-box/business/FileManagementService.php?listFoldersAndFilesForDriveAndDirectory", data, 
				null, 
				function(json) {
					$Phyer.setJson(json);
					$Phyer.FileManager.__rowCounter = 0;
					
					$("#" + $Phyer.FileManager.FILE_LIST_TABLE + " > tbody").empty();

					var fileListTableCaption = $("#" + $Phyer.FileManager.FILE_LIST_TABLE + " > caption");
					
					$(fileListTableCaption).text("Files and Folders located in: " + json.path);
					
					var directoryCount = json.directoryCount;
					var fileCount = json.fileCount;
					
					if (directoryCount > 0) {
						for (var directoryIndex = 0; directoryIndex <= directoryCount; directoryIndex++) {
							var nextDir = json.directories[directoryIndex];
							
							if (nextDir) {
								$Phyer.FileManager.createFileListDirectoryRow(location, directory, nextDir.name, nextDir.modifiedTime, nextDir.size, nextDir.permissions);
								
								$Phyer.FileManager.__rowCounter++;
							}
						}
					}
					
					if (fileCount > 0) {
						for (var fileIndex = 1; fileIndex <= fileCount; fileIndex++) {
							var nextFile = json.files[fileIndex];
							
							if (nextFile) {
								$Phyer.FileManager.createFileListRow(nextFile.type, nextFile.name, nextFile.modifiedTime, nextFile.size, nextFile.permissions);
								
								$Phyer.FileManager.__rowCounter++;
							}
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
					
					$("input[type='checkbox']").change(function(e) {
						if ($(this).attr("checked")) {
							$("#downloadFiles").attr("class", "phyleBoxIcons download active");
							$("#downloadFiles").attr("title", "Download Files");
						} else {
							$("#downloadFiles").attr("class", "phyleBoxIcons download");
							$("#downloadFiles").attr("title", "Select Files to Download");
						}
					});
				}
			);			
		},
		
		/**
		 * createFile
		 * Creates a file in the current directory if it doesn't already exist.
		 * @param string location
		 * @param string directory
		 * @param string fileName
		 * @param string contents
		 * @return bool
		 */
		createFile: function(location, directory, fileName, contents) {
			var data = "{ \"" + $Phyer.FileManager.DRIVE_LOCATION + "\": \"" + location + "\", \"" + $Phyer.FileManager.DIRECTORY + "\": \"" + directory + "\", \"" + $Phyer.FileManager.FILE_NAME + "\": \"" + fileName + "\", \"" + $Phyer.FileManager.FILE_CONTENTS + "\": \"" + contents + "\" }";
			
			$Phyer.postJson($Phyer.PHYER_ROOT + "phyle-box/business/FileManagementService.php?createNewFile", data, 
					null, 
					function(json) {
						$Phyer.setJson(json);
					}
				);
		},
		
		/**
		 * createFolder
		 * Creates a folder in the current directory if it doesn't already exist.
		 * @return bool
		 */
		createFolder: function() {
			var location = $("#driveSelector").val();
			var directory = $("#currentDirectory").val();
			var folderName = $("#newFolderName").val();
			var data = "{ \"" + $Phyer.FileManager.DRIVE_LOCATION + "\": \"" + location + "\", \"" + $Phyer.FileManager.DIRECTORY + "\": \"" + directory + "\", \"" + $Phyer.FileManager.FOLDER_NAME + "\": \"" + folderName + "\" }";
			
			$Phyer.postJson($Phyer.PHYER_ROOT + "phyle-box/business/FileManagementService.php?createNewFolder", data, 
					null, 
					function(json) {
						$Phyer.setJson(json);
					}
				);
		},
		
		/**
		 * createFileListDirectoryRow
		 * Creates a directory row in the file list.
		 * @param string location
		 * @param string directory
		 * @param string name
		 * @param string modifiedTime
		 * @param double size
		 * @param int permissions
		 * @return void
		 */
		createFileListDirectoryRow: function(location, directory, name, modifiedTime, size, permissions) {
			var fileListTable = $("#" + $Phyer.FileManager.FILE_LIST_TABLE + " > tbody");
			var newDirectory = directory.replace(/^\s*/, "").replace(/\s*$/, "") + name + "/";
			var newLocation = location.replace(/^\s*/, "").replace(/\s*$/, "");
			var newName = name;
			
			if (name == "..") {
				name = "";
				
				var locationComponents = directory.substring(0, (directory.length - 1)).split("/");
				
				directory = "";
				
				for (var i = 0; i < (locationComponents.length - 1); i++) {
					if (i > 0) {
						directory = directory + "/";
					}

					directory = directory + locationComponents[i];
				}
			}
			
			var row = "<td class=\"checkBox\"><input type=\"checkbox\" id=\"checkBox" + $Phyer.FileManager.__rowCounter + "\" name=\"checkBox" + $Phyer.FileManager.__rowCounter + "\" /></td><td class=\"icon\"><div class=\"typeDirectory\"></div></td><td class=\"fileOrDirName\"><a href=\"" + $Phyer.PHYER_ROOT + "phyle-box/file-manager.php?currentDirectory=" + directory + name + "/\">" + newName + "</a></td><td class=\"modifiedTime\">" + modifiedTime + "</td><td class=\"size\">" + size + " Kb</td><td class=\"permissions\">" + permissions + "</td><td class=\"actions\"></td>";
			
			if (($Phyer.FileManager.__rowCounter % 2) == 0) {
				$("<tr>" + row + "</tr>").appendTo(fileListTable);
			} else {
				$("<tr class=\"oddRow\">" + row + "</tr>").appendTo(fileListTable);
			}			
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
			var row = "<td class=\"checkBox\"><input type=\"checkbox\" id=\"checkBox" + $Phyer.FileManager.__rowCounter + "\" name=\"checkBox" + $Phyer.FileManager.__rowCounter + "\" /></td><td class=\"icon\"><div class=\"type" + type + "\"></div></td><td class=\"fileOrDirName\"><a href=\"javascript: void(0);\" onclick=\"javascript: $Phyer.FileManager.openFileEditor('" + name + "');\">" + name + "</a></td><td class=\"modifiedTime\">" + modifiedTime + "</td><td class=\"size\">" + size + " Kb</td><td class=\"permissions\">" + permissions + "</td><td class=\"actions\"></td>";
			
			if (($Phyer.FileManager.__rowCounter % 2) == 0) {
				$("<tr>" + row + "</tr>").appendTo(fileListTable);
			} else {
				$("<tr class=\"oddRow\">" + row + "</tr>").appendTo(fileListTable);
			}
		},
		
		/**
		 * openFileEditor
		 * Opens a file editor with the optional file name argument.
		 * @param string fileName
		 * @return void
		 */
		openFileEditor: function(fileName) {
			$.fancybox(
				{
					"width": 620,
					"height": 674,
					"autoScale": false,
					"transitionIn": true,
					"transitionOut": false,
					"type": "iframe",
					"href": "/phyle-box/simple-editor.php?fileName=" + fileName + "&driveSelector=" + $("#driveSelector").val() + "&currentDirectory=" + $("#currentDirectory").val()
				}
			);
		}
			
	}
	
	Phyer.prototype.FileManager = new FileManager();
	
}

window.$Phyer = new Phyer();
