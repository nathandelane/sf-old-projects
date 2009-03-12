package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

public class Number extends TokenBase {

	public Number() {
		super(TokenType.NUMBER, TokenPrecedence.NULL_PRECEDENCE, "Not a Number");
		
		_pattern = Pattern.compile("");
	}
	
	public Number(long value) {
		super(TokenType.NUMBER, TokenPrecedence.NUMBER, (new Long(value)).toString());
		
		_pattern = Pattern.compile("^[\\d]*[.]{0,1}[\\d]+");
	}

	public Number(int value) {
		super(TokenType.NUMBER, TokenPrecedence.NUMBER, (new Integer(value)).toString());
		
		_pattern = Pattern.compile("^[\\d]*[.]{0,1}[\\d]+");
	}

	public Number(double value) {
		super(TokenType.NUMBER, TokenPrecedence.NUMBER, (new Double(value)).toString());
		
		_pattern = Pattern.compile("^[\\d]*[.]{0,1}[\\d]+");
	}

	public Number(String value) {
		super(TokenType.NUMBER, TokenPrecedence.NUMBER, value);
		
		_pattern = Pattern.compile("^[\\d]*[.]{0,1}[\\d]+");
	}
	
	public boolean isLong() {
		return this.getValue().length() < 20;
	}
	
	public boolean isInt() {
		return this.getValue().length() < 11; 
	}
	
	public boolean isDouble() {
		return this.getValue().contains(".");
	}

}
