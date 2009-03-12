package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

public class Division extends TokenBase {
	
	public Division() {
		super(TokenType.OPERATOR, TokenPrecedence.MULTIPLICATION_DIVISION, "/");
		
		_pattern = Pattern.compile("^[/]{1}");
	}

}
