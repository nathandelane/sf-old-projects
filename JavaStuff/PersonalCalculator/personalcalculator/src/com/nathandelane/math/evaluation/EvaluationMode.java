package com.nathandelane.math.evaluation;

public class EvaluationMode {

	private static int __enumCount = 0;
	
	private int _value;
	private String _name;

	private EvaluationMode(String name) {
		_name = name;
		_value = __enumCount;
		__enumCount++;
	}
	
	public String toString() {
		return _name;
	}
	
	public int toInt() {
		return _value;
	}
	
	public static final EvaluationMode RADIANS = new EvaluationMode("RADIANS");

}
