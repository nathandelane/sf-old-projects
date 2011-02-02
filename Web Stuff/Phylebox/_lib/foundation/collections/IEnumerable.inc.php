<?php

/**
 * IEnumerable
 * Anything that is enumerable or that can have an enumerator should implement this interface.
 * @author lanathan
 *
 */
interface IEnumerable {
	
	/**
	 * getEnumerator
	 * Gets an IEnumerator object for a collection.
	 * @return IEnumerator
	 */
	public function getEnumerator();
	
}