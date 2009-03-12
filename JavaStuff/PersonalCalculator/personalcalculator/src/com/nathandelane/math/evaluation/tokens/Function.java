package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

public abstract class Function extends TokenBase {

	public Function(String name) {
		super(TokenType.FUNCTION, TokenPrecedence.FUNCTION, name);
		
		_pattern = Pattern.compile("");
	}
	
}
