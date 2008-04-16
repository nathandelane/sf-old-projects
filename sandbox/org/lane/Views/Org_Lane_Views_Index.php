<?php
	require_once("Common/Org_Lane_Views_Common_View.php");
	
	class Org_Lane_Views_Index
	{
		private $_dtd;
		private $_resources;
		
		public function __constructor()
		{
			$_dtd = new Org_Lane_Views_Common_DocType(Org_Lane_Views_Common_DocType::TRANSITIONAL);
			$_resources = array();
			
			$_resources[0] = new Org_Lane_Views_Common_Resource(Org_Lane_Views_Common_Resource::TITLE);
			$_resources[0]->setTextNode("Lil' Nice Website");
		}
		
		public function writeStart()
		{
			if($_dtd)
			{
				$_dtd.write();
			}
			
			Org_Lane_Views_Common_View::startHtml();
			Org_Lane_Views_Common_View::startHead();
			
			if(sizeof($_resource) > 0)
			{
				foreach($_resources as $resource)
				{
					$resource.write();
				}
			}
			
			Org_Lane_Views_Common_View::endHead();
			Org_Lane_Views_Common_View::startBody();
		}
		
		public function writeEnd()
		{
			Org_Lane_Views_Common_View::endBody();
			Org_Lane_Views_Common_View::endHtml();
		}
	}
?>