package org.lane.personalcalculator;

public class ExpressionToken {
	
	public static final int VoidToken = -1;
	public static final int OpenParenthesis = 0;
	public static final int CloseParenthesis = 1;
	public static final int Number = 2;
	public static final int AddOperator = 3;
	public static final int NegationOperator = 4;
	public static final int MultiplicationOperator = 5;
	public static final int DivisionOperator = 6;
	public static final int Variable = 7;
	public static final int Function = 8;
	public static final int PowerOperator = 9;
	public static final int LastResult = 10;
	public static final int TraceCommand = 11;
	public static final int Factorial = 12;
	public static final String MD2Method = "MD2";
	public static final String MD5Method = "MD5";
	public static final String SHA1Method = "SHA";
	
	private String token;
	private int tokenType;
	
	public ExpressionToken(String token, int tokenType) {
		this.token = token;
		this.tokenType = tokenType;
	}
	
	public String getToken()
	{
		return token;
	}
	
	public int getTokenType()
	{
		return tokenType;
	}

}
