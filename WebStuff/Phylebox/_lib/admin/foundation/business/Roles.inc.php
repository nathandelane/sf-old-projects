<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");

/**
 * Roles
 * This class corresponds to the values in the roles table.
 * @author nalane
 *
 */
final class Roles {
	
	const SYSTEM_ADMINISTRATOR = 1;
	const SERVICE_TECHNICIAN = 2;
	const SYSTEM_MODERATOR = 3;
	const TECHNICIAN = 4;
	const GROUP_MODERATOR = 5;
	const SUPER_USER = 6;
	const CONTENT_MANAGER = 7;
	
}

?>