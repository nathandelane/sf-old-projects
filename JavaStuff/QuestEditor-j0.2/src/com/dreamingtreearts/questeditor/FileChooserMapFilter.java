package com.dreamingtreearts.questeditor;

import java.io.*;
import javax.swing.filechooser.FileFilter;

public class FileChooserMapFilter extends FileFilter {
	
	private static final String map = "dtamap";
	
	public FileChooserMapFilter() {
		
	}
	
	public boolean accept(File f) {
		boolean result = false;
		
		if(f.isDirectory()) {
			return true;
		} else {
			String extension = getExtension(f);
			
			if(extension != null) {
				if(extension.equals(FileChooserMapFilter.map)) {
					result = true;
				}
			}
		}
		
		return result;
	}
	
	private String getExtension(File f) {
		String ext = null;
        String s = f.getName();
        int i = s.lastIndexOf('.');

        if (i > 0 &&  i < s.length() - 1) {
            ext = s.substring(i+1).toLowerCase();
        }
        return ext;
	}

	@Override
	public String getDescription() {
		return "*.dtamap DTA Quest Files";
	}

}
