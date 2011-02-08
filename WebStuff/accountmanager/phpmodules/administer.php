<?php
/* adminster.php
 *
 * This module is used to manage the administrator account.
 */
session_start();
require_once('info.php');

$adminFileLocation = $adminRoot . "/admin";
$adminAccepted = false;

if(isset($_POST['adminPasswordField']))
{
	if(file_exists($adminFileLocation))
	{
		$afp = fopen($adminFileLocation, 'r');
		$pwhash = fgets($afp);
		fclose($afp);
		
		$enteredHash = md5($adminPass);
		
		if($pwhash == $enteredHash)
		{
			$adminAccepted = true;
		}
		else
		{
			if($debug)
			{
				echo "\$pwhash: " . $pwhash . ";<br/>\$enteredHash: " . $enteredHash . "<br/>";
			}
		}
	}
	else
	{
		echo "Internal error occurred!";
	}
}
?>