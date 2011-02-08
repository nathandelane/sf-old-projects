<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/IRenderable.inc.php");

/**
 * FileList
 * This class represents the file list.
 * @author lanathan
 *
 */
class FileList implements IRenderable {
	
	/**
	 * Constructor
	 * @return FileList
	 */
	public function FileList() {
		
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/IRenderable::render()
	 */
	public function render() {
		
?>
<div class="fileList">
	<table id="fileListTable" class="fileListTable">
		<caption>
			Files and Folders located in 
			<span id="captionFolderPath"></span>
		</caption>
		<thead>
			<tr>
				<td class="checkBox">
					<label for="checkAllCheckBox">Check All</label>
					<input type="checkbox" id="checkAllCheckBox" name="checkAllCheckbox" />
				</td>
				<td class="icon">File Type</td>
				<td class="fileOrDirName">File of Directory Name</td>
				<td class="modifiedTime">Modified Time</td>
				<td class="size">Size</td>
				<td class="permissions">Permissions</td>
				<td class="actions">Actions</td>
			</tr>
		</thead>
		<tbody>
		</tbody>
	</table>
	<script type="text/javascript">
		$("#checkAllCheckBox").change(function(e) {
			var target;
			
			if (!e) {
				var e = window.event;
			}

			if (e.target) {
				target = e.target;
			} else if (e.srcElement) {
				target = e.srcElement;
			}

			if (target.nodeType == 3) {
				target = target.parentNode;
			}
			
			if ($(target).attr("checked")) {
				$("input[type='checkbox']").attr("checked", true);
			} else {
				$("input[type='checkbox']").attr("checked", false);
			}
		});
	</script>
</div>
<?php
		
	}
	
}

?>