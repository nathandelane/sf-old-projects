package com.nathandelane.mud.server;

public interface ILogger {
	
	/**
	 * Sends a message to the error log.
	 * @param messageType
	 * @param message
	 */
	public void sendMessage(MessageType messageType, String message);
	
	/**
	 * Sends a message to the access log.
	 * @param message
	 */
	public void sendAccessMessage(String message);

}
