package com.nathandelane.math.evaluation.tokens;

public class InvalidTokenException extends Exception {

	public InvalidTokenException(String expression) {
		super(expression);
	}

	private static final long serialVersionUID = 2825458251618277189L;

}
