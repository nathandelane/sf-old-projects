<?php

require_once(dirname(__FILE__) . "/_login.inc.php");

$page = new _Login_Page();
$page->openDocument();

?>

<div class="loginPanel">
	<p>Welcome to the PhyerNet Admin. Please use the form below to log in.</p>
	<?php $page->adminLoginForm->render(); ?>
</div>

<?php

$page->closeDocument();

?>