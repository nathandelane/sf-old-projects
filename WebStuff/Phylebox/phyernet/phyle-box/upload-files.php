<?php

require_once(dirname(__FILE__) . "/_upload-files.inc.php");

$page = new _Upload_Files_Page();
$page->openDocument();

?>
<div id="contentInner">
	<form id="fileUploadForm" action="<?php echo $_SERVER["REQUEST_URI"]; ?>" method="post" enctype="multipart/form-data">
		<input type="hidden" name="token" id="token" value="<?php echo "{$page->createToken()}"; ?>" />
		<input type="hidden" 
		<input type="file" name="filesBeingUploaded" id="filesBeingUploaded" value="" />
		<input type="submit" value="Upload Images" id="submit" />
	</form>
	<span><?php echo "{$page->successMessage}"; ?></span>
</div>
<?php

$page->closeDocument();

?>