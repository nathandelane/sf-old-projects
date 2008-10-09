<?php
	$defaultStylesheetName = "default.css";
	$sitePath = "/";
	$xml = null;
	$pageTitle = null;
	$contents = null;
	
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
		else
		{
			$pageTitle = "Untitled Page";
		}
	}
	else
	{
		$contents = "error";
		header("http/1.0 404 Not Found");
	}
	
	require_once('_templates/default.php');	
?>
