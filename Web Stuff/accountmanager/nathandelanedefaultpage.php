<?php
	session_start();
	include_once('info.php');
	
	if(!$_SESSION['user_validated'])
	{
		if(empty($userValidated))
		{
			$userValidated = false;
		}
			
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
			else // Process the information.
			{
				include_once('phpmodules/validateUser.php');
			}
		}	
		
		if(!empty($userValidated))
		{
			if($userValidated)
			{
				header('Location: /accountmanager/accounts.php');
			}
			else
			{
				$msg = "An unknown error occurred!";
				$errorOccurred = true;
			}
		}
	}
	else
	{
		header('location: /accountmanager/accounts.php');
	}
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
	<head>
		<title>Account Manager 0.1.0</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<link media="screen" type="text/css" href="_stylesheets/main.css" rel="stylesheet" />
		<link rel="shortcut icon" href="favicon.ico"/>
	</head>
	<body>
		<?php
			include_once('phpmodules/debugPost.php');
		?>
		<div id="headerBar" class="headerBar">
			<div id="accountManagerButton" class="accountManagerButton" style="background-position: center left;" onmouseover="this.style.backgroundPosition='center right'" onmouseout="this.style.backgroundPosition='center left'" onclick="javascript: document.location='/accountmanager/'"></div>
		</div>
		<div id="pageContainer" class="pageContainer">
			<div id="mainContent" class="mainContent">
				<div id="welcome" class="welcome">
					<h1>Welcome to the Account Manager.</h1>
					<h4>This is Account Manager version <?php echo $version; ?>.</h4>
					<p>
						This tool can be used to update and keep track of personal spending. It allows you to graph spending habits and understand income to spending ratios. It can be updated by simply downloading a CSV (Comma Separated Value) file from your bank's or credit union's web site and importing the data into this tool using the <span class="blueText">Import</span> feature.
					</p>
					<p>
						You may enter as many accounts into this tool as you'd like and keep track of all of them.
					</p>
					<p>
						Please log in below with your credentials.
					</p>
					<div id="loginFormContainer" class="formContainer">
						<div id="errorForm" style="font-size:10px;font-weight:bold;padding:2px;display:<?php if($invalidate) { echo "block"; } else { echo "none"; } ?>;background-color:#ff0000;">
							Error occurred! All fields are required!
						</div>
						<div id="errorTransaction" style="font-size:10px;font-weight:bold;padding:2px;display:<?php if($errorOccurred) { echo "block"; } else { echo "none"; } ?>;background-color:#ff0000;">
							Error occurred! <?php echo $msg; ?>
						</div>
						<form id="loginForm" name="loginForm" method="post" action="accounts.php">
							<input id="referringPage" name="referringPage" type="hidden" value="/index.php"/>
							<input id="hiddenInput" name="posted" type="hidden" value="1"/>
							<label for="userNameField">Username:</label>
							<input id="userNameField" type="text" name="userNameField" class="textInputField"/>
							<label for="passwordField">Password:</label>
							<input id="passwordField" type="password" name="passwordField" class="textInputField"/>
							<p></p>
							<div>
								<input id="submitButton" name="submitButton" value="Submit" type="submit" class="loginSubmitButton"/>
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