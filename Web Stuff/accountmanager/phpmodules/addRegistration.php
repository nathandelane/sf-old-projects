<?php
/* addRegistration.php
 *
 * This module adds a new user if it doesn't already exist in the users file.
 */
session_start();
require_once('info.php');

$fileLocation = $usersRoot . "/etc/users";
$lines = array();

include_once('administer.php');

// Check for correct administrator password.
if($adminAccepted)
{
	// Look to see if the users file is located correctly.
	if(file_exists($fileLocation))
	{
		// Get all of the logins from the users file to see if one matches the credentials supplied.
		$fp = fopen($fileLocation, 'r');
		while(!feof($fp))
		{
			$lines[] = fgets($fp);
		}
		fclose($fp);
		
		$thisNewUser = formatUserEntry();
		$username = $_POST['userNameField'];
		
		foreach($lines as $nextLine)
		{
			if($debug)
			{
				echo $nextLine . "<br/>";
			}
			
			$nextUser = explode(':', $nextLine);
			
			if($username == $nextUser[0])
			{
				$msg = "User already entered! Please go <a href='resetpassword.php'>here</a> if you need to reset a user's password.";
				$errorOccurred = true;
			}
		}
		
		if(!$errorOccurred)
		{
			$fp = fopen($fileLocation, 'a+');
			fwrite($fp,  "\r\n" . $thisNewUser);
			fclose($fp);
			$msg = "User was successfully added.";
			$successful = true;
		}
	}
	else
	{
		if($debug)
		{
			echo "User file location defined as: " . $fileLocation . "<br/>";
		}
		
		$msg = "Users file could not be found.";
		$errorOccurred = true;
	}
}
else
{
	$msg = "Administrator error.";
	$errorOccurred = true;
}

function formatUserEntry()
{
	$thisUser = $_POST['userNameField'] . ":" . $_POST['firstNameField'] . ":" . $_POST['lastNameField'] . ":" . md5($_POST['passwordField'] . $salt);
	
	if($debug)
	{
		echo "\$thisUser: " . $thisUser . "<br/>";
	}
	
	return $thisUser;
}
?>