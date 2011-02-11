<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/Logger.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/collections/IHash.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/collections/IEnumerator.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/collections/IEnumerable.inc.php");

/**
 * HashCollection
 * This class represents a hash collection.
 * @author lanathan
 *
 */
class HashCollection implements IHash, IEnumerable {
	
	protected $_logger;
	
	private $_collection;
	
	/**
	 * Constructor
	 * @param array $hash
	 * @return HashCollection
	 */
	public function HashCollection(array $hash = array()) {
		$this->_collection = $hash;
		$this->_logger = Logger::getInstance();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IHash::add()
	 */
	public function add($key, $value) {
		if (is_array($value)) {
			$this->_logger->sendMessage(LOG_DEBUG, sprintf('Key: %1$s, Value: %2$s, foundation/collections/HashCollection', $key, http_build_query($value)));
		} else {
			$this->_logger->sendMessage(LOG_DEBUG, sprintf('Key: %1$s, Value: %2$s, foundation/collections/HashCollection', $key, $value));
		}
		
		if ($this->containsKey($key)) {
			throw new Exception("HashCollection already contains key $key.");
		}
		
		$this->_collection[$key] = $value;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IHash::get()
	 */
	public function get($key) {
		$result = null;
		
		if ($this->containsKey($key)) {
			$result = $this->_collection[$key];
		}
		
		return $result;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IHash::clear()
	 */
	public function clear() {
		$this->_collection = array();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IHash::containsKey()
	 */
	public function containsKey($key) {
		return array_key_exists($key, $this->_collection);
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IHash::remove()
	 */
	public function remove($key) {
		$result = null;
		
		if ($this->containsKey($key)) {
			$result = $this->get($key);
			
			unset($this->_collection[$key]);
		}
		
		return $result;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IHash::tryGetValue()
	 */
	public function tryGetValue($key, &$value) {
		$value = null;
		$result = false;
		
		if ($this->containsKey($key)) {
			$value = $this->get($key);
			$result = true;
		}
		
		return $result;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IEnumerable::getEnumerator()
	 */
	public function getEnumerator() {
		$enumerator = new HashCollectionEnumerator($this->_collection);
		
		return $enumerator;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IHash::size()
	 */
	public function size() {
		return count($this->_collection);
	}
	
	/**
	 * toHash
	 * Gets a native hash of this HashCollection.
	 * @return array
	 */
	public function toHash() {
		return $this->_collection;
	}
	
}

/**
 * HashCollectionEnumerator
 * This class represents an enumerator for a HashCollection object.
 * @author lanathan
 *
 */
final class HashCollectionEnumerator implements IEnumerator {
	
	private $_hash;
	private $_keys;
	private $_cursor;
	private $_currentKey;
	private $_currentValue;
	private $_logger;
	
	/**
	 * Constructor
	 * @param array $hash
	 * @return HashCollectionEnumerator
	 */
	public function HashCollectionEnumerator(array $hash) {
		$this->_hash = $hash;
		$this->_keys = array_keys($this->_hash);
		$this->_cursor = 0;
		$this->_logger = Logger::getInstance();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IEnumerator::moveNext()
	 */
	public function moveNext() {
		$result = false;
		
		if ($this->_cursor < (count($this->_keys))) {
			$result = true;
			
			$this->_currentKey = $this->_keys[$this->_cursor];
			$this->_currentValue = $this->_hash[$this->_currentKey];
			$this->_cursor++;
		}
		
		return $result;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IEnumerator::reset()
	 */
	public function reset() {
		$this->_cursor = 0;
	}
	
	/**
	 * getNextKey
	 * Gets the next key where the cursor is.
	 * @return mixed
	 */
	public function getNextKey() {
		return $this->_currentKey;
	}
	
	/**
	 * getNextValue
	 * Gets the next value where the cursor is.
	 * @return mixed
	 */
	public function getNextValue() {
		return $this->_currentValue;
	}
	
}

?>