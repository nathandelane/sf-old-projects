package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

public class OpenPerenthesis extends TokenBase {
	
	public OpenPerenthesis() {
		super(TokenType.STRUCTURE, TokenPrecedence.OPEN_PERENTHESIS, "(");
		
		_pattern = Pattern.compile("^[(]{1}");
	}

}
