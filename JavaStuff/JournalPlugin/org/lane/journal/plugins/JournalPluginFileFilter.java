package org.lane.journal.plugins;

import java.io.*;

public class JournalPluginFileFilter implements FilenameFilter{
	
	public JournalPluginFileFilter() {
		
	}
	
	public boolean accept(File file, String name) {
		boolean result = false;
		
		if(file.isDirectory()) {
			result = true;
		}
		
		String fileName = file.getName();
		
		return fileName.endsWith(".jar") || result;
	}

}
