package com.nathandelane.paintchat.business;

import java.io.*;
import java.util.*;

public final class Logger {
	
	private static final String logFileBaseName = "paintchat%1$s.log";
	private static String logFileIdentifier = String.format("%1$s.%2$s.%3$s", Calendar.getInstance().get(Calendar.MONTH), Calendar.getInstance().get(Calendar.DAY_OF_MONTH), Calendar.getInstance().get(Calendar.YEAR));
	
	public static void logMessage(String message) {
		try {
			File file = createFileIfNotExists();
			
			writeMessage(new PrintWriter(file), message);
		} catch(IOException ex) {
			writeMessage(new PrintWriter(System.err), message);
		}
	}
	
	private static void writeMessage(PrintWriter pw, String message) {
		pw.write(message);
		pw.write(System.getProperty("line.separator"));
		pw.flush();
		pw.close();
	}
	
	private static File createFileIfNotExists() throws IOException  {
		File file;
		
		if(!(file = new File(String.format(logFileBaseName, logFileIdentifier))).exists()) {
			file.createNewFile();
		}
		
		return file;
	}

}
