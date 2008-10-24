<?php
	include_once('../_phpmodules/info.php');
	
	$invalidate = false;

	if(isset($_POST['posted']))
	{
		if(empty($_POST['userNameField']))
		{
			$invalidate = true;
		}
		else if(empty($_POST['passwordField']))
		{
			$invalidate = true;
		}
		else if(empty($_POST['passwordConfirmField']))
		{
			$invalidate = true;
		}
		else if(empty($_POST['adminPasswordField']))
		{
			$invalidate = true;
		}
		else // Since the post is valid, process the information.
		{
			$adminPass = $_POST['adminPasswordField'];
			include_once('../_phpmodules/resetPassword.php');
		}
	}	
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
	<head>
		<title>Admin - Reset Existing User's Password</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<link media="screen" type="text/css" href="../_stylesheets/main.css" rel="stylesheet" />
		<link rel="shortcut icon" href="../favicon.ico"/>
	</head>
	<body>
		<?php
			include_once('../_phpmodules/debugPost.php');
		?>
		<div id="headerBar" class="headerBar">
			<div id="accountManagerButton" class="accountManagerButton" style="background-position: center left;" onmouseover="this.style.backgroundPosition='center right'" onmouseout="this.style.backgroundPosition='center left'" onclick="javascript: document.location='/accountmanager/'"></div>
		</div>
		<div id="pageContainer" class="pageContainer">
			<div id="mainContent" class="mainContent">
				<div id="welcome" class="welcome">
					<h1>Use This Form to Reset Existing Users' Passwords.</h1>
					<p>
						Please use this form to change or reset an existing user's password.
					</p>
					<div id="registerFormContainer" class="formContainer">
						<div id="errorForm" style="font-size:10px;font-weight:bold;padding:2px;display:<?php if($invalidate) { echo "block"; } else { echo "none"; } ?>;background-color:#ff0000;">
							Error occurred! All fields are required!
						</div>
						<div id="errorTransaction" style="font-size:10px;font-weight:bold;padding:2px;display:<?php if($errorOccurred) { echo "block"; } else { echo "none"; } ?>;background-color:#ff0000;">
							Error occurred! <?php echo $msg; ?>
						</div>
						<form id="loginForm" name="loginForm" method="post" action="resetpassword.php">
							<input id="hiddenInput" name="posted" type="hidden" value="1"/>
							<label for="userNameField">Username:</label>
							<input id="userNameField" type="text" name="userNameField" class="textInputField"/>
							<label for="passwordField">Password:</label>
							<input id="passwordField" type="password" name="passwordField" class="textInputField"/>
							<label for="passwordConfirmField">Confirm Password:</label>
							<input id="passwordConfirmField" type="password" name="passwordConfirmField" class="textInputField"/>
							<label for="adminPasswordField">Administrator Password:</label>
							<input id="adminPasswordField" type="password" name="adminPasswordField" class="textInputField"/>
							<p></p>
							<div>
								<input id="submitButton" name="submitButton" value="Reset Password" type="submit" class="resetPasswordSubmitButton"/>
							</div>
						</form>
					</div>					
				</div>
			</div>
		</div>
		<div id="footer" class="footer">
			Copyright &copy; 2008, Nathan Lane
		</div>
	</body>
</html>