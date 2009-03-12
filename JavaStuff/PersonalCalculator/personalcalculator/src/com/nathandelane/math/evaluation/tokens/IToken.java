package com.nathandelane.math.evaluation.tokens;

public interface IToken {
	
	public boolean matches(String pattern) throws NotImplementedException;
	
	public String firstMatch() throws NotImplementedException;
	
	public TokenType getType();
	
	public TokenPrecedence getPrecedence();
	
	public void setPrecedence(TokenPrecedence precedence);
	
	public String getValue();
	
	public String toString();
}
