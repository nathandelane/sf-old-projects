<?php
	/**
	 * Package: BlueZam
	 * Class: PhpSession
	 * Description: This class takes care of basic user session recording and user path logging.
	 * Author: Nathan Lane
	 * Date: 02.05.2008
	 * @package BlueZam
	 */
	
	class BlueZam_PhpSession
	{
		private $_sessionId;
		private $_currentPage;
		private $_sessionPageSequence;
		
		public function __construct()
		{
			session_start();
			$this->_sessionId = session_id();
			$this->_sessionPageSequence = array();
		}
		
		public function getSessionId()
		{
			return $this->_sessionId;
		}
		
		public function setCurrentPage($currentPage)
		{
			$this->_currentPage = $currentPage;
			array_push($this->_sessionPageSequence, $currentPage);
		}
		
		public function getCurrentPage()
		{
			return $this->_currentPage;
		}
	}
?>