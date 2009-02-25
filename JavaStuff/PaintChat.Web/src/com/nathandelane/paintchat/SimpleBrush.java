package com.nathandelane.paintchat;

import java.awt.*;

public class SimpleBrush implements IBrush {

	private BrushType _type;
	private Color _color;
	
	public SimpleBrush(BrushType type) {
		_type = type;
		_color = Color.BLACK;
	}
	
	public SimpleBrush(BrushType type, Color color) {
		_type = type;
		_color = color;
	}
	
	public Color getColor() {
		return _color;
	}
	
	@Override
	public BrushType getType() {
		return _type;
	}

}
