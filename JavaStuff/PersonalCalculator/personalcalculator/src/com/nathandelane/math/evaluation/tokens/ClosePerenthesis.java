package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

public class ClosePerenthesis extends TokenBase {

	public ClosePerenthesis() {
		super(TokenType.STRUCTURE, TokenPrecedence.CLOSE_PERENTHESIS, ")");
		
		_pattern = Pattern.compile("^[)]{1}");
	}
	
}
