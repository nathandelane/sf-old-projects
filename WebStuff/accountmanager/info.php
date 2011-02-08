<?php
session_start();

if(empty($_SESSION['user_validated']))
{
	session_register('user_validated');
	$_SESSION['user_validated'] = false;
}

$debug = true;
$errorOccurred = false;
$successful = false;
$usersRoot = "C:/accountmanager/user";
$adminRoot = "C:/accountmanager/user/admin";
$dataIndex = "C:/accountmanager/data";
$version = "0.1.0";
$msg = "";
$salt = "69331119-4628-4090-9515-1FA2F915BC09";
?>