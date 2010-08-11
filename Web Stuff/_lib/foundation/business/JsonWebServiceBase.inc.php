<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/Logger.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/Strings.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");

/**
 * JsonWebServiceBase
 * This class is used by all web services (which must be hosted) as a base class. It provides a dispatcher for multiple-method webservice architecture.
 * @author lanathan
 *
 */
abstract class JsonWebServiceBase {
	
	protected $_logger;
	
	private $_serviceProvider;
	private $_registeredServices;
	
	/**
	 * Default constructor for JsonWebServiceBase.
	 * @param string $serviceProvider Name of the service provider class.
	 * @return JsonWebServiceBase
	 */
	protected function JsonWebServiceBase(/*string*/ $serviceProvider) {
		ArgumentTypeValidator::isString($serviceProvider, "ServiceProvider must be a string.");
		
		$this->_logger = Logger::getInstance();
		$this->_serviceProvider = $serviceProvider;
		$this->_registeredServices = array();
	}
	
	/**
	 * registerServiceMethod
	 * Used to register a particular service method for a given service.
	 * @param string $name
	 * @param array $expectedArguments
	 * @param string $description
	 */
	protected function registerServiceMethod(/*string*/ $name, array $expectedArguments, /*string*/ $description) {
		ArgumentTypeValidator::isString($name, "Name must be a string.");
		ArgumentTypeValidator::isString($description, "Description must be a string.");
		
		if (!in_array($name, array_keys($this->_registeredServices))) {
			$serviceDescription = array($expectedArguments, $description);
			
			$this->_registeredServices[$name] = $serviceDescription;
		}
	}
	
	/**
	 * echoJson
	 * Echos the json string that has been created before hand.
	 * @param string $jsonString
	 * @return void
	 */
	protected function echoJson(/*string*/ $jsonString) {
		ArgumentTypeValidator::isString($jsonString, "JsonString must be a string.");
		
		header('Content-Type: application/json');
		 
		echo "$jsonString"; 
	}
	
	/**
	 * echoJsonError
	 * Echos an error back to the client.
	 * @param string $error
	 * @param string $description
	 * @return void
	 */
	protected function echoJsonError(/*string*/ $error, /*string*/ $description) {
		ArgumentTypeValidator::isString($error, "Error must be a string.");
		ArgumentTypeValidator::isString($description, "Description must be a string.");
		
		$this->echoJson(json_encode(array("messageType" => "error", "error" => $error, "description" => $description)));
	}
	
	/**
	 * This is the default behavior of the web service. If no method is supplied, then this appears.
	 * @return {void}
	 */
	protected function listMethods() {
		
?>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en" dir=ltr">
	<head>
		<title><?php echo "$this->_serviceProvider"; ?> | Methods</title>
		<link rel="icon" type="image/png" href="/favicon.png" />
		<link rel="stylesheet" type="text/css" href="/_css/services.css" />
	</head>
	<body>
		<h1><?php echo "$this->_serviceProvider"; ?> Methods</h1>
		<ul>
	
<?php

		foreach ($this->_registeredServices as $name => $details) {

?>

			<li>
				<h2><?php echo "$name"; ?></h2>
				<h3>Description</h3>
				<p><?php echo "$details[1]"; ?></p>
				<h3>Required Parameters (as JSON)</h3>
				<ul>
					
<?php

			foreach ($details[0] as $arg) {

?>

					<li><?php echo "$arg"; ?></li>

<?php

			}

?>
					
				</ul>
			</li>

<?php

		}

?>

		</ul>	
	</body>
</html>

<?php
		
	}
	
	/**
	 * dispatch
	 * Used to dispatch requests.
	 * @return void
	 */
	public function dispatch(/*object*/ $object) {
		$params = $_GET;
		
		if (count($params) > 0) {
			foreach ($params as $key => $value) {
				if (Strings::isNullOrEmpty($value)) {
					$methodName = $key;
					$methodArguments = json_decode(file_get_contents("php://input"));
					$methodCallable = is_callable(array($object, $methodName));
					
					$this->_logger->sendMessage(LOG_NOTICE, sprintf('Method name: %1$s, callable: %2$s, arguments: %3$s.', $methodName, ($methodCallable ? "true" : "false"), json_encode($methodArguments)));
					
					if ($methodCallable) {
						call_user_func(array($object, $methodName), $methodArguments);
					} else {
						$this->listMethods();
						break;
					}
				}
			}
		} else {
			$this->listMethods();
		}
	}
	
}

?>