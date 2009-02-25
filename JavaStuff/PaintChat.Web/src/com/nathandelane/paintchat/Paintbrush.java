package com.nathandelane.paintchat;

import java.awt.*;


public class Paintbrush implements ITool {
	
	private static final int __defaultWidth = 10;
	private static final int __defaultHeight = 10;
	
	private SimpleBrush _brush;
	private Dimension _dimensions;
	
	public Paintbrush() {
		_brush = new SimpleBrush(BrushType.ELLIPSE);
		_dimensions = new Dimension(__defaultWidth, __defaultHeight);
	}
	
	public Paintbrush(Color color) {
		_brush = new SimpleBrush(BrushType.ELLIPSE, color);
		_dimensions = new Dimension(__defaultWidth, __defaultHeight);
	}
	
	public Paintbrush(BrushType type) {
		_brush = new SimpleBrush(type);
		_dimensions = new Dimension(__defaultWidth, __defaultHeight);
	}
	
	public Paintbrush(BrushType type, Color color) {
		_brush = new SimpleBrush(type, color);
		_dimensions = new Dimension(__defaultWidth, __defaultHeight);
	}
	
	public Paintbrush(BrushType type, int width, int height) {
		_brush = new SimpleBrush(type);
		_dimensions = new Dimension(width, height);
	}
	
	public Paintbrush(BrushType type, int width, int height, Color color) {
		_brush = new SimpleBrush(type, color);
		_dimensions = new Dimension(width, height);
	}
	
	public BrushType getType() {
		return _brush.getType();
	}

	public Dimension getDimensions() {
		return _dimensions;
	}
	
	public Color getColor() {
		return _brush.getColor();
	}
	
	public void paint(ILayer layer, Point brushPosition) {
		layer.setColor(_brush.getColor());
		
		if(_brush.getType() == BrushType.ELLIPSE) {
			layer.getGraphics().fillOval(getCalculatedPoint(brushPosition).x, getCalculatedPoint(brushPosition).y, _dimensions.width, _dimensions.height);
		} else if(_brush.getType() == BrushType.RECTANGLE) {
			layer.getGraphics().fillRect(getCalculatedPoint(brushPosition).x, getCalculatedPoint(brushPosition).y, _dimensions.width, _dimensions.height);
		}
	}
	
	private Point getCalculatedPoint(Point brushPosition) {
		Point calculatedPoint = new Point((int)(brushPosition.x - (_dimensions.width / 2)), (int)(brushPosition.y - (_dimensions.height / 2)));
		
		return calculatedPoint;
	}
	
	@Override
	public Dimension getDimension() {
		return new Dimension(_dimensions.width, _dimensions.height);
	}

	@Override
	public IBrush getBrush() {
		return _brush;
	}
	
}
