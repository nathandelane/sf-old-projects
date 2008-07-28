/*
 * Author: Nathan Lane
 * Last Updated: 03/16/2007
 * 
 * This is the main entry point for the program.
 */

package org.lane.journal;

import org.lane.journal.plugins.*;

import javax.swing.JToggleButton;
import java.util.Enumeration;
import java.awt.Font;

public class Main {
	
	public static JournalGUI objJournalGUI;
	public static JournalPluginRegistry journalPluginReg;	
	
	public Main() {
		objJournalGUI = new JournalGUI();
		journalPluginReg = new JournalPluginRegistry();
		System.out.println("Plugins found: " + journalPluginReg.pluginHashtable.size());
		setupPluginsInterface();
	}
	
	public void run_main() {
		objJournalGUI.setVisible(true);
	}
	
	private void setupPluginsInterface() {
		Enumeration<String> pluginHashtableEnum = journalPluginReg.pluginHashtable.keys();
		while(pluginHashtableEnum.hasMoreElements()) {
			String nextElement = pluginHashtableEnum.nextElement();
			System.out.println("Adding button for " + nextElement);
			JToggleButton jtButton = new JToggleButton(nextElement);
			objJournalGUI.getJournalPluginPanel().add(jtButton);
			objJournalGUI.pluginButtonsTable.put(nextElement, jtButton);
			jtButton.setToolTipText(nextElement);
			jtButton.setFocusPainted(false);
			jtButton.setFont(new Font("Sans Serif", Font.PLAIN, 8));
		}
	}
	
	public static void main(String args[]) {
		Main main = new Main();
		main.run_main();
	}

}
