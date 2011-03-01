<?php

require_once(dirname(__FILE__) . "/_image-viewer.inc.php");

$page = new _Image_Viewer_Page();
$page->openDocument();

?>
<div id="imageViewerContainer">
	<img src="<?php $page->imageLoaderControl->render(); ?>" />
</div>
<?php

$page->closeDocument();

?>