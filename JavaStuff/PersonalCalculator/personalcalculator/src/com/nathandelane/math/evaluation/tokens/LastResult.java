package com.nathandelane.math.evaluation.tokens;

import java.util.regex.Pattern;

import com.nathandelane.math.evaluation.EvaluationState;

public class LastResult extends SpecialNumber {

	public LastResult() {
		super("$");
		
		_pattern = Pattern.compile("^[$]{1}");
	}
	
	@Override
	public double evaluate() {
		return (Double.parseDouble(EvaluationState.__lastResult));
	}

}
