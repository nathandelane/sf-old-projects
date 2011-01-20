<?php

/**
 * IReadable
 * This interface defines an interface of a collection that has readonly functions.
 * @author lanathan
 *
 */
interface IReadable {
	
	/**
	 * get
	 * Gets a specific value from a collection.
	 * @param mixed $key
	 * @return object
	 */
	public function get(/*mixed*/ $key);
	
	/**
	 * getFirstOrDefault
	 * Gets the first or default value of a collection.
	 * @return object
	 */
	public function getFirstOrDefault();
	
}

?>