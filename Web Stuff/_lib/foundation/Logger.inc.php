<?php

require_once(dirname(__FILE__) . "/../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/Environment.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/LogStatus.inc.php");

/**
 * Logger
 * This class represents a static logger.
 * @author lanathan
 *
 */
final class Logger {
	
	private static $__instance;
	
	/**
	 * Constructor
	 * This is private for the singleton in it.
	 * @return Logger
	 */
	private function Logger() {
		// TODO: Whatever needs to be done
	}
	
	/**
	 * logMessage
	 * Sends a message with a status to the error log.
	 * @param int $status Use _lib/foundation/LogStatus
	 * @param string $message
	 */
	public function sendMessage(/*int*/ $status, /*string*/ $message) {
		ArgumentTypeValidator::isString($message, "Message must be a string.");
		
		$logMessage = null;
		
		if(Config::isDebugEnvironment()) {
			$logMessage = sprintf('[Debug:%1$s] %2$s%3$s', $this->_levelToString($loggingLevel), $this->_normalizeMessage($message), Environment::getNewLine());
		} else {
			$logMessage = sprintf('[%1$s] %2$s%3$s', $this->_levelToString($loggingLevel), $this->_normalizeMessage($message), Environment::getNewLine());
		}

		if($loggingLevel <= $permittedLevel && $logMessage) {
			error_log($logMessage);
		}
	}
	
	/**
	 * logException
	 * Logs an exception to the error log.
	 * @param Exception $exception
	 */
	public function logException(Exception $exception) {
		$this->sendMessage(LogStatus::NOTICE, sprintf('Message: %1$s, File: $2$s, Line %3$s', $exception->getMessage(), $exception->getFile(), $exception->getLine()));
	}
	
	/**
	 * getInstance
	 * Returns the static instance of Logger.
	 * @return Logger
	 */
	public static function getInstance() {
		if (!isset(self::$__instance)) {
			self::$__instance = new Logger();
		}
		
		return self::$__instance;
	}

	/**
	 * _normalizeMessage
	 * Replaces \r and \n with \\r and \\n text.
	 * @param string $message
	 * @return string
	 */
	private function _normalizeMessage(/*string*/ $message) {
		ArgumentTypeValidator::isString($message, "Message must be a string.");
		
		$newMessage = str_replace("\r", "\\r", $message);
		$newMessage = str_replace("\n", "\\n", $message);
		
		return $newMessage;
	}
	
	/**
	 * _levelToString
	 * Converts logging level to string.
	 * @param int $loggingLevel
	 */
	private function _levelToString(/*int*/ $loggingLevel) {
		$strLoggingLevel = "UNKNOWN";
		
		if($loggingLevel == LogStatus::DEBUG) {
			$strLoggingLevel = "DEBUG";
		} else if($loggingLevel == LogStatus::INFO) {
			$strLoggingLevel = "INFO";
		} else if($loggingLevel == LogStatus::NOTICE) {
			$strLoggingLevel = "NOTICE";
		} else if($loggingLevel == LogStatus::WARNING) {
			$strLoggingLevel = "WARNING";
		} else if($loggingLevel == LogStatus::ERROR) {
			$strLoggingLevel = "ERROR";
		} else if($loggingLevel == LogStatus::CRITICAL) {
			$strLoggingLevel = "CRITICAL";
		} else if($loggingLevel == LogStatus::ALERT) {
			$strLoggingLevel = "ALERT";
		} else if($loggingLevel == LogStatus::EMERGENCY) {
			$strLoggingLevel = "EMERGENCY";
		}
								
		return $strLoggingLevel;
	}
	
}

?>