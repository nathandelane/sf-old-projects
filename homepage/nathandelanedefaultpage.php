<?php
	$pageTitles = array(
		"main" => "Home | Nathandelane, software architect, programmer, designer", 
		"kontaktieren" => "Contact Me | Nathandelane", 
		"junkgenerator" => "Junk Generator | Nathandelane", 
		"tvgproject" => "The TVG Project | Nathandelane"
	);
	
	$pageContent = "main";
	$pageTitle = $pageTitles[$pageContent];
	
	if(!$_GET["page"])
	{
		$pageContent = "main";
		$pageTitle = $pageTitles[$pageContent];
	}
	else
	{
		$pageContent = $_GET["page"];
		$pageTitle = $pageTitles[$pageContent];
	}
	
	require_once("_templates/nathandelanedefaulttemplate.php");
?>