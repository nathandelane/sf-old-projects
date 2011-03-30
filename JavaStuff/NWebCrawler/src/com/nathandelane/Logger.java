package com.nathandelane;

import java.io.*;
import java.util.*;

public final class Logger {
	
	private static Logger instance;
	
	private Map<String, OutputStream> outputStreams;
	
	/**
	 * Creates an instance of Logger.
	 */
	private Logger() {
		this.outputStreams = new HashMap<String, OutputStream>();
	}
	
	/**
	 * Gets a reference to the instance of Logger.
	 * @return
	 */
	public static Logger getLogger() {
		if (Logger.instance == null) {
			Logger.instance = new Logger();
		}
		
		return Logger.instance;
	}
	
	/**
	 * Logs a message to all streams. If no streams exist, then logs to the default System.err.
	 * @param logMessageType
	 * @param message
	 */
	private void logMessage(LogMessageType logMessageType, String message) {
		String formattedMessage = String.format("[%1$s] %2$s", logMessageType, message);
		
		if (outputStreams.isEmpty()) {
			System.err.println(formattedMessage);
		} else {
			for (OutputStream nextStream : outputStreams.values()) {
				if (nextStream != null) {
					try {
						nextStream.write(formattedMessage.getBytes());
					} catch(IOException ioe) {
						ioe.printStackTrace();
					}
				}
			}
		}
	}
	
	/**
	 * Adds a named stream to the stream collection.
	 * @param name
	 * @param stream
	 */
	public void addStream(String name, OutputStream stream) {
		outputStreams.put(name, stream);
	}
	
	/**
	 * Removes a named stream if it exists.
	 * @param name
	 * @return The removed stream or null if it did not exist.
	 */
	public OutputStream removeStream(String name) {
		OutputStream stream = outputStreams.get(name);
		
		if (stream != null) {
			outputStreams.remove(name);
		}
		
		return stream;
	}
	
	/**
	 * Logs an information message.
	 * @param message
	 */
	public void info(String message) {
		logMessage(LogMessageType.Info, message);
	}
	
	/**
	 * Logs a debug message.
	 * @param message
	 */
	public void debug(String message) {
		logMessage(LogMessageType.Debug, message);
	}
	
	/**
	 * Logs a warn message.
	 * @param message
	 */
	public void warn(String message) {
		logMessage(LogMessageType.Warn, message);
	}
	
	/**
	 * Logs an error message.
	 * @param message
	 */
	public void error(String message) {
		logMessage(LogMessageType.Error, message);
	}
	
	/**
	 * Logs a fatal message.
	 * @param message
	 */
	public void fatal(String message) {
		logMessage(LogMessageType.Fatal, message);
	}

}
