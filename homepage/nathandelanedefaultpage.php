<?php
	$defaultStylesheetName = "default.css";
	$sitePath = "/webdesign/";
	$xml = null;
	$pageTitle = null;
	$contents = null;
	
	if(!isset($_GET['page']))
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
		if(strcmp($page, "main"))
		{
			$pageTitle = "Nathan Lane's Homepage";
		}
		elseif(strcmp($page, "appPortfolio"))
		{
			$pageTitle = "Applications Portfolio";
		}
		elseif(strcmp($page, "webPortfolio"))
		{
			$pageTitle = "Web Apps Portfolio";
		}
		elseif(strcmp($page, "kontaktieren"))
		{
			$pageTitle = "Contact Me";
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
