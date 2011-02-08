<?php
// This module logs the user out.
session_start();
require_once('info.php');
 
$_SESSION['user_validated'] = false;
header("Location: /accountmanager/");
?>