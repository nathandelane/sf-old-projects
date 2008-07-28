package org.lane.journal.plugins;

import java.util.*;
import java.util.jar.*;
import java.util.zip.*;
import java.io.*;

import org.lane.journal.common.*;

public class JournalPluginRegistry {
	
	public Hashtable pluginHashtable;
	
	private File pluginsDirectory;
	private JournalPluginFileFilter pluginFileFilter;
	private JarClassLoader pluginClassLoader;

	public JournalPluginRegistry() {
		String pluginsDirPath = System.getProperty("user.dir") + File.separator + "plugins";
		System.out.println("Plugin path: " + pluginsDirPath);
		pluginFileFilter = new JournalPluginFileFilter();
		pluginsDirectory = new File(pluginsDirPath);
		
		pluginHashtable = getAllPlugins();
	}
	
	private Hashtable getAllPlugins() {
		System.out.println("Getting all plugins");
		// Setup and get started
		Hashtable<String, JournalPlugin> tempPluginHash = new Hashtable<String, JournalPlugin>();
		
		// Search for any files, only jars allowed for now
		//System.out.println("Checking for plugins... " + pluginsDirectory.getAbsolutePath());
		if(pluginsDirectory.exists() && pluginsDirectory.isDirectory()) {
			String pluginFiles[] = pluginsDirectory.list(pluginFileFilter);
			
			for(int i = 0; i < pluginFiles.length; ++i) {
				try {
					//System.out.println("Plugin found: " + pluginFiles[i]);
					String pluginPath = pluginsDirectory + File.separator + pluginFiles[i];
					JarFile pluginJar = new JarFile(pluginPath);
					Enumeration pluginEntries =  pluginJar.entries();
					
					while(pluginEntries.hasMoreElements()) {
						String entryName = null;
						
						JarEntry jarEntry = (JarEntry)pluginEntries.nextElement();
						entryName = jarEntry.getName();
						if(entryName.indexOf(".class") != -1) {
							String className = ((entryName.split(".class"))[0]).replaceAll("/", ".");
							System.out.println("Class Name: " + className);
							pluginClassLoader = new JarClassLoader(pluginPath);
							Class plugin = pluginClassLoader.loadClass(className, true);
							Object journalPluginObject = plugin.newInstance();
							if(journalPluginObject instanceof JournalPlugin) {
								JournalPlugin jp = (JournalPlugin)journalPluginObject;
								tempPluginHash.put(className, jp);
							}
						}
					}
				} catch(ZipException ze) {
					ze.printStackTrace();
				} catch(IOException ioe) {
					ioe.printStackTrace();
				} catch(ClassNotFoundException cnfe) {
					cnfe.printStackTrace();
				} catch(IllegalAccessException iae) {
					iae.printStackTrace();
				} catch(InstantiationException ie) {
					ie.printStackTrace();
				}
			}
		}
		
		return tempPluginHash;
	}
	
}
