package com.nathandelane.math.evaluation.tokens;

import java.util.regex.*;

public class NullToken extends TokenBase {
	
	public NullToken() {
		super(TokenType.NULL, TokenPrecedence.NULL_PRECEDENCE, "");
		
		_pattern = Pattern.compile("");
	}
	
}
