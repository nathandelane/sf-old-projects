package org.lane.postfixcalculator;

import org.lane.postfixcalculator.text.*;
import org.lane.postfixcalculator.formula.*;
import java.io.*;

public class Main {

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		String[] tokens;
		boolean flag = false;
		
		while(true) {
			String input = "";
			
			BufferedReader br = new BufferedReader(new InputStreamReader(System.in));
			
			try {
				input = br.readLine();
			} catch(IOException e) {
				e.printStackTrace();
			}
			
			if(input.equals("q")) {
				break;
			} else
			if(input.equals("<")) {
				flag = false;
				continue;
			} else
			if(input.equals(">")) {
				flag = true;
				continue;
			}
			
			tokens = input.split(" ");
			
			try {
				GenericFormula f = new GenericFormula();
				
				if(flag) {
					if(f.parseFormula(combine(tokens)) == 0) {
						//System.out.println("\n" + f.BuiltFormula());
						System.out.println(f.getResult() + "\n");
					} else {
						System.out.println("\n" + "error." + "\n");
					}
				} else {
					System.out.println("\n" + (new Parser()).parseTokens(tokens) + "\n");					
				}
			} catch(Exception e) {
				System.out.println("\nexception.\n");
			}
		}			
	}
	
	private static String combine(String[] array) {
		String result = "";
		
		for(int i = 0; i < array.length; ++i) {
			result += array[i] + " ";
		}
		
		return result;
	}

}
