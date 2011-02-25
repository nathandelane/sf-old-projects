<?php

require_once(dirname(__FILE__) . "/_index.inc.php");

$page = new _Index_Page();
$page->openDocument();

?>
<div id="appContainer">
	<a class="applicationIcons fileManager left" href="<?php echo PhyleBox_Config::getPhyleBoxRoot() . "/file-manager.php"  ?>" title="Use File Manager"></a>
	<a class="applicationIcons profileManager" href="<?php echo PhyleBox_Config::getPhyleBoxRoot() . "/profile.php"  ?>" title="Profile Manager"></a>
</div>
<?php

$page->closeDocument();

?>