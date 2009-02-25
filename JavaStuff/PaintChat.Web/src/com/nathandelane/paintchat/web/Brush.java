package com.nathandelane.paintchat.web;

import java.awt.*;

public class Brush {
	
	private BrushType _type;
	private int _width;
	private int _height;
	private Color _color;
	
	public Brush() {
		_type = BrushType.ELLIPSE;
		_width = 10;
		_height = 10;
		_color = Color.BLACK;
	}
	
	public Brush(Color color) {
		_type = BrushType.ELLIPSE;
		_width = 10;
		_height = 10;
		_color = color;
	}
	
	public Brush(BrushType type) {
		_type = type;
		_width = 10;
		_height = 10;
		_color = Color.BLACK;
	}
	
	public Brush(BrushType type, Color color) {
		_type = type;
		_width = 10;
		_height = 10;
		_color = color;
	}
	
	public Brush(BrushType type, int width, int height) {
		_type = type;
		_width = width;
		_height = height;
		_color = Color.BLACK;
	}
	
	public Brush(BrushType type, int width, int height, Color color) {
		_type = type;
		_width = width;
		_height = height;
		_color = color;
	}
	
	public BrushType getType() {
		return _type;
	}
	
	public int getWidth() {
		return _width;
	}
	
	public int getHeight() {
		return _height;
	}
	
	public Color getColor() {
		return _color;
	}
	
	public void paint(Layer layer, int x, int y) {
		layer.setColor(_color);
		
		if(_type == BrushType.ELLIPSE) {
			layer.getGraphics().fillOval(getCalculatedX(x), getCalculatedY(y), _width, _height);
		} else if(_type == BrushType.RECTANGLE) {
			layer.getGraphics().fillRect(getCalculatedX(x), getCalculatedY(y), _width, _height);
		}
	}
	
	private int getCalculatedX(int x) {
		return (int)(x - (_width / 2));
	}
	
	private int getCalculatedY(int y) {
		return (int)(y - (_height / 2));
	}
	
}
