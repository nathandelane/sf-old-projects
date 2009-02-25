package com.nathandelane.paintchat.web;

import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class Client extends JApplet implements MouseMotionListener {

	private static final long serialVersionUID = 1L;
	private static final int __width = 800;
	private static final int __height = 600;
	
	private Image _backBuffer;
	private Graphics _background;
	
	public void init() {
		try {
			setupComponents();
		} catch(Exception e) {
			this.getGraphics().drawString(String.format("Exception caught! %1$s", e.getMessage()), 0, 0);
		}
	}	
	
	private void setupComponents() {
		setSize(__width, __height);
		setLayout(null);
		
		_backBuffer = createImage(__width, __height);
		
		_background = _backBuffer.getGraphics();
		_background.setColor(Color.WHITE);
		_background.fillRect(0, 0, __width, __height);
		_background.setColor(Color.BLACK);
		
		addMouseMotionListener(this);
	}
	
	public void update(Graphics g) {
		g.drawImage(_backBuffer, 0, 0, this);
	}
	
	public void paint(Graphics g) {
		update(g);
	}

	@Override
	public void mouseDragged(MouseEvent e) {
		int brushX = e.getX();
		int brushY = e.getY();
		
		_background.fillOval(brushX - 5, brushY - 5, 10, 10);
		
		repaint();
		
		e.consume();
	}

	@Override
	public void mouseMoved(MouseEvent e) { }

}
