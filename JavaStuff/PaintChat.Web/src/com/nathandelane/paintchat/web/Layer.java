package com.nathandelane.paintchat.web;

import java.awt.*;

public class Layer {
	
	private Graphics2D _graphics;
	private Image _buffer;

	public Layer(Image buffer) {
		_buffer = buffer;
		_graphics = (Graphics2D)_buffer.getGraphics();
	}
	
	public Layer(Image buffer, Graphics graphics) {
		_buffer = buffer;
		_graphics = (Graphics2D)graphics;
	}
	
	public Layer(Image buffer, Graphics2D graphics) {
		_buffer = buffer;
		_graphics = graphics;
	}
	
	public Graphics2D getGraphics() {
		return _graphics;
	}
	
	public Image getBuffer() {
		return _buffer;
	}
	
	public void setColor(Color color) {
		_graphics.setColor(color);
	}
	
	public Color getColor() {
		return _graphics.getColor();
	}
	
	public void setBackground(Color color) {
		_graphics.setBackground(color);
	}
	
	public Color getBackground() {
		return _graphics.getBackground();
	}
	
	public Brush getDefaultEraser() {
		return new Brush(_graphics.getBackground());
	}
	
	public Brush getEraserForBrush(Brush brush) {
		return new Brush(brush.getType(), _graphics.getBackground());
	}

}
