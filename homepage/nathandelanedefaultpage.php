<?php
	session_start();
	
	$defaultStylesheetName = "default.css";
	$sitePath = "/Homepage/";
	
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
			header("http/1.0 " . $_SESSION['errorNumber'] . " " . $_SESSION['errorMessage']);
			$pageTitle = "HTTP " . $_SESSION['errorNumber'] . " " . $_SESSION['errorMessage'];
		}
		else
		{
			$pageTitle = "Untitled Page";
		}
	}
	else
	{
		$contents = "error";
		$_SESSION['errorNumber'] = 404;
		$_SESSION['errorMessage'] = "Not Found";
		header("http/1.0 " . $errorNumber . " " . $errorMessage);
		$pageTitle = "HTTP " . $_SESSION['errorNumber'] . " " . $_SESSION['errorMessage'];
	}
	
	require_once('_templates/default.php');	
?>
