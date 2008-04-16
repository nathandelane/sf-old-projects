<?php
	include("Common/Org_Lane_Views_Common_DocType.php");
	include("Common/Org_Lane_Views_Common_Resource.php");
	
	class Org_Lane_Views_Common_View
	{
		public static function startHtml()
		{
			echo "<html>\n";
		}
		
		public static function endHtml()
		{
			echo "</html>";
		}
		
		public static function startHead()
		{
			echo "<head>";
		}
		
		public static function endHead()
		{
			echo "</head>";
		}
		
		public static function startBody()
		{
			echo "<body>";
		}
		
		public static function endBody()
		{
			echo "</body>";
		}
	}
?>