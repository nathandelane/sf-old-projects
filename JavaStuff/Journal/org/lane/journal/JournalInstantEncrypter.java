/*
 * Author: Nathan Lane
 * Last Updated: 03/16/2007
 * 
 * This is the main entry point for the program.
 */

package org.lane.journal;

import java.util.Calendar;

public class JournalInstantEncrypter {
	
	private static int currentEncryptionHash;
	private static int lastEncryptionHash;
	
	public JournalInstantEncrypter() {
		currentEncryptionHash = requestNewHash();
	}
	
	public int requestNewHash() {
		Calendar cal = Calendar.getInstance();
		
		lastEncryptionHash = currentEncryptionHash;
		currentEncryptionHash = cal.hashCode(); 
		
		return currentEncryptionHash;
	}
	
	public static int getHash() {
		return currentEncryptionHash;
	}
	
	public static int setHash(int hash) {
		currentEncryptionHash = hash;
		
		return currentEncryptionHash;
	}
	
	public int revertToOldHash() {		
		currentEncryptionHash = lastEncryptionHash;
		
		return currentEncryptionHash;
	}
	
	public String quickEncrypt(String plainText) {
		String cypherText = "";
		byte bytesPlain[] = plainText.getBytes();
		byte bytesCypher[] = new byte[(bytesPlain.length)];
		int intCounter = 0;
		
		do {
			bytesCypher[intCounter] = xor(bytesPlain[intCounter], (byte)currentEncryptionHash);
			
			++intCounter;
		} while(intCounter < bytesPlain.length);
		cypherText = new String(bytesCypher);
		
		return cypherText;
	}
	
	public String quickEncryptAll(String plainText) {
		String cypherText = "";
		String encryptionHash = "" + currentEncryptionHash;
		
		cypherText = xor(plainText, encryptionHash);
		
		return cypherText;
	}
	
	public byte xor(byte plainVal, byte hashVal) {
		byte result = 0;
		
		result = (byte)((int)plainVal ^ (int)hashVal);
		
		return result;
	}
	
	public String xor(String plainText, String hashText) {
		String result = "";
		byte plainArray[] = plainText.getBytes();
		byte cypherArray[] = new byte[plainArray.length];
		byte hashArray[] = hashText.getBytes();
		int intPlainCounter = 0;
		int intHashCounter = 0;
		
		do {
			cypherArray[intPlainCounter] = (byte)((int)plainArray[intPlainCounter] ^ (int)hashArray[intHashCounter]);
			
			++intPlainCounter;
			++intHashCounter;
			if(intHashCounter >= hashArray.length) {
				intHashCounter = 0;
			}
		} while(intPlainCounter < plainArray.length);
		result = new String(cypherArray);
		
		return result;
	}

}
