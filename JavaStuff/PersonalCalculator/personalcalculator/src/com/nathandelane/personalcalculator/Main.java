package com.nathandelane.personalcalculator;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;

import com.nathandelane.math.evaluation.*;
import com.nathandelane.math.evaluation.tokens.DivisionByZeroException;
import com.nathandelane.math.evaluation.tokens.InvalidTokenException;
import com.nathandelane.math.evaluation.tokens.NotImplementedException;

public class Main {
	
	private Main() {
		String expression = "";
		BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(System.in));
		
		while(true) {			
			try {
				System.out.print("> ");
				expression = (bufferedReader.readLine()).toLowerCase();
				
				if(expression.startsWith("q")) {
					break;
				} else {
					Expression result = Expression.postfixate(Expression.parse(expression));
					String evaluation = result.evaluate();
					
					System.out.println(String.format("%1$s\n", evaluation));
				}
			} catch (IOException e) {
				e.printStackTrace();
			} catch (InvalidTokenException e) {
				System.err.println(String.format("The expression parser found an invalid token. Message: %1$s", e.getMessage()));
				e.printStackTrace();
			} catch (TokenTranspositionException e) {
				System.err.println(String.format("A token was transposed during postfix formatting. Message: %1$s", e.getMessage()));
				e.printStackTrace();
			} catch (DivisionByZeroException e) {
				System.err.println(String.format("Division by zero results in undefined."));
				e.printStackTrace();
			} catch (IllegalStateException e) {
				System.err.println(String.format("The expression parser found an invalid token. Message: %1$s", e.getMessage()));
				e.printStackTrace();
			} catch (NotImplementedException e) {
				System.err.println(String.format("There was an error because a method is being used that is not yet implemented. Please contact the developer(s)."));
				e.printStackTrace();
			}
		}
		
		System.out.println("Thank you for using Personal Calculator (pc).");
	}

	public static void main(String[] args) {
		if(args.length > 0) {
			String expression = join(args, "");
			try {
				Expression result = Expression.postfixate(Expression.parse(expression));
				String evaluation = result.evaluate();
				
				System.out.println(String.format("%1$s\n", evaluation));
			} catch (InvalidTokenException e) {
				System.err.println(String.format("The expression parser found an invalid token. Message: %1$s", e.getMessage()));
				e.printStackTrace();
			} catch (TokenTranspositionException e) {
				System.err.println(String.format("A token was transposed during postfix formatting. Message: %1$s", e.getMessage()));
				e.printStackTrace();
			} catch (DivisionByZeroException e) {
				System.err.println(String.format("Division by zero results in undefined."));
				e.printStackTrace();
			} catch (IllegalStateException e) {
				System.err.println(String.format("The expression parser found an invalid token. Message: %1$s", e.getMessage()));
				e.printStackTrace();
			} catch (NotImplementedException e) {
				System.err.println(String.format("There was an error because a method is being used that is not yet implemented. Please contact the developer(s)."));
				e.printStackTrace();
			}
		} else {
			new Main();
		}
	}
	
	private static String join(String[] array, String delimiter) {
		StringBuilder sb = new StringBuilder();
		
		sb = sb.append(array[0]);
		for(int arrayIndex = 1; arrayIndex < array.length; arrayIndex++) {
			sb = sb.append(array[arrayIndex]);
			sb.append(delimiter);
		}
		
		return sb.toString();
	}

}
