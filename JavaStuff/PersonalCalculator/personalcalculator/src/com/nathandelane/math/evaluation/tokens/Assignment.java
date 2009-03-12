package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

public class Assignment extends TokenBase {
	
	public Assignment() {
		super(TokenType.OPERATOR, TokenPrecedence.ASSIGNMENT, "=");
		
		_pattern = Pattern.compile("^[=]{1}");
	}

}
