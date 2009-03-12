package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

public abstract class Command extends TokenBase {

	public Command(TokenType type, TokenPrecedence precedence, String value) {
		super(type, precedence, value);
		
		_pattern = Pattern.compile("");
	}
	
	public abstract void run();

}
