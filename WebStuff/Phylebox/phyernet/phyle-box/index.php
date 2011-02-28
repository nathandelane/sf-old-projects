<?php

require_once(dirname(__FILE__) . "/_index.inc.php");

$page = new _Index_Page();
$page->openDocument();

?>
<div id="appContainer">
	<a id="fileManager" class="applicationIcons fileManager left" href="<?php echo PhyleBox_Config::getPhyleBoxRoot() . "/file-manager.php"  ?>" title="Use File Manager"></a>
	<a id="profileManager" class="applicationIcons profileManager" href="<?php echo PhyleBox_Config::getPhyleBoxRoot() . "/profile.php"  ?>" title="Profile Manager"></a>
</div>
<div id="fileManagerContent" class="btContent">
	<h4>File Manager</h4>
	<p>This here you can upload, download and manage the files and folders that are in your account.</p>
</div>
<div id="profileManagerContent" class="btContent">
	<h4>Profile Manager</h4>
	<p>Make changes to your account or configure how PhyleBox functions for you!</p>
</div>
<?php

$page->closeDocument();

?>