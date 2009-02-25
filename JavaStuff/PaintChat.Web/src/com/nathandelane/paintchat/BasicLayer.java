package com.nathandelane.paintchat;

import java.awt.*;


public class BasicLayer implements ILayer {
	
	private Graphics2D _graphics;
	private Image _buffer;

	public BasicLayer(Image buffer) {
		_buffer = buffer;
		_graphics = (Graphics2D)_buffer.getGraphics();
	}
	
	public BasicLayer(Image buffer, Graphics graphics) {
		_buffer = buffer;
		_graphics = (Graphics2D)graphics;
	}
	
	public BasicLayer(Image buffer, Graphics2D graphics) {
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
	
	public Paintbrush getDefaultEraser() {
		return new Paintbrush(_graphics.getBackground());
	}
	
	public Paintbrush getEraserForBrush(Paintbrush brush) {
		return new Paintbrush(brush.getType(), _graphics.getBackground());
	}

}
