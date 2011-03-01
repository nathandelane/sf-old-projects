if (Phyer && $Phyer) {
	
	var FileManager = function() { }
	
	FileManager.prototype = {
		/**
		 * Static variables
		 */
		__rowCounter: 0,
		__imageFileExtensions: new Array("png", "svg", "jpg", "jpeg", "bmp", "gif"),
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
						for (var fileIndex = 0; fileIndex <= fileCount; fileIndex++) {
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
						if ($("input[type='checkbox']:checked").length > 0) {
							$("#downloadFiles").attr("class", "phyleBoxIcons download active");
							$("#downloadFiles").attr("title", "Download Files");
							$("#deleteFiles").attr("class", "phyleBoxIcons delete active");
							$("#deleteFiles").attr("title", "Delte Selected Files");
						} else {
							$("#downloadFiles").attr("class", "phyleBoxIcons download");
							$("#downloadFiles").attr("title", "Select Files to Download");
							$("#deleteFiles").attr("class", "phyleBoxIcons delete");
							$("#deleteFiles").attr("title", "Select Files to be Deleted");
						}
					});
					
					$(".actionIcons.changeProperties").click(function(e) {
						$.fancybox(
							{
								"width": 400,
								"height": 600,
								"autoScale": false,
								"transitionIn": true,
								"transitionOut": false,
								"type": "iframe",
								"href": "/phyle-box/file-properties.php?fileName=" + $(this).attr("name") + "&driveSelector=" + $("#driveSelector").val() + "&currentDirectory=" + $("#currentDirectory").val() + "&type=" + $(this).attr("fm:type")
							}
						);
					});
				}
			);			
		},
		
		/**
		 * editFile
		 * Creates or edits a file in the current directory.
		 * @param string location
		 * @param string directory
		 * @param string fileName
		 * @param string contents
		 * @return bool
		 */
		editFile: function(location, directory, fileName, contents) {
			var data = "{ \"" + $Phyer.FileManager.DRIVE_LOCATION + "\": \"" + location + "\", \"" + $Phyer.FileManager.DIRECTORY + "\": \"" + directory + "\", \"" + $Phyer.FileManager.FILE_NAME + "\": \"" + fileName + "\", \"" + $Phyer.FileManager.FILE_CONTENTS + "\": \"" + contents + "\" }";
			
			$Phyer.postJson($Phyer.PHYER_ROOT + "phyle-box/business/FileManagementService.php?editFile", data, 
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
			
			if (name === "..") {
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
			
			var row = "";
			
			if (newName === "..") {
				row = "<td class=\"checkBox\"><input type=\"checkbox\" id=\"checkBox" + $Phyer.FileManager.__rowCounter + "\" name=\"checkBox" + $Phyer.FileManager.__rowCounter + "\" /></td><td class=\"icon\"><a href=\"" + $Phyer.PHYER_ROOT + "phyle-box/file-manager.php?currentDirectory=" + $.URLEncode(directory + name) + "%2F&amp;driveSelector=" + location + "\"><div class=\"typeUpDirectory\"></div></a></td><td class=\"fileOrDirName\"><a href=\"" + $Phyer.PHYER_ROOT + "phyle-box/file-manager.php?currentDirectory=" + $.URLEncode(directory + name) + "%2F&amp;driveSelector=" + location + "\">" + newName + "</a></td><td class=\"modifiedTime\">" + modifiedTime + "</td><td class=\"size\">" + size + " Kb</td><td class=\"permissions\">" + permissions + "</td><td class=\"actions\"></div></td>";
			} else {
				row = "<td class=\"checkBox\"><input type=\"checkbox\" id=\"checkBox" + $Phyer.FileManager.__rowCounter + "\" name=\"checkBox" + $Phyer.FileManager.__rowCounter + "\" /></td><td class=\"icon\"><a href=\"" + $Phyer.PHYER_ROOT + "phyle-box/file-manager.php?currentDirectory=" + $.URLEncode(directory + name) + "%2F&amp;driveSelector=" + location + "\"><div class=\"typeDirectory\"></div></a></td><td class=\"fileOrDirName\"><a href=\"" + $Phyer.PHYER_ROOT + "phyle-box/file-manager.php?currentDirectory=" + $.URLEncode(directory + name) + "%2F&amp;driveSelector=" + location + "\">" + newName + "</a></td><td class=\"modifiedTime\">" + modifiedTime + "</td><td class=\"size\">" + size + " Kb</td><td class=\"permissions\">" + permissions + "</td><td class=\"actions\"><div class=\"actionIcons changeProperties\" name=\"" + name + "\" title=\"Edit properties for " + name + "\" fm:type=\"directory\"></div></td>";
			}
			
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
			var openFunction = "openFileEditor";
			
			if ($Phyer.FileManager.isImage(type)) {
				if (type === "svg") {
					openFunction = "openImageViewerSvg";
				} else {
					openFunction = "openImageViewer";
				}
			}
			
			var row = "<td class=\"checkBox\"><input type=\"checkbox\" id=\"checkBox" + $Phyer.FileManager.__rowCounter + "\" name=\"checkBox" + $Phyer.FileManager.__rowCounter + "\" /></td><td class=\"icon\"><a href=\"javascript: void(0);\" onclick=\"javascript: $Phyer.FileManager." + openFunction + "('" + name + "');\"><div class=\"type" + type + "\"></div></a></td><td class=\"fileOrDirName\"><a href=\"javascript: void(0);\" onclick=\"javascript: $Phyer.FileManager." + openFunction + "('" + name + "');\">" + name + "</a></td><td class=\"modifiedTime\">" + modifiedTime + "</td><td class=\"size\">" + size + " Kb</td><td class=\"permissions\">" + permissions + "</td><td class=\"actions\"><div class=\"actionIcons changeProperties\" name=\"" + name + "\" title=\"Edit properties for " + name + "\" fm:type=\"" + type + "\"></div></td>";
			
			if (($Phyer.FileManager.__rowCounter % 2) == 0) {
				$("<tr>" + row + "</tr>").appendTo(fileListTable);
			} else {
				$("<tr class=\"oddRow\">" + row + "</tr>").appendTo(fileListTable);
			}
		},
		
		/**
		 * Creates a new file uploader input for the uploader page.
		 * @return void
		 */
		createNewFileUploaderRow: function() {
			var totalNumberOfFiles = parseInt($("#numberOfFilesUploaded").val());
			
			if (totalNumberOfFiles < 20) {
				totalNumberOfFiles = totalNumberOfFiles + 1;
				
				var row = "<div class=\"formRow\"><label>File " + totalNumberOfFiles + ": </label><input class=\"file\" type=\"file\" name=\"filesBeingUploaded[]\" id=\"filesBeingUploaded[]\" value=\"\" /></div>";
				
				$(row).appendTo("#filesFieldSet");			
				$("#numberOfFilesUploaded").val("" + totalNumberOfFiles);
			}
		},
		
		/**
		 * refreshFileList
		 * Refreshes the file list.
		 * @return void.
		 */
		refreshFileList: function() {
			$Phyer.FileManager.populateFileList($("#driveSelector").val(), $("#currentDirectory").val());
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
					"height": 690,
					"autoScale": false,
					"transitionIn": true,
					"transitionOut": false,
					"type": "iframe",
					"href": "/phyle-box/simple-editor.php?fileName=" + fileName + "&driveSelector=" + $("#driveSelector").val() + "&currentDirectory=" + $("#currentDirectory").val(),
					"onClosed": function() {
						$Phyer.FileManager.populateFileList($("#driveSelector").val(), $("#currentDirectory").val());
					}
				}
			);
		},
		
		/**
		 * openImageViewer
		 * Opens an image viewer to view the selected image.
		 * @param string location
		 * @param string directory
		 * @param string fileName
		 * @return void
		 */
		openImageViewer: function(fileName) {
			$.fancybox(
				"<h1>We are sorry. This function is currently not available</h1><span>Please check back in about 24 hours.</span>",
				{
					"autoDimensions": false,
					"width": 350,
					"height": "auto",
					"transitionIn": "none",
					"transitionOut": "none",
				}
			);
		},
		
		/**
		 * openImageViewerSvg
		 * Opens a special SVG image viewer in case the user-agent doesn't support SVG.
		 * @param string location
		 * @param string directory
		 * @param string fileName
		 * @return void
		 */
		openImageViewerSvg: function(fileName) {
			$.fancybox(
				"<h1>We are sorry. This function is currently not available</h1><span>Please check back in about 24 hours.</span>",
				{
					"autoDimensions": false,
					"width": 350,
					"height": "auto",
					"transitionIn": "none",
					"transitionOut": "none"
				}
			);			
		},
		
		/**
		 * openFileUploader
		 * Opens the file uploader.
		 * @return void
		 */
		openFileUploader: function() {
			$.fancybox(
				{
					"width": 620,
					"height": 674,
					"autoScale": false,
					"transitionIn": true,
					"transitionOut": false,
					"type": "iframe",
					"href": "/phyle-box/upload-files.php?driveSelector=" + $("#driveSelector").val() + "&currentDirectory=" + $("#currentDirectory").val(),
					"onClosed": function() {
						$Phyer.FileManager.populateFileList($("#driveSelector").val(), $("#currentDirectory").val());
					}
				}
			);
		},
		
		/**
		 * isImage
		 * Gets whether a file appears to be an image.
		 * @param string fileType
		 * @return bool
		 */
		isImage: function(fileType) {
			var retVal = false;

			for (index = 0; index < $Phyer.FileManager.__imageFileExtensions.length; index++) {
				if ($Phyer.FileManager.__imageFileExtensions[index] === fileType) {
					retVal = true;
					
					break;
				}
			}
			
			return retVal;
		}			
	}
	
	Phyer.prototype.FileManager = new FileManager();
	
}

window.$Phyer = new Phyer();
