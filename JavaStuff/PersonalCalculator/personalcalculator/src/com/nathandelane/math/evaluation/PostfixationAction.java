package com.nathandelane.math.evaluation;

public class PostfixationAction {
	
	private static int __enumCount = 0;
	
	private int _value;
	private String _name;

	private PostfixationAction(String name) {
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
	
	public static final PostfixationAction STACK = new PostfixationAction("STACK");
	public static final PostfixationAction POP = new PostfixationAction("POP");
	public static final PostfixationAction PUSH = new PostfixationAction("PUSH");

}
