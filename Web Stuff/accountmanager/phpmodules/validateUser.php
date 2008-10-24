<?php
/* validateUser.php
 *
 * This module validates the user to be logged in.
 */
session_start();
require_once('info.php');
 
$fileLocation = $usersRoot . "/etc/users";
$lines = array();

if(file_exists($fileLocation))
{
	$fp = fopen($fileLocation, 'r');
	while(!feof($fp))
	{
		$lines[] = fgets($fp);
	}
	fclose($fp);
	
	if($debug)
	{
		echo "raw password: " . $_POST['passwordField'] . "; " . md5($_POST['passwordField']) . "<br/>";
	}
	
	$username = $_POST['userNameField'];
	$password = md5($_POST['passwordField']);
	
	if($debug)
	{
		echo "Username=" . $username . ",Password=" . $password . "<br/>";
	}
	
	$i = 1;
	while(!$userValidated)
	{
		if($debug)
		{
			echo "-" . $lines[$i] . "<br/>";
		}
		
		$nextUser = explode(':', $lines[$i]);
		
		if($debug)
		{
			echo "$username == " . ((strcmp($nextUser[0], null) == 0) ? "Empty string" : $nextUser[0]) . "<br/>";
		}
		
		if(strcmp($username, chop($nextUser[0])) == 0)
		{
			if($debug)
			{
				echo "$password == " . chop($nextUser[3]) . "<br/>";
				echo strlen($password) . " == " . strlen(chop($nextUser[3])) . "<br/>";
			}
			
			if(strcmp($password, chop($nextUser[3])) == 0)
			{
				$_SESSION['user_validated'] = true;
				$errorOccurred = false;
			}
			else
			{
				$_SESSION['user_validated'] = false;
				$msg = "Username or password is invalid.";
				$errorOccurred = true;
			}
			
			if($debug)
			{
				echo "\$userValidated: " . (($userValidated) ? "true" : "false") . "<br/>";
			}
		}
		else
		{
			$msg = "Username or password is invalid.";
			$errorOccurred = true;
		}
		
		$i++;
		
		if($i == sizeof($lines))
		{
			break;
		}
	}
}
else
{
	$msg = "Users file could not be found.";
	$errorOccurred = true;
}
?>