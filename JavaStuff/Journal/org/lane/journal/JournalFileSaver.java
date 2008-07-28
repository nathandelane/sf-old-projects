/*
 * Author: Nathan Lane
 * Last Updated: 03/16/2007
 * 
 * This is the main entry point for the program.
 */

package org.lane.journal;

import javax.swing.*;
import java.io.*;

public class JournalFileSaver {
	
	private File journalDirectory;
	private File journalFile;
	
	public JournalFileSaver() {
		String userHome = System.getProperty("user.home");
		journalDirectory = new File(userHome);
		journalFile = null;
	}
	
	public File resetFile() {
		journalFile = null;
		
		return journalFile;
	}
	
	public int saveFile() {
		int response = 0;
		String text = Main.objJournalGUI.getJournalText();
		
		if(journalFile == null) {
			JFileChooser jfc = new JFileChooser(journalDirectory);
			jfc.setFileFilter(new JournalFileFilter());
			response = jfc.showSaveDialog(Main.objJournalGUI);
			if(response == JFileChooser.APPROVE_OPTION) {
				journalFile = jfc.getSelectedFile();
				journalDirectory = jfc.getCurrentDirectory();
				if(!writeFile(text)) {
					JOptionPane.showMessageDialog(null, "Could not write file " + journalFile.getAbsolutePath());
				}
			} else {
				// Do nothing, because the operation was cancelled
			}
		} else {
			if(!writeFile(text)) {
				JOptionPane.showMessageDialog(null, "Could not write file " + journalFile.getAbsolutePath());
			}
		}

		return response;
	}
	
	public byte[] intToByteArray (int integer) {
		int byteNum = (40 - Integer.numberOfLeadingZeros (integer < 0 ? ~integer : integer)) / 8;
		byte[] byteArray = new byte[4];
		
		for (int n = 0; n < byteNum; n++)
			byteArray[3 - n] = (byte) (integer >>> (n * 8));
		
		return (byteArray);
	}
	
	public boolean writeFile(String text) {
		int encryptionHash = JournalInstantEncrypter.getHash();
		boolean result = false;
		byte hashArray[] = intToByteArray(encryptionHash);
		byte byteArray[] = new byte[(text.length() + hashArray.length)];
		
		// Add the hash to the array
		for(int i = 0; i < hashArray.length; ++i) {
			byteArray[i] = hashArray[i];
		}
		
		// Add the text to the array
		for(int i = 0; i < text.length(); ++i) {
			byteArray[(i + hashArray.length)] = (text.getBytes())[i];
		}
		
		try {
			String journalPath = journalFile.getAbsolutePath();
			if(journalPath.indexOf(".jnl") == -1) {
				journalPath += ".jnl";
				journalFile = new File(journalPath);
			}
			
			FileOutputStream fos = new FileOutputStream(journalFile);
			fos.write(byteArray);
			fos.close();
			result = true;
		} catch(FileNotFoundException fnfe) {
			fnfe.printStackTrace();
			result = false;
		} catch(IOException ioe) {
			ioe.printStackTrace();
			result = false;
		}
		
		return result;
	}

}
