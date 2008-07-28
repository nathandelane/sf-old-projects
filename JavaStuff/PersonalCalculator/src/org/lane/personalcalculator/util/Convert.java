package org.lane.personalcalculator.util;

import org.lane.personalcalculator.*;
import org.lane.personalcalculator.collections.*;

public class Convert {
	
	private Convert() {
		
	}
	
	public static TokenList postfixate(TokenList tokens) {		
		TokenList postfixTokens = TokenList.parseExpression("");
		
		if(tokens.isFloatingPoint()) {
			postfixTokens.setIsFloatingPoint(true);
		}
		
		TokenList operatorStack = TokenList.parseExpression("");
		
		for(int i = 0; i < tokens.size(); i++) {
			if(tokens.get(i).getTokenType() == ExpressionToken.Number) {
				postfixTokens.add(tokens.get(i));
			} else if(tokens.get(i).getTokenType() == ExpressionToken.AddOperator || tokens.get(i).getTokenType() == ExpressionToken.NegationOperator) {
				if(operatorStack.size() > 0) {
					int tokenType = operatorStack.get(operatorStack.size() - 1).getTokenType();
					
					if(tokenType == ExpressionToken.AddOperator || tokenType == ExpressionToken.NegationOperator || tokenType == ExpressionToken.DivisionOperator || tokenType == ExpressionToken.MultiplicationOperator || tokenType == ExpressionToken.Function) {
						postfixTokens.add(operatorStack.remove(operatorStack.size() - 1));
					}
				}
				
				operatorStack.add(tokens.get(i));
			} else if(tokens.get(i).getTokenType() == ExpressionToken.MultiplicationOperator || tokens.get(i).getTokenType() == ExpressionToken.DivisionOperator || tokens.get(i).getTokenType() == ExpressionToken.PowerOperator) {
				if(operatorStack.size() > 0) {
					int tokenType = operatorStack.get(operatorStack.size() - 1).getTokenType();
					
					if(tokenType == ExpressionToken.MultiplicationOperator || tokenType == ExpressionToken.DivisionOperator || tokenType == ExpressionToken.PowerOperator || tokenType == ExpressionToken.Function || tokenType == ExpressionToken.Factorial) {
						postfixTokens.add(operatorStack.remove(operatorStack.size() - 1));
					}					
				}
				
				operatorStack.add(tokens.get(i));
			} else if(tokens.get(i).getTokenType() == ExpressionToken.OpenParenthesis) {
				operatorStack.add(tokens.get(i));
			} else if(tokens.get(i).getTokenType() == ExpressionToken.CloseParenthesis) {
				int j = operatorStack.size() - 1;
				
				while(!(operatorStack.get(j).getTokenType() == ExpressionToken.OpenParenthesis)) {
					postfixTokens.add(operatorStack.remove(j));
					j--;
				}
				
				// Remove the open parenthesis
				operatorStack.remove(j);
			} else if(tokens.get(i).getTokenType() == ExpressionToken.Function ) {			
				operatorStack.add(tokens.get(i));
			} else if(tokens.get(i).getTokenType() == ExpressionToken.LastResult) {
				tokens.set(i, new ExpressionToken(PCState.lastResult, ExpressionToken.Number));
				postfixTokens.add(tokens.get(i));
			} else if(tokens.get(i).getTokenType() == ExpressionToken.Factorial) {
				postfixTokens.add(tokens.get(i));
			}/* else if(tokens.get(i).getTokenType() == ExpressionToken.Variable) {
				String varName = tokens.get(i).getToken().split("=")[0];				
				PCState.vars.put(varName, "0.0");
				PCState.currentVarName = varName;
				PCState.setVariable = true;
				postfixTokens.add(tokens.get(i));
			}*/
		}
		
		if(operatorStack.size() > 0) {
			for(int i = operatorStack.size(); i > 0 ; i--) {
				postfixTokens.add(operatorStack.get(i - 1));
			}
		}
		
		return postfixTokens;
	}

}
