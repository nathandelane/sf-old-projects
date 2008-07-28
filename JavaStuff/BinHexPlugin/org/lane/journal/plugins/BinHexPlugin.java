package org.lane.journal.plugins;

public class BinHexPlugin extends JournalPlugin {
	
	private char allowedValues[] = { 'A', 'B', 'C', 'D', 'E', 'F', 'G',
									 'H', 'I', 'J', 'K', 'L', 'M', 'N',
									 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
									 'V', 'W', 'X', 'Y', 'Z' };
	
	public BinHexPlugin() {
		// Default constructor
	}
	
	public String encrypt(String plainText) {
		String result = "";
		
		// TODO: fill in "BinHex" code here
		// Strip unwanted characters
		result = removePunctuation(plainText);
		// Make all of the characters upper case
		result = result.toUpperCase();
		// Encrypt the string
		result = convertToBinHex(result);
		
		return result;
	}
	
	private String removePunctuation(String plainText) {
		String text = "";
		char textInAnArray[] = plainText.toCharArray();
		char newString[] = new char[textInAnArray.length];
		
		int newIndex = 0;
		for(int i = 0; i < textInAnArray.length; ++i) {
			if(!(textInAnArray[i] == '.') &&
			   !(textInAnArray[i] == ',') &&
			   !(textInAnArray[i] == ' ') &&
			   !(textInAnArray[i] == '\t') &&
			   !(textInAnArray[i] == '!') &&
			   !(textInAnArray[i] == '\n') &&
			   !(textInAnArray[i] == '\r') &&
			   !(textInAnArray[i] == '?')) 
			{
				
				newString[newIndex] = textInAnArray[i];
				++newIndex;
			}
					
		}
		
		text = new String(newString, 0, newIndex);
		
		return text;
	}
	
	private String convertToBinHex(String plainText) {
		String result = "";
		char resultArray[] = plainText.toCharArray();
		
		for(int i = 0; i < resultArray.length; ++i) {
			for(int n = 0; n < allowedValues.length; ++n) {
				if(resultArray[i] == allowedValues[n]) {
					result = result + charToBinHex(n);
				}
			}
		}
		
		return result;
	}
	
	private String charToBinHex(Integer c) {
		String str = "";
		
		String binStr = Integer.toBinaryString(c);
		char binArr[] = binStr.toCharArray();
		for(int i = 0; i < binArr.length; ++i) {
			if(binArr[i] == '1') {
				str = str + (allowedValues[i]);
			}
		}
		
		return str;
	}

}
