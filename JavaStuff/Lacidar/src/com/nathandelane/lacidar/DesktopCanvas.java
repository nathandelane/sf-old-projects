package com.nathandelane.lacidar;

import java.awt.Color;
import java.awt.Dimension;

import javax.swing.JFrame;

/**
 * Extend this class in order to create a desktop variant of the application.
 * @author lanathan
 *
 */
@SuppressWarnings("serial")
public abstract class DesktopCanvas extends JFrame {
	
	private ChildCanvas childCanvas;
	
	/**
	 * Constructor for DesktopCanvas
	 * @param java.awt.Dimension dimension
	 */
	protected DesktopCanvas(Dimension dimension) {
		super();
		
		this.childCanvas = new ChildCanvas();
		
		this.setSize(dimension);
		this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		this.setContentPane(this.childCanvas);
		this.setResizable(false);
		this.setUndecorated(true);
	}
	
	@Override
	public void setBackground(Color color) {
		this.getContentPane().setBackground(color);
	}

}
