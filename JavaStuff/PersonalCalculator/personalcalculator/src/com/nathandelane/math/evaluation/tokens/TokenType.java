package com.nathandelane.math.evaluation.tokens;

public class TokenType {

	private static int __enumCount = 0;
	
	private int _value;
	private String _name;

	public TokenType(String name) {
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
	
	public static final TokenType NULL = new TokenType("NULL");
	public static final TokenType SPECIAL_NUMBER = new TokenType("SPECIAL NUMBER");
	public static final TokenType NUMBER = new TokenType("NUMBER");
	public static final TokenType OPERATOR = new TokenType("OPERATOR");
	public static final TokenType STRUCTURE = new TokenType("STRUCTURE");
	public static final TokenType FUNCTION = new TokenType("FUNCTION");
	public static final TokenType COMMAND = new TokenType("COMMAND");
}
