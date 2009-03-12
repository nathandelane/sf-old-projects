package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

public class Multiplication extends TokenBase {

	public Multiplication() {
		super(TokenType.OPERATOR, TokenPrecedence.MULTIPLICATION_DIVISION, "*");
		
		_pattern = Pattern.compile("^[*]{1}");
	}
	
}
