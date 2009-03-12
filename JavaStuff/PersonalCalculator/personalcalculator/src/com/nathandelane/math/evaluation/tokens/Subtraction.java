package com.nathandelane.math.evaluation.tokens;

import java.util.regex.*;

public class Subtraction extends TokenBase {
	
	public Subtraction() {
		super(TokenType.OPERATOR, TokenPrecedence.ADDITION_SUBTRACTION, "-");
		
		_pattern = Pattern.compile("^[-]{1}");
	}
	
}
