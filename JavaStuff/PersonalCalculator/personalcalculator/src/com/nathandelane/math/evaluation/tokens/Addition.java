package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

public class Addition extends TokenBase {
	
	public Addition() {
		super(TokenType.OPERATOR, TokenPrecedence.ADDITION_SUBTRACTION, "+");
		
		_pattern = Pattern.compile("^[+]{1}");
	}

}
