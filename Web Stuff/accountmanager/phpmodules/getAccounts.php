<?php
/* getAccounts.php
 *
 * 
 */
session_start();
require_once('info.php');

$dataIndexFile = $dataIndex . "/indexfile";
$noUserAccountFound = true;
$thisUser = $_SESSION['user_name'];

$fp = fopen($dataIndexFile, 'r');
while(!feof($fp))
{
	$lines[] = fgets($fp);
}
fclose($fp);

foreach($lines as $nextLine)
{
	$nextUserAccountsFile = explode(':', $nextLine);
	
	if($thisUser == $nextUserAccountsFile[0])
	{
		$userAccountsFile = $nextUserAccountsFile[1];
		$noUserAccountFound = false;
	}

	if(!$noUserAccountFound)
	{
		// Open data index
		$fp = fopen($dataIndex . "/" . $userAccountsFile, r);
		while(!feof($fp))
		{
			$lines[] = fgets($fp);
		}
		fclose($fp);
		
		$userAccountsFileFound = false;
		foreach($lines as $nextLine)
		{
			$nextUserAccountsFile = explode(':', $nextLine);
			
			if($nextUserAccountsFile[0] == $_SESSION['user_name'])
			{
				$userAccountsFile = $nextUserAccountsFile[1];
				$userAccountsFileFound = true;
			}
		}
		
		// Open user accounts file
		if($userAccountsFileFound)
		{
			$fp = fopen($dataIndex . "/" . $userAccountsFile, r);
			while(!feof($fp))
			{
				$accountlines[] = fgets($fp);
			}
			fclose($fp);
			
			for($i = 1; $i < sizeof($accountlines); $i++)
			{
				$nextUserAccount = explode(':', $accountlines[$i]);
				$class = "";
				if($counter % 2 > 0)
				{
					$class = "accountRowBlue";
				}
				else
				{
					$class = "accountRowYellow";
				}
				echo "<form id=\"accountsForm\" name=\"accountsForm\" method=\"post\" action=\"showaccount.php\">";
				echo "<input type='hidden' name='id' value='{$i}'/>";
				echo "<input type='hidden' name='name' value='{$nextUserAccount[0]}'/>";
				echo "<input type='hidden' name='hash' value='{$nextUserAccount[1]}'/>";
				echo "<input type='hidden' name='referringPage' value='accounts.php'/>";
				echo "<tr><td class=\"{$class}\">{$i}</td><td class=\"{$class}\">{$nextUserAccount[0]}</td><td class=\"{$class}\"><input style='float:right;' type='submit' value='Go to {$nextUserAccount[0]}'/></td></tr>";
				echo "</form>";
			}
		}
	}
}
?>