<?php

require_once(dirname(__FILE__) . "/_users.inc.php");

$page = new _Users_Page();
$page->openDocument();

?>
<table id="usersEnrolled">
	<thead>
		<tr>
			<th>Username</th>
			<th>First Name</th>
			<th>Last Name</th>
			<th>Disabled</th>
			<th>Reason</th>
			<th>Locked</th>
			<th>Content Type</th>
			<th>Signature</th>
			<th>Account Type</th>
			<th>Date of Birth</th>
			<th>Merits</th>
			<th>Date Created</th>
		</tr>
	</thead>
	<tbody>
		<?php $page->renderUsers(); ?>
	</tbody>
</table>
<?php

#page->closeDocument();

?>