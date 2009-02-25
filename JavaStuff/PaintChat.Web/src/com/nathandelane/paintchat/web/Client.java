package com.nathandelane.paintchat.web;

import java.awt.*;
import java.awt.event.*;
import java.util.*;
import javax.swing.*;

import com.nathandelane.paintchat.*;

public class Client extends JApplet implements MouseMotionListener, MouseListener {

	private static final long serialVersionUID = 1L;
	private static final int __defaultNumLayers = 2;
	private static final int __width = 800;
	private static final int __height = 600;
	
	private ArrayList<ILayer> _layers;
	private Paintbrush _brush;
	private int _currentLayer;
	
	public void init() {
		setupComponents();
	}	
	
	public void update(Graphics g) {
		g.drawImage(_layers.get(_currentLayer).getBuffer(), 0, 0, this);
	}
	
	public int getCurrentLayer() {
		return _currentLayer;
	}
	
	public Paintbrush getBrush() {
		return _brush;
	}
	
	public int getLayerCount() {
		return _layers.size();
	}
	
	public void paint(Graphics g) {
		update(g);
	}
	
	public void paintOnLayer(Point brushPosition) {
		_brush.paint(_layers.get(_currentLayer), brushPosition);
		
		repaint();
	}
	
	@Override
	public void mouseDragged(MouseEvent e) {
		paintOnLayer(new Point(e.getX(), e.getY()));
		
		e.consume();
	}

	@Override
	public void mouseMoved(MouseEvent e) { }

	@Override
	public void mouseClicked(MouseEvent e) {
		paintOnLayer(new Point(e.getX(), e.getY()));
		
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

	private void setupComponents() {
		setSize(__width, __height);
		setLayout(null);
		setupLayers(getLayerCountParameter());
		
		_currentLayer = 0;
		
		_brush = new Paintbrush(BrushType.RECTANGLE, Color.RED);
		
		addMouseMotionListener(this);
		addMouseListener(this);
	}

	private void setupLayers(int count) {
		_layers = new ArrayList<ILayer>();
		
		for(int counter = 0; counter < count; counter++) {
			Image buffer = createImage(__width, __height);
			
			BasicLayer background = new BasicLayer(buffer);
			background.setBackground(Color.WHITE);
			
			_layers.add(background);			
		}
	}

	private int getLayerCountParameter() {
		return (getParameter("layers") == null) ? __defaultNumLayers : Integer.parseInt(getParameter("layers"));
	}

}
