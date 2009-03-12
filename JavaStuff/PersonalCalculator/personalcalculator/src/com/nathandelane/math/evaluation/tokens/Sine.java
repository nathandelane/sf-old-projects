package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

public class Sine extends Function {

	public Sine() {
		super("sin");
		
		_pattern = Pattern.compile("^(sin){1}");
	}
	
}
