<?php

/**
 * IHash
 * This class represents a hash collection, which has a key for every value.
 * @author lanathan
 *
 */
interface IHash {
	
	/**
	 * add
	 * Adds an element with a key indicating the location of the value.
	 * @param mixed $key
	 * @param mixed $value
	 */
	public function add(/*mixed*/ $key, /*mixed*/ $value);
	
	/**
	 * get
	 * Gets the value associated with the key.
	 * @param mixed $key
	 * @return void
	 */
	public function get(/*mixed*/ $key);
	
	/**
	 * clear
	 * Clears the contents of the hash.
	 * @return void
	 */
	public function clear();
	
	/**
	 * containsKey
	 * Gets whether this hash contains a certain key.
	 * @param mixed $key
	 * @return bool
	 */
	public function containsKey(/*mixed*/ $key);
	
	/**
	 * remove
	 * Removes the value specified by the key.
	 * @param mixed $key
	 * @return mixed Object being removed.
	 */
	public function remove(/*mixed*/ $key);
	
	/**
	 * tryGetValue
	 * Attempts to get the value associated with the key
	 * @param mixed $key
	 * @param mixed $value By reference. The value found by the key in the hash.
	 * @return bool Whether the value was found using the key.
	 */
	public function tryGetValue(/*mixed*/ $key, /*mixed*/ &$value);
	
	/**
	 * getEnumerator
	 * Gets an enumerator for this hash
	 * @return IEnumerator
	 */
	public function getEnumerator();
	
	/**
	 * size
	 * Gets the current size of the hash.
	 * @return int
	 */
	public function size();
	
	
}

?>