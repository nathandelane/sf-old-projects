package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

public abstract class TokenBase implements IToken {

	protected Pattern _pattern = Pattern.compile("");
	
	private static Matcher _matcher;
	
	private TokenType _type;
	private TokenPrecedence _precedence;
	private String _value;
	
	public TokenBase(TokenType type, TokenPrecedence precedence, String value) {
		_type = type;
		_precedence = precedence;
		_value = value;
	}
	
	@Override
	public String firstMatch() {
		return TokenBase._matcher.group(0);
	}

	@Override
	public TokenPrecedence getPrecedence() {
		return _precedence;
	}

	@Override
	public TokenType getType() {
		return _type;
	}

	@Override
	public String getValue() {
		return _value;
	}

	@Override
	public boolean matches(String pattern) {
		TokenBase._matcher = _pattern.matcher(pattern);
		
		return TokenBase._matcher.find();
	}

	@Override
	public void setPrecedence(TokenPrecedence precedence) {
		_precedence = precedence;
	}
	
	@Override
	public String toString() {
		return String.format("{Type=%1$s;Value=%2$s;Precedence=%3$s}", getType(), getValue(), getPrecedence());
	}

}
