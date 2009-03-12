package com.nathandelane.math.evaluation;

public class EvaluationState {
	
	public static String __lastResult = "0";
	public static EvaluationMode __mode = EvaluationMode.RADIANS;
	public static VariableCollection __variables = new VariableCollection();
	
	public static String evaluate() {
		StringBuilder sb = new StringBuilder();
		
		sb.append(String.format("LastResult = %1$s\n", EvaluationState.__lastResult));
		sb.append(String.format("Mode = %1$s\n", EvaluationState.__mode));
		sb.append(String.format("LastResult = %1$s\n", EvaluationState.__variables));
		
		return sb.toString();
	}

}
