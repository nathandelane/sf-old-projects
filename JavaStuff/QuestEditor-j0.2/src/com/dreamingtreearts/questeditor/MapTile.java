package com.dreamingtreearts.questeditor;

import java.awt.*;
import java.awt.image.*;

public class MapTile {
	
	private BufferedImage _image;
	private Rectangle _rect;
	
	public MapTile() {
		
	}
	
	public MapTile(BufferedImage image, Rectangle rect) {
		_image = image;
		_rect = rect;
	}
	
	public void setRect(Rectangle rect) {
		_rect = rect;
	}
	
	public void setImage(BufferedImage image) {
		_image = image;
	}
	
	public BufferedImage getImage() {
		return _image;
	}
	
	public Rectangle getRect() {
		return _rect;
	}

}
