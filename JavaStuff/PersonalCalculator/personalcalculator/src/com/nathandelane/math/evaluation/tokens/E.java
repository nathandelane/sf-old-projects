package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

public class E extends SpecialNumber {

	public E() {
		super("e");
		
		_pattern = Pattern.compile("^[e]{1}");
	}
	
	@Override
	public double evaluate() {
		return Math.E;
	}

}
