<?php

if(!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../../_lib/phyle-box/Config.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(PhyleBox_Config::getFrameworkRoot() . "presentation/AuthenticationPage.inc.php");

if(isset($_SESSION[AuthenticationPage::AUTHENTICATION_KEY]) && Strings::equals($_SESSION[AuthenticationPage::AUTHENTICATION_KEY], session_id())) {
	session_destroy();
}

header("Location: " . PhyleBox_Config::getPhyleBoxRoot() . "/login.php");

?>