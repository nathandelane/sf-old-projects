/*
 * Author: Nathan Lane
 * Last Updated: 03/16/2007
 * 
 * This is the main entry point for the program.
 */

package org.lane.journal;

import java.awt.event.*;

import javax.swing.JOptionPane;

public class JournalGUIWindowEventHandler implements WindowListener {
	
	int journalCloseResponse;
	
	public JournalGUIWindowEventHandler() {
		journalCloseResponse = JOptionPane.CANCEL_OPTION;
	}
	
	public void windowIconified(WindowEvent we) {}
	public void windowDeiconified(WindowEvent we) {}
	public void windowActivated(WindowEvent we) {}
	public void windowDeactivated(WindowEvent we) {}
	public void windowOpened(WindowEvent we) {}
	public void windowClosed(WindowEvent we) {}
	
	public void windowClosing(WindowEvent we) {
		//if((journalCloseResponse = askAreYouSure("Close Journal")) == JOptionPane.YES_OPTION) {
			we.getWindow().setVisible(false);
			System.exit(0);
		//}
	}
	/*
	private int askAreYouSure(String operation) {
		int result = 0;
		
		result = JOptionPane.showConfirmDialog(Main.objJournalGUI, "Are you want to do this?\n" + operation);
		
		return result;
	}
	*/
}
