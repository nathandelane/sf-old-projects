<?php

/**
 * IEnumerator
 * This interface represents an enumerator for a collection.
 * @author lanathan
 *
 */
interface IEnumerator {
	
	/**
	 * Gets whether there is a next element and moves the cursor to that element.
	 * @return bool
	 */
	public function moveNext();
	
	/**
	 * Resets the cursor.
	 * @return void
	 */
	public function reset();
	
}

?>