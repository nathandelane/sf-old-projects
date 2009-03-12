package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

public class Cosine extends Function {

	public Cosine() {
		super("cos");
		
		_pattern = Pattern.compile("^(cos){1}");
	}
	
}
