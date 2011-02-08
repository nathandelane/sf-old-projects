<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");

/**
 * PeopleRoles
 * This class corresponds to the values in the people_roles table.
 * @author nalane
 *
 */
final class PeopleRoles {
	
	const SYSTEM_ADMINISTRATOR = 1;
	const SERVICE_TECHNICIAN = 2;
	const SYSTEM_MODERATOR = 3;
	const TECHNICIAN = 4;
	const GROUP_MODERATOR = 5;
	const SUPER_USER = 6;
	const CONTENT_MANAGER = 7;
	
}

?>