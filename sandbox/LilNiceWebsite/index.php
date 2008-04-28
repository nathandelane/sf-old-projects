<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<?php
	header("Location: " . $homepage);
	session_start();
?>
<html>
	<head>
		<title>Log In | Lil' Nice Website</title>
		<link rel="stylesheet" type="text/css" href="_stylesheets/login.css"/>
	</head>
	<body>
		<?php
			$sessionId = session_id();
			
			if($_POST["sessionId"] != "")
			{
				require_once("_application/authentication/UserAuthentication.php");
				
				$userSession = new UserAuthentication($sessionId, $_POST["userName"], $_POST["password"]);
				
				if($userSession->isUserAuthenticated())
				{
					$sessionId = $userSession->getSessionId();
				}
				else
				{
					$sessionId = session_id();
				}
			}
		?>
		<div id="loginPanelContainer" class="loginPanelContainer">
			<h1>Log Into Lil' Nice Website</h1>
			<div class="hr"></div>
			<form id="loginForm" name="loginForm" method="post" action="index.php">
				<input name="sessionId" type="hidden" id="sessionId" value="<?php echo $sessionId; ?>"/>
				<fieldset class="credentials">
					<legend>User Credentials</legend>
					<label for="userName">User Name:&nbsp;</label>
					<input type="text" title="Enter user name" maxlength="" size="30" id="userName" name="userName" value=""/>
					&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<label for="password">Password:&nbsp;</label>
					<input type="password" title="Enter password" maxlength="" size="22" id="password" name="password" value=""/>
					<br/><br/>
					Remember Me On This Computer:&nbsp;
					<input type="radio" title="Do you want me to remember you on this computer" name="rememberMe" id="rememberMeYes" value="yes" checked/>
					<label for="rememberMeYes">Yes</label>
					<input type="radio" title="Do you want me to remember you on this computer" name="rememberMe" id="rememberMeNo" value="no"/>
					<label for="rememberMeNo">No</label>
				</fieldset>
				<fieldset class="information">
					<legend>Disclaimer</legend>
					<h3>Security</h3>
					<p>
						This web site is secure and uses SSL and certificates to maintain security. You will never be asked to give out your personal 
						login information. If you are, please report it immediately to the web master.
					</p>
					<h3>Usage Agreement</h3>
					<p>
						This site is provided as a service to our family. Please don't give others your information in order to gain access to this site. 
						This site will also be underconstant maintenance, as new features will be added from time to time. If you have a request, please 
						feel free to submit the request on the Feature Requests page after you log in.
					</p>
				</fieldset>
				<input class="submitButton" name="submitButton" id="submitButton" type="submit" value="Submit"/>
				<input class="resetButton" name="resetButton" id="resetButton" type="reset" value="Reset"/>
			</form>
		</div>
	</body>
</html>
