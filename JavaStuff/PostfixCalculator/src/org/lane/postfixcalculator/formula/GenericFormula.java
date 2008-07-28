package org.lane.postfixcalculator.formula;

import java.util.*;

public class GenericFormula {
	
	private static String[] varTable = { "a", "b", "c", "d", "e", "f",
		"g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r",
		"s", "t", "u", "v", "w", "x", "y", "z"
	};
	private Hashtable<String, String> componentList = new Hashtable<String, String>();
	private ArrayList<String> operatorList = new ArrayList<String>();
	private String builtFormula = "";
	private double formulaResult = 0.0;
	
	public String BuiltFormula() {
		return builtFormula;
	}
	
	public int parseFormula(String formula) {
		int result = 0;
		
		char[] formulaBroken = formula.toCharArray();
		
		int leftPerenCount = 0;
		int rightPerenCount = 0;
		int varCount = 0;
		int subCounter = 1;
		String numberString = "";

		for(int i = 0; i < formulaBroken.length; ++i) {	
			String c = "" + formulaBroken[i];
			boolean tokenFlag = false;
			
			if(c.equals("(")) {
				++leftPerenCount;
				builtFormula += "(";
				tokenFlag = false;
			} else
			if(c.equals(")")) {
				++rightPerenCount;
				builtFormula += ")";
				tokenFlag = false;
			} else
			if(c.matches("[\\d.]")) {
				numberString += formulaBroken[i];
				
				while((c = "" + formulaBroken[(i + subCounter)]).matches("[\\d.]")) {
					numberString += c;
					++subCounter;
				}
				
				
				i += (subCounter - 1);
				componentList.put(GenericFormula.varTable[varCount], numberString);
				builtFormula += GenericFormula.varTable[varCount];
				++varCount;
				subCounter = 1;
				numberString = "";
				tokenFlag = false;
			} else
			if(c.matches("[\\s\\t]")) {
				// Discard whitespace
				tokenFlag = false;
			} else
			if(c.matches("[+-/*]")) {
				if(!tokenFlag) {
					operatorList.add(c);
					builtFormula += c;
				} else {
					result = 1;
				}
				
				tokenFlag = true;
			} else {
				result = 1;
			}
		}
		
		return result;
	}
	
	public double getResult() {
		char[] charArray = builtFormula.toCharArray();
		
		formulaResult = Double.parseDouble(componentList.get("a"));
		
		for(int i = 0; i < charArray.length; ++i) {
			if(charArray[i] == 'a') {
				for(int j = (i + 1); j < charArray.length; ++j) {
					if(charArray[j] == '+') {
						++j;
						double d1 = formulaResult;
						double d2 = Double.parseDouble(componentList.get(("" + charArray[j])));
						
						formulaResult = sum(d1, d2);
					} else
					if(charArray[j] == '-') {
						++j;
						double d1 = formulaResult;
						double d2 = Double.parseDouble(componentList.get(("" + charArray[j])));
						
						formulaResult = difference(d1, d2);
					} else
					if(charArray[j] == '/') {
						++j;
						double d1 = formulaResult;
						double d2 = Double.parseDouble(componentList.get(("" + charArray[j])));
						
						formulaResult = divide(d1, d2);
					} else
					if(charArray[j] == '*') {
						++j;
						double d1 = formulaResult;
						double d2 = Double.parseDouble(componentList.get(("" + charArray[j])));
						
						formulaResult = multiply(d1, d2);
					}
				}
			}
		}
		
		return formulaResult;
	}
	
	private double sum(double d1, double d2) {
		return (d1 + d2);
	}

	private double difference(double d1, double d2) {
		return (d1 - d2);
	}

	private double multiply(double d1, double d2) {
		return (d1 * d2);
	}

	private double divide(double d1, double d2) {
		return (d1 / d2);
	}

}
