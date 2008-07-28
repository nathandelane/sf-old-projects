/*
 * Author: Nathan Lane
 * Last Updated: 03/16/2007
 * 
 * This is the main entry point for the program.
 */

package org.lane.journal;

import java.awt.event.*;
import java.util.Enumeration;
import java.util.ArrayList;

import javax.swing.JOptionPane;

import org.lane.journal.plugins.*;

public class JournalKeyEventHandler implements KeyListener {
	
	private JournalFileSaver journalFileSaver;
	private JournalFileOpener journalFileOpener;
	private JournalInstantEncrypter journalInstantEncrypter;
	private Boolean rEncrypted, eEncrypted;
	
	public JournalKeyEventHandler() { 
		journalFileSaver = new JournalFileSaver();
		journalFileOpener = new JournalFileOpener();
		journalInstantEncrypter = new JournalInstantEncrypter();
		rEncrypted = new Boolean(false);
		eEncrypted = new Boolean(false);
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
				toggleEncrypted(eEncrypted);
			} else
			if(ke.getKeyCode() == KeyEvent.VK_R) {
				instantEncryptAll();
				toggleEncrypted(rEncrypted);
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
	
	private void toggleEncrypted(Boolean encryptionFlag) {
		if(encryptionFlag) {
			encryptionFlag = false;
		} else {
			encryptionFlag = true;
		}
	}
	
	private ArrayList<JournalPlugin> checkForSelectedPlugins() {
		ArrayList<JournalPlugin> retVal = new ArrayList<JournalPlugin>();
		
		System.out.println("CheckForSelectedPlugins called");
		Enumeration<String> pluginHashtableEnum = Main.journalPluginReg.pluginHashtable.keys();
		System.out.println("pluginHashtableEnum has " + Main.journalPluginReg.pluginHashtable.size() + " elements");
		while(pluginHashtableEnum.hasMoreElements()) {
			String nextElement = pluginHashtableEnum.nextElement();
			System.out.println("Checking " + nextElement + " plugin button to see if it's selected");
			if(Main.objJournalGUI.pluginButtonsTable.get(nextElement).isSelected()) {
				try {
					JournalPlugin tempPlugin = (JournalPlugin)Main.journalPluginReg.pluginHashtable.get(nextElement);
					retVal.add(tempPlugin);
				} catch(Exception e) {
					e.printStackTrace();
				}
			}
		}
		
		return retVal;
	}
	
	private void saveJournalFile() {
		ArrayList pluginArray = null;
		String originalText = Main.objJournalGUI.getJournalText();
		
		System.out.println("Checking for selected plugins");
		if((pluginArray = checkForSelectedPlugins()).size() > 0) {
			// Find out whether the text is currently encrypted or not
			System.out.println("Is text encrypted e-wise?");
			if(eEncrypted) {
				System.out.println("Yes");
				instantEncrypt();
				toggleEncrypted(eEncrypted);
			}
			
			System.out.println("Is text encrypted r-wise?");
			if(rEncrypted) {
				System.out.println("Yes");
				instantEncryptAll();
				toggleEncrypted(eEncrypted);
			}
			
			// Try to encrypt the text using the plugins
			System.out.println("Trying to encrypt text using plugins: " + pluginArray.size() + " plugins to be used");
			try {
				for(int i = 0; i < pluginArray.size(); ++i) {
					String plainText = Main.objJournalGUI.getJournalText();
					JournalPlugin nextCheckedPlugin = (JournalPlugin)pluginArray.get(i);
					String cypherText = nextCheckedPlugin.encrypt(plainText);
					Main.objJournalGUI.setJournalEditField(cypherText);
				}
			} catch(Exception e) {
				e.printStackTrace();
			}
		}
		
		journalFileSaver.saveFile();
		
		Main.objJournalGUI.setJournalEditField(originalText);
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
