package org.lane.postfixcalculator.text;

import java.util.*;

public class Parser {
	
	private ArrayList<String> functionTokens;
	private ArrayList<String> amountTokens;
	
	public Parser() { }
	
	public double parseTokens(String[] args) {
		double result = 0;
		functionTokens = new ArrayList<String>();
		amountTokens = new ArrayList<String>();
		
		for(int i = 0; i < args.length; ++i) {
			if(args[i].equals("/") || args[i].equals("*") || args[i].equals("-") || args[i].equals("+")) {
				functionTokens.add(args[i]);
			} else {
				if(args[i].toLowerCase().equals("pi")) {
					args[i] = "" + Math.PI;
				}
					
				amountTokens.add(args[i]);
			}
		}
		
		result = Double.parseDouble(amountTokens.get(0));
		for(int i = 0; i < functionTokens.size(); ++i) {
		
			double amount = 0;
			amount = Double.parseDouble(amountTokens.get(i + 1));
			
			if(functionTokens.get(i).equals("/")) {
				result = result / amount;
			} else
			if(functionTokens.get(i).equals("*")) {
				result = result * amount;
			} else
			if(functionTokens.get(i).equals("-")) {
				result = result - amount;
			} else
			if(functionTokens.get(i).equals("+")) {
				result = result + amount;
			}
		}
		
		return result;
	}

}
