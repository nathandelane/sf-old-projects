<?php

require_once(dirname(__FILE__) . "/_sign-up-upload-avatar.inc.php");

$page = new _Sign_Up_Upload_Avatar();
$page->openDocument();

?>
<div class="contentInner">
	<h1>Sign Up for PhyleBox - Upload Avatar</h1>
	<div class="panel">
		<div class="panelInner">
			<form id="signUpForm" name="signUpForm" action="<?php echo $_SERVER["REQUEST_URI"]; ?>" method="post" enctype="multipart/form-data">
				<input type="hidden" id="token" name="token" value="<?php echo $page->createToken(); ?>" />
				<table border="0" id="registrationFormTable">
					<tbody>
					</tbody>
				</table>
				<div class="formRow">
					<input id="submitButton" type="submit" value="Finish Sign Up and Log In >>" />
				</div>
			</form>
		</div>
	</div>
</div>
<?php

$page->closeDocument();

?>