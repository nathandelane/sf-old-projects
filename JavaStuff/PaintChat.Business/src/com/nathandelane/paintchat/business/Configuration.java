package com.nathandelane.paintchat.business;

import java.io.*;
import java.util.*;

public final class Configuration extends Properties {

	private static final long serialVersionUID = 1L;
	private static final String configurationFile = "/paintchat.properties";
	
	public Configuration() {
		LoadConfiguration();
	}
	
	private void LoadConfiguration() 	{
		try {
			this.loadFromXML(new FileInputStream(Configuration.configurationFile));
		} catch(FileNotFoundException ex) {
			
		} catch(IOException ex) {
			
		}
	}

}
