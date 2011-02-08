<?php

require_once(dirname(__FILE__) . "/_login.inc.php");

$page = new _Login_Page();
$page->openDocument();

?>

<div class="loginPanel">
	<p>Welcome to PhyleBox. Please use the form below to log in.</p>
	<?php $page->phyleBoxLoginForm->render(); ?>
</div>

<?php

$page->closeDocument();

?>