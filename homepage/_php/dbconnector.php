Begin 
<?php
	$dbhost = "localhost";
	$dbuser = "root";
	$dbpass = "config1";
	
	if(function_exists(mysql_connect)) {
		$conn = mysql_connect($dbhost, $dbuser, $dbpass) or die('Error connecting to mysql');
		print("connected.");
	} else {
		print("could not connect.");
		phpinfo();
	}
?>
End