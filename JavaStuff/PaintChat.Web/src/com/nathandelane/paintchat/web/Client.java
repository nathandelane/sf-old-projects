package com.nathandelane.paintchat.web;

import java.awt.*;
import java.awt.event.*;
import java.util.*;
import javax.swing.*;

public class Client extends JApplet implements MouseMotionListener, MouseListener {

	private static final long serialVersionUID = 1L;
	private static final int __width = 800;
	private static final int __height = 600;
	
	private ArrayList<Layer> _layers;
	private Brush _brush;
	private int _currentLayer;
	
	public void init() {
		setupComponents();
	}	
	
	private void setupComponents() {
		setSize(__width, __height);
		setLayout(null);
		
		Image buffer = createImage(__width, __height);
		
		_layers = new ArrayList<Layer>();
		
		Layer background = new Layer(buffer);
		background.setBackground(Color.WHITE);
		
		_layers.add(background);
		
		_currentLayer = 0;
		
		_brush = new Brush(BrushType.RECTANGLE, Color.RED);
		
		addMouseMotionListener(this);
		addMouseListener(this);
	}
	
	public void update(Graphics g) {
		g.drawImage(_layers.get(_currentLayer).getBuffer(), 0, 0, this);
	}
	
	public void paint(Graphics g) {
		update(g);
	}

	@Override
	public void mouseDragged(MouseEvent e) {
		int brushX = e.getX();
		int brushY = e.getY();

		_brush.paint(_layers.get(_currentLayer), brushX, brushY);
		
		repaint();
		
		e.consume();
	}

	@Override
	public void mouseMoved(MouseEvent e) { }

	@Override
	public void mouseClicked(MouseEvent e) {
		int brushX = e.getX();
		int brushY = e.getY();

		_brush.paint(_layers.get(_currentLayer), brushX, brushY);
		
		repaint();
		
		e.consume();
	}

	@Override
	public void mouseEntered(MouseEvent e) { }

	@Override
	public void mouseExited(MouseEvent e) { }

	@Override
	public void mousePressed(MouseEvent e) { }

	@Override
	public void mouseReleased(MouseEvent e) { }

}
