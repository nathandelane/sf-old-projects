package org.lane.personalcalculator.collections;

import java.util.*;

import org.lane.personalcalculator.*;

public class TokenList {
	
	private ArrayList<ExpressionToken> tokens;
	private boolean containsFloatingPoint;
	
	private TokenList() {
		// Do not instantiate this publicly
		tokens = new ArrayList<ExpressionToken>();
	}
	
	public static TokenList parseExpression(String expression) {
		TokenList tokenList = new TokenList();
		
		if(!expression.equals("")) {
			tokenList.tokenizeExpression(expression);
		}
		
		return tokenList;
	}
	
	public void setIsFloatingPoint(boolean isFloatingPoint) {
		containsFloatingPoint = isFloatingPoint;
	}
	
	public boolean isFloatingPoint() {
		return containsFloatingPoint;
	}
	
	public boolean add(ExpressionToken token) {
		return tokens.add(token);
	}
	
	public boolean contains(ExpressionToken token) {
		return tokens.contains(token);
	}
	
	public ExpressionToken get(int index) {
		return tokens.get(index);
	}
	
	public int indexOf(ExpressionToken token) {
		return tokens.indexOf(token);
	}
	
	public void push(ExpressionToken token) {
		if(tokens.size() > 0) {
			tokens.add(tokens.get(tokens.size() - 1));
			for(int i = (tokens.size() - 1); i > 0; i--) {
				tokens.set(i, tokens.get(i - 1));
			}
			
			tokens.set(0, token);
		} else {
			tokens.add(token);
		}
	}
	
	public ExpressionToken remove(int index) {
		return tokens.remove(index);
	}
	
	public int size() {
		return tokens.size();
	}
	
	public ExpressionToken set(int index, ExpressionToken token) {
		return tokens.set(index, token);
	}
	
	public ExpressionToken[] toArray() {
		ExpressionToken[] result = new ExpressionToken[size()];
		
		for(int i = 0; i < size(); i++) {
			result[i] = get(i);
		}
		
		return result;
	}
	
	/**
	 * Token-ize the expression such that tokens contains each token as an ExpressionToken object.
	 * @param expression
	 */
	private void tokenizeExpression(String expression) {
		containsFloatingPoint = false;
		char[] tokenBits = expression.toLowerCase().toCharArray();
		int lastTokenType = ExpressionToken.VoidToken;
		boolean nextNumberIsNegative = false;
		
		for(int i = 0; i < tokenBits.length; i++) {
			String ch = "" + tokenBits[i];

			if(ch.equals(".")) {
				containsFloatingPoint = true;
			}
			
			if(ch.matches("[\\d.,]")) {
				String token = ch;
				int j = i + 1;
				
				while(j < tokenBits.length && (ch = "" + tokenBits[j]).matches("[\\d.,]")) {
					if(ch.equals(".")) {
						containsFloatingPoint = true;
					}
					
					if(!ch.equals(",")) {
						token += ch;
					}
					
					j++;
				}
				
				if(nextNumberIsNegative) {
					if(containsFloatingPoint) {
						double d = Double.parseDouble(token) * (-1);
						token = "" + d;
					} else {
						int n = Integer.parseInt(token) * (-1);
						token = "" + n;
					}
					
					nextNumberIsNegative = false;
				}
				
				tokens.add(new ExpressionToken(token, ExpressionToken.Number));
				lastTokenType = ExpressionToken.Number;
				i = (j - 1);
			} else if(ch.matches("[mpelcstMPELCST]")) {
				String token = ch;
				int j = i + 1;
				
				while(j < tokenBits.length && (ch = "" + tokenBits[j]).matches("[A-za-z\\d]")) {
					token += ch;
					j++;
				}
				
				if(token.equals("trace")) {
					PCState.trace = true;
				} else if(token.equals("pi")) {
					tokens.add(new ExpressionToken("" + Math.PI, ExpressionToken.Number));
					lastTokenType = ExpressionToken.Number;
				} else if(token.equals("e")) {
					tokens.add(new ExpressionToken("" + Math.E, ExpressionToken.Number));
					lastTokenType = ExpressionToken.Number;
				} else {
					tokens.add(new ExpressionToken(token, ExpressionToken.Function));
					lastTokenType = ExpressionToken.Function;
				}
				
				i = (j - 1);
			} else if(ch.matches("[+]{1}")) {
				String token = ch;
				
				tokens.add(new ExpressionToken(token, ExpressionToken.AddOperator));
				lastTokenType = ExpressionToken.AddOperator;
			} else if(ch.matches("[-~]{1}")) {				
				if(lastTokenType != ExpressionToken.Number) {
					nextNumberIsNegative = true;
				} else {
					String token = ch;
					nextNumberIsNegative = false;
					tokens.add(new ExpressionToken(token, ExpressionToken.NegationOperator));
				}
				
				lastTokenType = ExpressionToken.NegationOperator;
			} else if(ch.matches("[*]{1}")) {
				String token = ch;
				
				tokens.add(new ExpressionToken(token, ExpressionToken.MultiplicationOperator));
				lastTokenType = ExpressionToken.MultiplicationOperator;
			} else if(ch.matches("[/]{1}")) {
				String token = ch;
				
				tokens.add(new ExpressionToken(token, ExpressionToken.DivisionOperator));
				lastTokenType = ExpressionToken.DivisionOperator;
			} else if(ch.matches("[(]{1}")) {
				String token = ch;
				
				tokens.add(new ExpressionToken(token, ExpressionToken.OpenParenthesis));
				lastTokenType = ExpressionToken.OpenParenthesis;
			} else if(ch.matches("[)]{1}")) {
				String token = ch;
				
				tokens.add(new ExpressionToken(token, ExpressionToken.CloseParenthesis));
				lastTokenType = ExpressionToken.CloseParenthesis;
			} else if(ch.matches("[\\^]")) {
				String token = ch;
				
				tokens.add(new ExpressionToken(token, ExpressionToken.PowerOperator));
				lastTokenType = ExpressionToken.PowerOperator;
			} else if(ch.matches("[\\$]")) {
				String token = ch;
				tokens.add(new ExpressionToken(token, ExpressionToken.LastResult));
				lastTokenType = ExpressionToken.LastResult;
			} else if(ch.matches("[!]")) {
				String token = ch;
				
				tokens.add(new ExpressionToken(token, ExpressionToken.Factorial));
				lastTokenType = ExpressionToken.Factorial;
			} else if(ch.matches("[A-za-z]")) {
				String token = ch;
				int j = i + 1;
				
				while(j < tokenBits.length && (ch = "" + tokenBits[j]).matches("[A-za-z]")) {
					token += ch;
					j++;
				}
				
				tokens.add(new ExpressionToken(token, ExpressionToken.Function));
				lastTokenType = ExpressionToken.Function;
				i = (j - 1);
			}
		}
	}

}
