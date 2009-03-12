package com.nathandelane.math.evaluation.tokens;

import com.nathandelane.math.evaluation.PostfixationAction;

public class TokenPrecedence {

	private static int __enumCount = 0;
	
	private int _value;
	private String _name;

	private TokenPrecedence(String name) {
		_name = name;
		_value = __enumCount;
		__enumCount++;
	}
	
	public String toString() {
		return _name;
	}
	
	public int toInt() {
		return _value;
	}
	
	public PostfixationAction compareTo(TokenPrecedence precedence) {
		PostfixationAction result = PostfixationAction.PUSH;
		
		if(precedence == TokenPrecedence.OPEN_PERENTHESIS) {
			result = PostfixationAction.STACK;
		} else {
			if(this == TokenPrecedence.ADDITION_SUBTRACTION) {
				if(precedence == this) {
					result = PostfixationAction.STACK;
				} else if(precedence == TokenPrecedence.FUNCTION) {
					result = PostfixationAction.POP;
				} else if(precedence == TokenPrecedence.MULTIPLICATION_DIVISION) {
					result = PostfixationAction.POP;
				}
			} else if(this == TokenPrecedence.CLOSE_PERENTHESIS) {
				result = PostfixationAction.POP;
			} else if(this == TokenPrecedence.FUNCTION) {
				if(precedence == this) {
					result = PostfixationAction.POP;
				} else if(precedence == TokenPrecedence.ADDITION_SUBTRACTION) {
					result = PostfixationAction.STACK;
				} else if(precedence == TokenPrecedence.MULTIPLICATION_DIVISION) {
					result = PostfixationAction.STACK;
				}
			} else if(this == TokenPrecedence.MULTIPLICATION_DIVISION) {
				if(precedence == this) {
					result = PostfixationAction.POP;
				} else if(precedence == TokenPrecedence.ADDITION_SUBTRACTION) {
					result = PostfixationAction.STACK;
				} else if(precedence == TokenPrecedence.FUNCTION) {
					result = PostfixationAction.POP;
				}
			}
		}
		
		return result;
	}
	
	public static final TokenPrecedence NULL_PRECEDENCE = new TokenPrecedence("NULL PRECEDENCE");
	public static final TokenPrecedence ADDITION_SUBTRACTION = new TokenPrecedence("ADDITION SUBTRACTION");
	public static final TokenPrecedence MULTIPLICATION_DIVISION = new TokenPrecedence("MULTIPLICATION DIVISION");
	public static final TokenPrecedence OPEN_PERENTHESIS = new TokenPrecedence("OPEN PERENTHESIS");
	public static final TokenPrecedence CLOSE_PERENTHESIS = new TokenPrecedence("CLOSE PERENTHESIS");
	public static final TokenPrecedence FUNCTION = new TokenPrecedence("FUNCTION");
	public static final TokenPrecedence NUMBER = new TokenPrecedence("SPECIAL NUMBER");
	public static final TokenPrecedence ASSIGNMENT = new TokenPrecedence("ASSIGNMENT");
	
}
