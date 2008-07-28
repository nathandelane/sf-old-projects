/*
 * Random Password Generator 2
 * Filename: RPG2.java
 * Contents: RPG2 class, this class specifies the entire project, as a random password
 * generator for up to any number of characters.  If more than 36 characters are used,
 * then duplicate characters are allowed, otherwise only one of each individual character
 * is allowed, A-Z and 0-9, in a password, thus making the passwords generator hopefully
 * much stronger against brute force.
 */

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.util.*;

public class RPG2 extends JFrame implements ActionListener {
	
	private static long numChars;
	private char[] gereatedPassword;
	private String[] possibleCharacters = {
		"a", "b", "c", "d", "e", "f",
		"g", "h", "i", "j", "k", "L",
		"m", "n", "o", "p", "q", "r",
		"s", "t", "u", "v", "w", "x",
		"y", "z", "0", "1", "2", "3",
		"4", "5", "6", "7", "8", "9"
	};
	private ArrayList remainingChars;
	
	public RPG2() {
		remainingChars = (ArrayList)Arrays.asList(possibleCharacters);
		
		if(RPG2.numChars < 37) {
			generatePasswordNoReuse();
		} else {
			generatePasswordWithReuse();
		}
	}
	/*
	private void generatePasswordNoReuse() {
		Random r = new Random(Calendar.getInstance().getTimeInMillis());
		for(long l = 0; l < numChars; ++l) {
			int i = (int)(r.nextFloat() * remainingChars.size());
			char c = (char)remainingChars.get(i);
		}
	}
	*/
	private void generatePasswordWithReuse() {
		
	}
	
	public void actionPerformed(ActionEvent ae) {
		
	}
	
	public static void main(String args[]) {
		RPG2.numChars = 8;
		
		if(args.length > 0) {
			try {
				RPG2.numChars = Long.parseLong(args[0]);
			} catch(NumberFormatException nfe) {
				JOptionPane.showMessageDialog(null, nfe.toString(), "Exception caught!", 1);
			}
		}
		
		RPG2 rpg2 = new RPG2();
	}
	
}
