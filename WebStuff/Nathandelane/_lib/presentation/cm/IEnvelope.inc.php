<?php

/**
 * IEnvelope
 * Base interface for all content envelopes.
 * @author lanathan
 *
 */
interface IEnvelope {
	
	/**
	 * getID
	 * Gets the envelope's ID.
	 * @return int
	 */
	public function getId();
	
	/**
	 * getStatus
	 * Gets the envelope's status.
	 * @return int
	 */
	public function getStatus();
	
	/**
	 * getOwner
	 * Gets the envelope's owner ID.
	 * @return int
	 */
	public function getOwner();
	
	/**
	 * getDateCreated
	 * Gets the date that the envelope was created.
	 * @return DateTime
	 */
	public function getDateCreated();
	
	/**
	 * getRevisionNumber
	 * Gets this envelope's revision number.
	 * @return int
	 */
	public function getRevisionNumber();
	
}

?>