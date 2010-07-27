<?php

require_once(dirname(__FILE__) . "/../../Config.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/Logger.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/collections/ICollection.inc.php");
require_once(Config::getFrameworkRoot() . "foundation/collections/IEnumerator.inc.php");

/**
 * ArrayList
 * This class represents an indexed collection.
 * @author lanathan
 *
 */
class ArrayList implements ICollection {
	
	protected $_logger;
	
	private $_collection;
	
	/**
	 * Constructor
	 * @return ArrayList
	 */
	public function ArrayList() {
		$this->_collection = array();
		$this->_logger = Logger::getInstance();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/ICollection::add()
	 */
	public function add($object) {
		$this->_logger->sendMessage(LOG_DEBUG, sprintf('Adding script: %1$s, ArrayList', $object));
		
		$this->_collection[] = $object;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/ICollection::get()
	 */
	public function get($index) {
		$result = null;
		
		if (!is_int($index)) {
			throw new InvalidArgumentException("Index must be an integer");
		}
		
		if ($index < ($this->size() - 1)) {
			$result = $this->_collection[$index];
		} else {
			throw new OutOfBoundsException("Array index was out of bounds on ArrayList.");
		}
		
		return $result;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/ICollection::clear()
	 */
	public function clear() {
		$this->_collection = array();
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/ICollection::contains()
	 */
	public function contains($object) {
		return in_array($object, $this->_collection);
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/ICollection::removeAt()
	 */
	public function removeAt($index) {
		if (!is_int($index)) {
			throw new InvalidArgumentException("Index must be an integer");
		}
		
		if ($index < ($this->size() - 1)) {
			unset($this->_collection[$index]);
		} else {
			throw new OutOfBoundsException("Array index was out of bounds on ArrayList.");
		}
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/ICollection::getEnumerator()
	 */
	public function getEnumerator() {
		$enumerator = new CollectionEnumerator($this->_collection);
		
		return $enumerator;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/ICollection::size()
	 */
	public function size() {
		return count($this->_collection);
	}
	
	/**
	 * toArray
	 * Gets a native array version of the ArrayList.
	 * @return array
	 */
	public function toArray() {
		return $this->_collection;
	}
	
}


/**
 * CollectionEnumerator
 * This class represents an enumerator for a HashCollection object.
 * @author lanathan
 *
 */
final class CollectionEnumerator implements IEnumerator {
	
	private $_collection;
	private $_cursor;
	private $_currentItem;
	
	/**
	 * Constructor
	 * @param array $collection
	 * @return CollectionEnumerator
	 */
	public function CollectionEnumerator(array $collection) {
		$this->_collection = $collection;
		$this->_cursor = 0;
	}
	
	/**
	 * (non-PHPdoc)
	 * @see _lib/foundation/collections/IEnumerator::moveNext()
	 */
	public function moveNext() {
		$result = false;
		
		if ($this->_cursor < count($this->_collection)) {
			$result = true;
			
			$this->_currentItem = $this->_collection[$this->_cursor];
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
	 * getNextItem
	 * Gets the next item where the cursor is.
	 * @return mixed
	 */
	public function getNextItem() {
		return $this->_currentItem;
	}
	
}

?>