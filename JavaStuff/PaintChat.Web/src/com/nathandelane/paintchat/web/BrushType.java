package com.nathandelane.paintchat.web;

public class BrushType {
	
	private static int __enumCount = 0;
	
	private int _value;
	private String _name;

	public BrushType(String name) {
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
	
	public static final BrushType NULL = new BrushType("NULL");
	public static final BrushType ELLIPSE = new BrushType("ELLIPSE");
	public static final BrushType RECTANGLE = new BrushType("RECTANGLE");
}
