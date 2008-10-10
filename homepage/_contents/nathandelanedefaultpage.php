<?php
session_start();
$_SESSION['errorNumber'] = 403;
$_SESSION['errorMessage'] = "Access Forbidden";
header("Location: /?page=error");
?>