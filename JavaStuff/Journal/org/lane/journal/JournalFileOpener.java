/*
 * Author: Nathan Lane
 * Last Updated: 03/16/2007
 * 
 * This is the main entry point for the program.
 */

package org.lane.journal;

import javax.swing.*;

import java.io.*;

public class JournalFileOpener {

	private File journalDirectory;
	private File journalFile;
	private String fileText;
	
	public JournalFileOpener() {
		String userHome = System.getProperty("user.home");
		journalDirectory = new File(userHome);
		journalFile = null;
	}
	
	public int openFile() {
		int response = 0;
		
		JFileChooser jfc = new JFileChooser(journalDirectory);
		jfc.setFileFilter(new JournalFileFilter());
		response = jfc.showOpenDialog(Main.objJournalGUI);
		if(response == JFileChooser.APPROVE_OPTION) {
			journalFile = jfc.getSelectedFile();
			journalDirectory = jfc.getCurrentDirectory();
			if(!readFile()) {
				JOptionPane.showMessageDialog(null, "Could not read file " + journalFile.getAbsolutePath());
			}
		} else {
			// Do nothing, because the operation was cancelled
		}

		return response;
	}
	/*
	public int byteArrayToInt (byte[] bytes) {
		byte[] byteArray = bytes;
		int byteNum = 0;// = (40 - Integer.numberOfLeadingZeros (integer < 0 ? ~integer : integer)) / 8;
		
		for(int i = 0; i < 4; ++i) {
			
		}
		
		return byteNum;
	}
	*/
	public static int byteArrayToInt(byte[] b) {
        int value = 0;
        for (int i = 0; i < 4; i++) {
            int shift = (4 - 1 - i) * 8;
            value += (b[i] & 0x000000FF) << shift;
        }
        return value;
    }
	
	public boolean readFile() {
		boolean result = false;
		
		byte byteArray[] = new byte[(int)journalFile.length()];
		
		try {
			FileInputStream fos = new FileInputStream(journalFile);
			fos.read(byteArray);
			fos.close();
			result = true;
		} catch(FileNotFoundException fnfe) {
			fnfe.printStackTrace();
			result = false;
		} catch(IOException ioe) {
			ioe.printStackTrace();
			result = false;
		}
		
		
		byte hashArray[] = new byte[4];
		for(int i = 0; i < 4; ++i) {
			hashArray[i] = byteArray[i];
		}
		
		JournalInstantEncrypter.setHash(byteArrayToInt(hashArray));
		
		byte byteArrayText[] = new byte[(int)(journalFile.length() - 4)];
		for(int i = 0; i < byteArrayText.length; ++i) {
			byteArrayText[i] = byteArray[(i + 4)];
		}
		
		fileText = new String(byteArrayText);
		
		return result;
	}
	
	public String getFileText() {
		return fileText;
	}

}
