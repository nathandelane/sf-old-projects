<?php
	require_once("org/lane/Views/Org_Lane_Views_Index.php");
	
	if(class_exists(Org_Lane_Views_Index))
	{
		$indexPage = new Org_Lane_Views_Index();
		$indexPage->writeStart();
	}
	else
	{
		echo "Could not find class named Org_Lane_Views_Index";
	}
?>