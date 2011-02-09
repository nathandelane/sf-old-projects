<?php

require_once(dirname(__FILE__) . "/_simple-editor.inc.php");

$page = new _Simple_Editor_Page();
$page->openDocument();

?>
<div id="simpleEditorContainer">
	<input type="hidden" name="driveSelector" id="driveSelector" value="<?php echo $page->getFieldValue("driveSelector"); ?>" />
	<input type="hidden" name="currentDirectory" id="currentDirectory" value="<?php echo $page->getFieldValue("currentDirectory"); ?>" />
	<label for="fileName">File Name:</label>
	<input type="text" id="fileName" name="fileName" value="<?php echo $page->getFieldValue("fileName"); ?>" />
	<textarea id="textFileContents" name="textFileContents"><?php echo $page->getFileContents($page->getFieldValue("driveSelector"), $page->getFieldValue("currentDirectory"), $page->getFieldValue("fileName")); ?></textarea>
	<input type="button" value="Save File" id="saveButton" />
</div>
<?php

$page->closeDocument();

?>