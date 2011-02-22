<?php

require_once(dirname(__FILE__) . "/_login.inc.php");

$page = new _Login_Page();
$page->openDocument();

?>

<div class="loginPanel">
	<p>Nathandelane.com client login. Please log in. If you have forgotten your credentails, please contact your account manager at Nathandelane.com.</p>
	<?php $page->utahKoiLoginForm->render(); ?>
</div>

<?php

$page->closeDocument();

?>