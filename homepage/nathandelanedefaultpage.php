<?php
	$errorNumber;
	$errorMessage;
	$defaultStylesheetName = "default.css";
	$sitePath = "/";
	
	if(!isset($_GET['page']))
	{
		$contents = "main";
	}
	elseif(strcmp($_GET['page'], '') == 0)
	{
		$contents = "main";
	}
	else
	{
		$contents = $_GET['page'];
	}
	
	$contentsFile = '_contents/' . $contents . '.php';
	
	if(file_exists($contentsFile))
	{
		if(strcmp($contents, "main") == 0)
		{
			$pageTitle = "Nathan Lane's Homepage";
		}
		elseif(strcmp($contents, "appPortfolio") == 0)
		{
			$pageTitle = "Applications Portfolio";
		}
		elseif(strcmp($contents, "webPortfolio") == 0)
		{
			$pageTitle = "Web Apps Portfolio";
		}
		elseif(strcmp($contents, "kontaktieren") == 0)
		{
			$pageTitle = "Contact Me";
		}
		elseif(strcmp($contents, "helpmeplease") == 0)
		{
			$pageTitle = "Help";
		}
		elseif(strcmp($contents, "error") == 0)
		{
			header("http/1.0 " . $errorNumber . " " . $errorMessage);
			$pageTitle = "Error Occurred";
		}
		else
		{
			$pageTitle = "Untitled Page";
		}
	}
	else
	{
		$contents = "error";
		$errorNumber = 404;
		$errorMessage = "Not Found";
		header("http/1.0 " . $errorNumber . " " . $errorMessage);
	}
	
	require_once('_templates/default.php');	
?>
