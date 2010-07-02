package com.nathandelane.mud.server;

import java.io.*;
import java.util.*;

/**
 * Singleton Logger class for logging messages.
 * @author lanathan
 *
 */
public class Logger implements ILogger {
	
	private static Logger instance;
	
	private PrintStream accessPrintStream;
	private PrintStream errorPrintStream;
	
	/**
	 * @see com.nathandelane.mud.server.ILogger
	 */
	public void sendMessage(MessageType messageType, String message) {
		String dateTime = Calendar.getInstance().getTime().toString();
		
		this.errorPrintStream.format("[%s] %s %s", dateTime, messageType.toString(), message);
	}
	
	/**
	 * @see com.nathandelane.mud.server.ILogger
	 * @param String message
	 */
	public void sendAccessMessage(String message) {
		String dateTime = Calendar.getInstance().getTime().toString();
		
		this.accessPrintStream.format("[%s] %s", dateTime, message);
	}
	
	/**
	 * Default constructor is private to enforce Singleton pattern.
	 * One Logger for everyone.
	 * @return Logger
	 */
	private Logger() {
		this.accessPrintStream = System.out;
		this.errorPrintStream = System.err;
	}
	
	public static Logger getInstance() {
		if (Logger.instance == null) {
			Logger.instance = new Logger();
		}
		
		return Logger.instance;
	}

}
