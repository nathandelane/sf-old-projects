<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

class FileManagerCommands implements IRenderable {
	
	public function FileManagerCommands() {
		
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div id="commands">
	<ul>
		<li>
			<a href="javascript: void(0);" id="createNewFileButton" title="Create New File" class="smallIcons createNewFile"></a>
		</li>
		<li>
			<a href="javascript: void(0);" id="createNewFolderButton" title="Create New Folder" class="smallIcons createNewFolder"></a>
		</li>
		<li>
			<a href="javascript: void(0);" id="uploadFiles" title="Upload Files" class="phyleBoxIcons upload"></a>
		</li>
		<li>
			<a href="javascript: void(0);" id="downloadFiles" title="Select Files to Download" class="phyleBoxIcons download"></a>
		</li>
	</ul>
</div>
<script type="text/javascript">
	$(document).ready(function() {
		$("#createNewFileButton").click(function(e) {
			$.fancybox(
				"<h2>Create New File</h2><label for=\"newFileName\">New File Name:</label><input type=\"text\" id=\"newFileName\" name=\"newFileName\" value=\"\" /><input type=\"button\" value=\"Create File\" onclick=\"javascript: var newFileName = $('#newFileName').val(); $Phyer.FileManager.openFileEditor(newFileName);\" />",
				{
					"autoDimensions": false,
					"width": 350,
					"height": "auto",
					"transitionIn": "none",
					"transitionOut": "none"
				}
			);
		});
		$("#createNewFolderButton").click(function(e) {
			$.fancybox(
				"<h2>Create New Folder</h2><label for=\"newFileName\">New Folder Name:</label><input type=\"text\" id=\"newFolderName\" name=\"newFolderName\" value=\"\" /><input type=\"button\" value=\"Create Folder\" onclick=\"javascript: $Phyer.FileManager.createFolder();\" />",
				{
					"autoDimensions": false,
					"width": 380,
					"height": "auto",
					"transitionIn": "none",
					"transitionOut": "none"
				}
			);
		});
		$("#uploadFiles").click(function(e) {});
		$("#downloadFiles").click(function(e) {});
	});
</script>
<?php
		
	}
	
}

?>