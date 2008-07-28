/*
 * Author: Nathan Lane
 * Last Updated: 03/16/2007
 * 
 * This is the main entry point for the program.
 */

package org.lane.journal;

import java.awt.event.*;
import javax.swing.JOptionPane;

public class JournalEditFieldKeyEventHandler implements KeyListener {
	
	private JournalFileSaver journalFileSaver;
	private JournalFileOpener journalFileOpener;
	private JournalInstantEncrypter journalInstantEncrypter;
	
	public JournalEditFieldKeyEventHandler() { 
		journalFileSaver = new JournalFileSaver();
		journalFileOpener = new JournalFileOpener();
		journalInstantEncrypter = new JournalInstantEncrypter();
	}
	
	public void keyPressed(KeyEvent ke) {
		// Ctrl + Alt + Key
		if(ke.getModifiersEx() == KeyEvent.CTRL_DOWN_MASK + KeyEvent.ALT_DOWN_MASK) {
			if(ke.getKeyCode() == KeyEvent.VK_C) {
				Main.objJournalGUI.centerJournalGUI();
			} else
			if(ke.getKeyCode() == KeyEvent.VK_A) {
				journalFileSaver.resetFile();
				saveJournalFile();
			}
		} else if(ke.getModifiersEx() == KeyEvent.CTRL_DOWN_MASK) { // Ctrl + Key
			if(ke.getKeyCode() == KeyEvent.VK_S) {
				saveJournalFile();
			} else
			if(ke.getKeyCode() == KeyEvent.VK_E) {
				instantEncrypt();
			} else
			if(ke.getKeyCode() == KeyEvent.VK_R) {
				instantEncryptAll();
			} else
			if(ke.getKeyCode() == KeyEvent.VK_W) {
				requestNewQuickHash();
			} else
			if(ke.getKeyCode() == KeyEvent.VK_K || ke.getKeyCode() == KeyEvent.VK_N) {
				cleanUpJournal();
			} else
			if(ke.getKeyCode() == KeyEvent.VK_O) {
				openJournalFile();
			} 
		}
	}
	
	public void keyReleased(KeyEvent ke) { }
	public void keyTyped(KeyEvent ke) { }
	
	private void saveJournalFile() {
		journalFileSaver.saveFile();
	}
	
	private void openJournalFile() {
		journalFileOpener.openFile();
		Main.objJournalGUI.setJournalEditField(journalFileOpener.getFileText());
	}
	
	private void instantEncrypt() {
		String plainText = Main.objJournalGUI.getJournalText();
		if(plainText != "") {
			String cypherText = journalInstantEncrypter.quickEncrypt(plainText);
			Main.objJournalGUI.setJournalEditField(cypherText);
		}
	}

	private void instantEncryptAll() {
		String plainText = Main.objJournalGUI.getJournalText();
		if(plainText != "") {
			String cypherText = journalInstantEncrypter.quickEncryptAll(plainText);
			Main.objJournalGUI.setJournalEditField(cypherText);
		}
	}
	
	private void requestNewQuickHash() {
		journalInstantEncrypter.requestNewHash();
	}
	
	private void cleanUpJournal() {
		if(askAreYouSure("Clear Journal") == JOptionPane.YES_OPTION) {
			journalFileSaver.resetFile();
			Main.objJournalGUI.setJournalEditField("");
			JOptionPane.showMessageDialog(Main.objJournalGUI, "Journal was cleared");
		} else {
			JOptionPane.showMessageDialog(Main.objJournalGUI, "Journal clear was aborted");
		}
	}
	
	private int askAreYouSure(String operation) {
		int result = 0;
		
		result = JOptionPane.showConfirmDialog(Main.objJournalGUI, "Are you want to do this?\n" + operation);
		
		return result;
	}

}
