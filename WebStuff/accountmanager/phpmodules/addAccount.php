<?php
session_start();
require_once('info.php');

// This module is used to add accounts for a particular user.
if(empty($_POST['accountNameField']))
{
	$msg = "You must name your account.";
	$errorOccurred = true;
}
else
{
	$dataIndexFile = $dataIndex . "/indexfile";
	$accountsFileName = $dataIndex . "/" . md5($_SESSION['user_name'] . $salt);
	$accountName = md5($_POST['accountNameField'] . $salt);
	
	// Add user's accounts file entry.
	if(file_exists($dataIndexFile))
	{
		// Find the user and his accounts file
		$noUserAccountFound = true;
		
		$fp = fopen($dataIndexFile, 'r');
		while(!feof($fp))
		{
			$lines[] = fgets($fp);
		}
		fclose($fp);
		
		$thisUser = $_SESSION['user_name'];
		
		foreach($lines as $nextLine)
		{
			if($debug)
			{
				echo $nextLine . "<br/>";
			}
			
			$nextUserAccountsFile = explode(':', $nextLine);
			
			if($thisUser == $nextUserAccountsFile[0])
			{
				$userAccountsFile = $nextUserAccountsFile[1];
				$noUserAccountFound = false;
			}
		}
		
		// Create it
		if($noUserAccountFound)
		{
			$newUserAccount = formatUserAccountsEntry(md5($_SESSION['user_name'] . $salt));
			$fp = fopen($dataIndexFile, 'a+');
			fwrite($fp,  "\r\n" . $newUserAccount);
			fclose($fp);
			$fp = fopen($accountsFileName, "x");
			fwrite($fp, "\r\n" . formatAccountName($_POST['accountNameField'], $accountName));
			fclose($fp);
			$fp = fopen(($dataIndex . "/" . $accountName), "x");
			fclose($fp);
			
			if($debug)
			{
				echo "newUserAccount: " . $newUserAccount . "<br/>";
				echo "accountsFileName: " . $accountsFileName . "<br/>";
				echo "formatAccountName(): " . formatAccountName($_POST['accountNameField']) . "<br/>";
				echo "accountName: " . $accountName . "<br/>";
			}
			
			$msg = "User was successfully added.";
			$successful = true;
		}
		else // Or open it and add the account if it doesn't already exist
		{
			$thisUsersAccountsFile = $dataIndex . "/" . $userAccountsFile;
			
			// Now ensure that the account doesn't already exist
			$accountExists = false;
			$fp = fopen($thisUsersAccountsFile, 'r');
			while(!feof($fp))
			{
				$lines[] = fgets($fp);
			}
			fclose($fp);
			
			$thisAccount = $_POST['accountNameField'];
			
			foreach($lines as $nextLine)
			{
				if($debug)
				{
					echo $nextLine . "<br/>";
				}
				
				$nextUserAccount = explode(':', $nextLine);
				
				if($thisAccount == $nextUserAccount[0])
				{
					$accountExists = true;
				}
			}
			
			// Add the account.
			if($accountExists)
			{
				$errorOccurred = true;
				$msg = "Account named " . $_POST['accountNameField'] . " already exists.";
			}
			else
			{
				$fp = fopen($accountsFileName, "a");
				fwrite($fp, "\r\n" . formatAccountName($_POST['accountNameField'], $accountName));
				fclose($fp);
				$fp = fopen(($dataIndex . "/" . $accountName), "x");
				fclose($fp);
			}
		}
	}
}

function formatUserAccountsEntry($accountsFileName)
{
	$thisAccountsFile = $_SESSION['user_name'] . ":" . $accountsFileName;
	
	if($debug)
	{
		echo "\$thisAccountsFile: " . $thisAccountsFile . "<br/>";
	}
	
	return $thisAccountsFile;
}

function formatAccountName($accountNameField, $accountName)
{
	$thisAccountName = $accountNameField . ":" . $accountName;
	
	if($debug)
	{
		echo "\$thisAccountName: " . $thisAccountName . "<br/>";
	}
	
	return $thisAccountName;
}
?>