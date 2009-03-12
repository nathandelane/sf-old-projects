package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

public class Tangent extends Function {

	public Tangent() {
		super("tan");
		
		_pattern = Pattern.compile("^(tan){1}");
	}
	
}
