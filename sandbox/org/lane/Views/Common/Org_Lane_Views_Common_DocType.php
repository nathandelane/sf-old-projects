<?php
	class Org_Lane_Views_Common_DocType
	{
		const TRANSITIONAL = "Transitional";
		const STRICT = "Strict";
		
		private $_type;
		
		public function __constructor($type = Org_Lane_Views_DocType::TRANSITIONAL)
		{
			if(empty($type))
			{
				throw new Exception("\$type cannot be empty");
			}
			else if(is_string($type))
			{
				$_type = $type;
			}
			else
			{
				throw new Exception("Unrecognized type for \$type");
			}
		}
		
		public function write()
		{
			echo $this->toString() . "\n";
		}
		
		public function toString()
		{
			$str = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 {$_type}//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-" . strtolower($_type) . ".dtd\">";
				
			return $str;
		}
		
		public function isTransitional()
		{
			$value = false;
			
			if($_type == Org_Lane_Views_DocType::TRANSITIONAL)
			{
				$value = true;
			}
			
			return $value;
		}
		
		public function isStrict()
		{
			$value = false;
			
			if($_type == Org_Lane_Views_DocType::STRICT)
			{
				$value = true;
			}
			
			return $value;
		}
	}
?>