package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

public class Pi extends SpecialNumber {

	public Pi() {
		super("pi");
		
		_pattern = Pattern.compile("^(pi){1}");
	}
	
	@Override
	public double evaluate() {
		return Math.PI;
	}

}
