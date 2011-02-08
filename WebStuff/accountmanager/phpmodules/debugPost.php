<?php
/* debugPost.php
 * 
 * This module shows all of the post variables, their values, and whether they are empty.
 */
session_start();
require_once('info.php');
 
if(isset($debug) && $debug)
{
	if(isset($_POST))
	{
		echo "POST<br/>";
		foreach($_POST as $key => $value)
		{
			echo "Key: " . $key . "; Value: " . $value;
			
			if(empty($_POST[$key]))
			{
				echo "; empty = true";
			}
			else
			{
				echo "; empty = false";
			}
			
			echo "<br/>";
		}
		
		if($invalidate)
		{
			echo "Invalidated<br/>";
		}
		else
		{
			echo "Valid<br/>";
		}
	}
	
	if(isset($_SESSION))
	{
		echo "SESSION<br/>";
		foreach($_SESSION as $key => $value)
		{
			echo "Key: " . $key . "; Value: " . $value;
			
			if(empty($_SESSION[$key]))
			{
				echo "; empty = true";
			}
			else
			{
				echo "; empty = false";
			}
			
			echo "<br/>";
		}
	}
}
?>
