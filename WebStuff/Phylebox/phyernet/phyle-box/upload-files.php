<?php

require_once(dirname(__FILE__) . "/_upload-files.inc.php");

$page = new _Upload_Files_Page();
$page->openDocument();

?>
<div id="uploadFilesContainer">
	<form id="fileUploadForm" action="<?php echo $_SERVER["REQUEST_URI"]; ?>" method="post" enctype="multipart/form-data">
		<input type="hidden" name="token" id="token" value="<?php echo "{$page->createToken()}"; ?>" />
		<input type="hidden" name="driveSelector" id="driveSelector" value="<?php echo $page->getFieldValue("driveSelector"); ?>" />
		<input type="hidden" name="currentDirectory" id="currentDirectory" value="<?php echo $page->getFieldValue("currentDirectory"); ?>" />
		<input type="hidden" name="numberOfFilesUploaded" id="numberOfFilesUploaded" value="1" />
		<input type="hidden" name="MAX_FILE_SIZE" id="MAX_FILE_SIZE" value="<?php echo ini_get("max_filesize"); ?>" />
		<input type="hidden" name="MAX_FILE_UPLOADS" id="MAX_FILE_UPLOADS" value="<?php echo ini_get("max_file_uploads"); ?>" />
		<div class="formRow">
			<input type="button" id="addFileButton" value="Additional File (up to <?php echo ini_get("max_file_uploads"); ?>)" onclick="javascript: void(0);" />
			<input type="submit" value="Upload Files" id="submit" />
		</div>
		<fieldset id="filesFieldSet">
			<legend>Files to Upload:</legend>
			<div class="formRow">
				<label>File 1:</label>
				<input class="file" type="file" name="filesBeingUploaded[]" id="filesBeingUploaded[]" value="" />
			</div>
		</fieldset>
	</form>
	<div id="successMessage"><?php echo "{$page->successMessage}"; ?></div>
</div>
<?php

$page->closeDocument();

?>