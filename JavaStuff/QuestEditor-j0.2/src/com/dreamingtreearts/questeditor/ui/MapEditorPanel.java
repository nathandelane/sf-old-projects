package com.dreamingtreearts.questeditor.ui;

import java.awt.*;
import java.awt.image.*;
import javax.swing.*;

public class MapEditorPanel extends JPanel {
	
	public static final long serialVersionUID = 1L;
	
	private int _width = 0, _height = 0;
	private MapPanel _mapPanel = null;
	
	public MapEditorPanel(int width, int height) {
		super();
		
		_width = width;
		_height = height;
		_mapPanel = new MapPanel(width, height);
		
		setLayout(new BorderLayout());
		setPreferredSize(new Dimension(width, height));
		
		add(_mapPanel, BorderLayout.CENTER);
	}
	
	public MapPanel getMapPanel() {
		return _mapPanel;
	}
	
	public void setPreferredSize(Dimension d) {
		super.setPreferredSize(d);
		
		_mapPanel.setPreferredSize(d);
		
		_width = d.width;
		_height = d.height;
		
		repaint();
	}
	
	public void paintComponent(Graphics g) {
		super.paintComponent(g);
		
		this.setIgnoreRepaint(true);
		
		BufferedImage backgroundImage = new BufferedImage(32, 32, BufferedImage.TYPE_INT_RGB);
		Graphics2D bi = backgroundImage.createGraphics();
		Color darkGray = new Color(136, 136, 136);
		bi.setColor(Color.LIGHT_GRAY);
		bi.fillRect(0, 0, 16, 16);
		bi.setColor(darkGray);
		bi.fillRect(16, 0, 16, 16);
		bi.setColor(darkGray);
		bi.fillRect(0, 16, 16, 16);
		bi.setColor(Color.LIGHT_GRAY);
		bi.fillRect(16, 16, 16, 16);
		
		TexturePaint tp = new TexturePaint(backgroundImage, new Rectangle(0, 0, 32, 32));
		
		BufferedImage bg = new BufferedImage(getWidth(), getHeight(), BufferedImage.TYPE_INT_RGB);
		
		Graphics2D g2 = bg.createGraphics();
		g2.setPaint(tp);
		g2.fill(new Rectangle(0, 0, _width, _height));
		
		g.drawImage(bg, 0, 0, this);		
	}
	
	public void update(Graphics g) {
		paintComponent(g);
	}

}
