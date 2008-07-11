<?php
	$pageContent = "main";
	
	if(!$_GET["page"])
	{
		$pageContent = "main";
	}
	else
	{
		$pageContent = $_GET["page"];
	}
	
	require_once("_templates/nathandelanedefaulttemplate.php");
?>