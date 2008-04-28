<?php
	class PhpSession
	{
		private $_sessionId;
		private $_currentPage;
		private $_sessionPageSequence;
		
		public function __construct()
		{
			session_start();
			$_sessionId = session_id();
			$_sessionPageSequence = array();
		}
		
		public function getSessionId()
		{
			return $_sessionId;
		}
		
		public function setCurrentPage($currentPage)
		{
			$_currentPage = $currentPage;
			array_push($_sessionPageSequence, $currentPage);
		}
		
		public function getCurrentPage()
		{
			return $_currentPage;
		}
	}
?>