package org.lane.personalcalculator;

import java.io.*;

import org.lane.personalcalculator.collections.*;
import org.lane.personalcalculator.util.*;

public class Main {
	
	private boolean runProgramLoop;
	private BufferedReader reader;
	//private BufferedWriter writer;
	
	public Main() {
		runProgramLoop = true;
		reader = new BufferedReader(new InputStreamReader(System.in));
		//writer = new BufferedWriter(new OutputStreamWriter(System.out));
		
		while(runProgramLoop) {
			PCState.trace = false;
			String result = "0.0";
			
			try {
				System.out.print("> ");
				String userInput = reader.readLine();
				PCState.lastToken = userInput;
				
				if(!userInput.equalsIgnoreCase("q") && !userInput.equalsIgnoreCase("?")) {
					TokenList tokens = TokenList.parseExpression(userInput);
					
					if(tokens.size() > 0) {
						result = Evaluate.evaluateExpression(Convert.postfixate(tokens));
						
						if(PCState.setVariable) {
							System.out.println(PCState.currentVarName + " = " + result + System.getProperty("line.separator"));
							PCState.setVariable = false;
						} else {
							System.out.println("" + result + System.getProperty("line.separator"));
						}
					}
				} else if(userInput.equalsIgnoreCase("?")) {
					System.out.println("Help is here" + System.getProperty("line.separator") + PCState.availableCommands + System.getProperty("line.separator"));
				} else if(userInput.equalsIgnoreCase("q")) {
					runProgramLoop = false;
				}
			} catch(Exception e) {
				PCState.error = -1;
				
				if(e instanceof NumberFormatException) {
					System.out.println("Could not convert " + PCState.lastToken + " to a number.");
				} else {
					System.out.println("Error occurred: " + e.toString());
				}
			}
		}
	}
	
	public static void main(String[] args) {
		new Main();
	}
	
}
