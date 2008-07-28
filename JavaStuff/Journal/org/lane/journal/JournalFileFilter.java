/*
 * Author: Nathan Lane
 * Last Updated: 03/16/2007
 * 
 * This is the main entry point for the program.
 */

package org.lane.journal;

import java.io.File;
import javax.swing.filechooser.FileFilter;

public class JournalFileFilter extends FileFilter {
	
	public JournalFileFilter() {
		// Default constructor
	}
	
	public boolean accept(File file) {
		boolean result = false;
		
		if(file.isDirectory()) {
			result = true;
		}
		
		String fileName = file.getName();
		
		return fileName.endsWith(".jnl") || result;
	}
	
	public String getDescription() {
		return "*.jnl";
	}

}
