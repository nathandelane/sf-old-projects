<?php
	class Org_Lane_Views_Common_Resource
	{
		const LINK = "link";
		const SCRIPT = "script";
		const META = "meta";
		const TITLE = "title";
		
		private $_resourceType;
		private $_attributes;
		private $_textNode;
		
		public function __constructor($resourceType)
		{
			$_resourceType = $resourceType;
			$_attributes = array();
		}
		
		public function addAttribute($name, $value)
		{
			if(is_string($name) && is_string($value))
			{
				if(!array_key_exists($name, $_attributes))
				{
					$_attributes[$name] = $value;
				}
				else
				{
					throw new Exception("Attribute {$name} already exists in the current context.");
				}
			}
			else
			{
				throw new Exception("\$name and \$value must both be strings.");
			}
		}
		
		public function removeAttribute($name)
		{
			if(is_string($name))
			{
				if(array_key_exists($name, $_attributes))
				{
					unset($_attributes[$name]);
				}
				else
				{
					throw new Exception("Attribute {$name} does not exist in the current context.");
				}
			}
			else
			{
				throw new Exception("\$name must be a string.");
			}
		}
		
		public function attributeExists($name)
		{
			$value = false;
			
			if(is_string($name))
			{
				if(array_key_exists($name, $_attributes))
				{
					$value = true;
				}
			}
			else
			{
				throw new Exception("\$name must be a string.");
			}
			
			return $value;
		}
		
		public function setTextNode($text)
		{
			if($_resourceType == Org_Lane_Views_Common_Resource::TITLE)
			{
				if(is_string($text))
				{
					$_textNode = $text;
				}
				else
				{
					throw new Exception("\$text must be a string.");
				}
			}
		}
		
		public function write()
		{
			echo $this->toString() . "\n";
		}
		
		public function toString()
		{
			$str = "<{$_resourceType} ";

			foreach($_attributes as $key => $value)
			{
				$str = "{$str} {$key}";
			}
			
			if($_resourceType == Org_Lane_Views_Common_Resource::LINK)
			{
				$str = "{$str} />";
			}
			else if($_resourceType == Org_Lane_Views_Common_Resource::META)
			{
				$str = "{$str}>";
			}
			else if($_resourceType == Org_Lane_Views_Common_Resource::SCRIPT)
			{
				$str = "{$str}></script>";
			}
			else if($_resourceType == Org_Lane_Views_Common_Resource::TITLE)
			{
				$str = "{$str}>{$_textNode}</title>"
			}
			
			return $str;
		}
	}
?>