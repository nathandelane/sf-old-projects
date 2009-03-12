package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

public abstract class SpecialNumber extends TokenBase {
	
	public SpecialNumber(String value) {
		super(TokenType.SPECIAL_NUMBER, TokenPrecedence.NUMBER, value);
		
		_pattern = Pattern.compile("");
	}
	
	public abstract double evaluate();

}
