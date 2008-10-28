<?php
	$username;
	
	session_start();
	include_once('info.php');
	
	if(isset($_POST['referringPage']))
	{
		if(!$_SESSION['user_validated'])
		{
			// Always validate user
			if(empty($_SESSION['user_validated']))
			{
				$_SESSION['user_validated'] = false;
			}
				
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
					$username = $_POST['userNameField'];
					session_register('user_name');
					session_register('pass_word');
					$_SESSION['user_name'] = $_POST['userNameField'];
					$_SESSION['pass_word'] = $_POST['passwordField'];
					include_once('phpmodules/validateUser.php');
				}
			}
			else
			{
				header('Location: /accountmanager/');
			}

			if(!empty($userValidated))
			{
				if(!$userValidated)
				{
					header('Location: /accountmanager/');
				}
				else
				{
					session_register('user_validated');
					$user_validated = $userValidated;
				}
			}
			// ********** End of validation **************
		}
		else
		{
		}
		
		if(strcmp($_POST['referringPage'], "/accounts.php") == 0)
		{
			require_once('phpmodules/addAccount.php');
		}
	}
	else
	{
		
	}
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
	<head>
		<title>Account Manager 0.1.0 - Accounts</title>
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
				<?php
					if($_SESSION['user_validated'])
					{
				?>
					<div>
						<a id="logoutLink" href="phpmodules/logout.php">Logout</a>
					</div>
					<h1>Welcome to Your Accounts Page.</h1>
					<p>
						On this page you may manage your accounts. If you currently have no accounts, then use this page to create a new account.
					</p>
					<div id="addAccountFormContainer" class="formContainer">
						<div id="errorTransaction" style="font-size:10px;font-weight:bold;padding:2px;display:<?php if($errorOccurred) { echo "block"; } else { echo "none"; } ?>;background-color:#ff0000;">
							Error occurred! <?php echo $msg; ?>
						</div>
						<form id="loginForm" name="loginForm" method="post" action="accounts.php">
							<input id="referringPage" name="referringPage" type="hidden" value="/accounts.php"/>
							<input id="hiddenInput1" name="posted" type="hidden" value="1"/>
							<label for="accountNameField">Account Name:</label>
							<input id="accountNameField" type="text" name="accountNameField" class="textInputField"/>
							<p></p>
							<div>
								<input id="submitButton" name="submitButton" value="Add Account" type="submit" class="accountAddButton"/>
							</div>
						</form>
					</div>
					<p>
						<table width="100%" border="0" style="border-collapse:collapse;">
							<tbody>
				<?php
							require_once('phpmodules/getAccounts.php');
				?>
							</tbody>
						</table>
					</p>
				<?php								
					}
					else
					{
				?>
					<h1>We're Sorry But You Could Not Be Validated.</h1>
					<p>
						Some possible reasons for this include:
						<ul>
							<li>Your login credentials were not recognized</li>
							<li>Your session expired</li>
							<li>The server is experiencing problems</li>
						</ul>
					</p>
					<p>
						Please click <a href="/accountmanager/">here</a> to go back to the Account Manager login page.
					</p>
					<p>
						If you feel like you reached this page in error, please contact the site administrator at <a href="mailto:nathamberlane@gmail.com">nathamberlane at gmail dot com</a>. Thank you.
					</p>
				<?php
					}
				?>
			</div>
		</div>
		<div id="footer" class="footer">
			Copyright &copy; 2008, Nathan Lane
		</div>
	</body>
</html>