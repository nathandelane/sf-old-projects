<?php

require_once("PHPUnit/Framework.php");
require_once(dirname(__FILE__) . "/../Config.inc.php");
require_once(LibUnitTests_Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");

class Ndl_Foundation_ArgumentTypeValidatorTests extends PHPUnit_Framework_TestCase {
	
	public function testIsString() {
		$string = "This is a string.";
		
		$this->assertTrue(ArgumentTypeValidator::isString($string, ""));
	}
	
	/**
	 * @expectedException InvalidArgumentException
	 */
	public function testIsStringWithNumber() {
		$nonString = 1234;
		
		$this->assertTrue(ArgumentTypeValidator::isString($nonString, ""));
	}
	
}

?>