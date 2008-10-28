<?php
/* resetPassword.php
 *
 * This module attempts to reset a password for an existing user.
 */
session_start();
require_once('info.php');
 
$fileLocation = $usersRoot . "/etc/users";
$lines = array();
$newUserProfile = "";

include_once('administer.php');

if($adminAccepted)
{
	if(file_exists($fileLocation))
	{
		$fp = fopen($fileLocation, 'r');
		while(!feof($fp))
		{
			$lines[] = fgets($fp);
		}
		fclose($fp);
		
		$thisNewUser = formatUserEntry();
		$username = $_POST['userNameField'];
		
		for($i = 0; $i < sizeof($lines); $i++)
		{
			if($debug)
			{
				echo $nextLine . "<br/>";
			}
			
			$nextUser = explode(':', $lines[$i]);
			
			if($username == $nextUser[0])
			{
				$lines[$i] = $_POST['userNameField'] . ":" . $nextUser[1] . ":" . $nextUser[2] . ":" . md5($_POST['passwordField'] . $salt);
				break;
			}
		}
		
		$fp = fopen($fileLocation, 'w+');
		foreach($lines as $nextLine)
		{
			fwrite($fp, $nextLine . "\r\n");
		}
		fclose($fp);
	}
	else
	{
		$msg = "Users file could not be found.";
		$errorOccurred = true;
	}
}
else
{
	$msg = "Administrator error.";
	$errorOccurred = true;
}
?>