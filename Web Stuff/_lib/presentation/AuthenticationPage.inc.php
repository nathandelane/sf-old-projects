<?php

if(!session_id()) {
	session_start();
}

require_once(dirname(__FILE__) . "/../Config.inc.php");
require_once(Config::getFrameworkRoot() . "presentation/Page.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/ArgumentTypeValidator.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/Strings.inc.php");

/**
 * This page represents a login or other type of authentication page.
 * @author lanathan
 *
 */
abstract class AuthenticationPage extends Page {
	
	const AUTHENTICATION_KEY = "ticket";
	const REFERRER_KEY = "referrer";
	const USERNAME_KEY = "userName";
	const PASSWORD_KEY = "password";
	
	protected $_salt;
	protected $_redirectUrl;
	protected $_authenticationErrorExists;
	
	/**
	 * Creates an instance of AuthenticationPage
	 * @param string $title
	 * @param string $redirectUrl
	 * @param string $salt
	 * @return AuthenticationPage
	 */
	protected function AuthenticationPage(/*string*/ $title, /*string*/ $redirectUrl, /*string*/ $salt) {
		parent::__construct($title);
		
		ArgumentTypeValidator::isString($redirectUrl, "RedirectUrl must be a string.");
		ArgumentTypeValidator::isString($salt, "Salt must be a string.");
		
		$this->_salt = $salt;
		$this->_redirectUrl = $redirectUrl;
		$this->_authenticationErrorExists = false;
		
		if($this->_userAuthenticationIsDefined()) {
			$this->setSessionFieldValue(AuthenticationPage::AUTHENTICATION_KEY, $this->tryAuthentication($this->getFieldValue(AuthenticationPage::USERNAME_KEY), $this->getFieldValue(AuthenticationPage::PASSWORD_KEY)));
			$this->setSessionFieldValue(AuthenticationPage::USERNAME_KEY, $this->getFieldValue(AuthenticationPage::USERNAME_KEY));
		}
		
		if($this->getSessionFieldValue(AuthenticationPage::AUTHENTICATION_KEY) && (StringExtensions::equals($this->getSessionFieldValue(AuthenticationPage::AUTHENTICATION_KEY), session_id()))) {
			$url = "http://" . $_SERVER["SERVER_NAME"] . $this->_redirectUrl;
			
			header("Location: $url");
		}
	}
	
	/**
	 * authenticationErrorExists
	 * Gets whether an authentication error exists.
	 * @return bool
	 */
	public function authenticationErrorExists() {
		return $this->_authenticationErrorExists;
	}
	
	/**
	 * tryAuthentication
	 * Attempts to authenticate a user. Upon success returns a new ticket value.
	 * @param string $userName
	 * @param string $password
	 * @return string
	 */
	public function tryAuthentication(/*string*/ $userName, /*string*/ $password) {
		ArgumentTypeValidator::isString($userName, "UserName must be a string.");
		ArgumentTypeValidator::isString($password, "Password must be a string.");
		//TODO:fix this
		$ticket = null;
		$password .= $this->_salt;
		$encryptedPassword = $this->_encryptPassword($password);
		$credentials = array("user" => "$userName", "pass" => "$encryptedPassword");		
		
		$rows = self::$usersTable->selectObjects($credentials);
		
		if (count($rows) > 0) {
			$ticket = session_id();
		} else {
			$this->_authenticationErrorExists = true;
		}
		
		return $ticket;
	}

	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/Page::openDocument()
	 */
	public function openDocument() {
		parent::openDocument();
	}

	/**
	 * (non-PHPdoc)
	 * @see _lib/presentation/Page::closeDocument()
	 */
	public function closeDocument() {
		parent::closeDocument();
	}
	
	/**
	 * _encryptPassword
	 * Encrypts the password for the database. May be used as a hook method by inheriting classes.
	 * Default is md5.
	 * @param string $password
	 * @return string Encrypted password.
	 */
	protected function _encryptPassword(/*string*/ $password) {
		ArgumentTypeValidator::isString($password, "Password must be a string.");
		
		$encryptedPassword = md5($password);
		
		return $encryptedPassword;
	}
	
	/**
	 * _userAuthenticationIsDefined
	 * Gets whether user authentication was defined in the form post body.
	 * @return bool
	 */
	private function _userAuthenticationIsDefined() {
		$result = false;
		
		if ($this->getFieldValue(AuthenticationPage::USERNAME_KEY) && $this->getFieldValue(AuthenticationPage::PASSWORD_KEY)) {
			$result = true;
		}
		
		return $result;
	}
	
}

?>