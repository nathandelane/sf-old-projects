<?php

/**
 * ICollection
 * This interface represents a collection of objects.
 * @author lanathan
 *
 */
interface ICollection {
	
	/**
	 * add
	 * Adds an element with a key indicating the location of the value.
	 * @param mixed $object
	 */
	public function add(/*mixed*/ $object);
	
	/**
	 * get
	 * Gets the value associated with the index.
	 * @param int $index
	 * @return void
	 */
	public function get(/*int*/ $index);
	
	/**
	 * clear
	 * Clears the contents of the hash.
	 * @return void
	 */
	public function clear();
	
	/**
	 * containsKey
	 * Gets whether this hash contains a certain key.
	 * @param mixed $object
	 * @return bool
	 */
	public function contains(/*mixed*/ $object);
	
	/**
	 * remove
	 * Removes the value specified by the index.
	 * @param int $object
	 * @return mixed Object being removed.
	 */
	public function removeAt(/*mixed*/ $index);
	
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