package com.nathandelane.paintchat;

import java.awt.*;

public interface ILayer {
	
	public Graphics2D getGraphics();
	
	public Image getBuffer();
	
	public void setColor(Color color);
	
	public Color getColor();
	
	public void setBackground(Color color);
	
	public Color getBackground();
	
}
