<?php

require_once(dirname(__FILE__) . "/_users.inc.php");

$page = new _Users_Page();
$page->openDocument();

?>
<table id="usersEnrolled">
	<thead>
		<tr>
			<th>Username</th>
			<th></th>
		</tr>
	</thead>
</table>
<?php

#page->closeDocument();

?>