package com.dreamingtreearts.questeditor.ui;

import com.dreamingtreearts.questeditor.resources.*;
import com.dreamingtreearts.questeditor.*;

import java.awt.*;
import java.awt.event.*;
import java.awt.image.*;
import java.util.*;
import javax.swing.*;

public class MapPanel extends JPanel implements MouseMotionListener, MouseListener {
	
	public static final long serialVersionUID = 1L;
	
	private int _width = 0, _height = 0;
	private int _left = 0, _top = 0;
	private Color selectColor = new Color(255, 179, 179, 180);
	private ArrayList<MapTile> _tiles;
	private BufferedImage _bg = null;
	
	public MapPanel(int width, int height) {
		super();
		
		setLayout(new BorderLayout());
		setPreferredSize(new Dimension(width, height));
		setOpaque(false);
		
		_width = width;
		_height = height;
		_tiles = new ArrayList<MapTile>();
		_bg = new BufferedImage(_width, _height, BufferedImage.TYPE_INT_ARGB_PRE);
		
		createDefaultTile();
		
		addMouseMotionListener(this);
		addMouseListener(this);
	}
	
	public void setPreferredSize(Dimension d) {
		super.setPreferredSize(d);
		
		_width = d.width;
		_height = d.height;
		
		repaint();
	}
	
	public void addTile(MapTile mapTile) {
		_tiles.add(mapTile);
		
		repaint();
	}
	
	public void clearTiles() {
		_tiles.clear();
		_bg = new BufferedImage(_width, _height, BufferedImage.TYPE_INT_ARGB_PRE);
		
		repaint();
	}
	
	public int getLeft() {
		return _left;
	}
	
	public int getTop() {
		return _top;
	}
	
	public void createDefaultTile() {
		BufferedImage img = new BufferedImage(16, 16, BufferedImage.TYPE_INT_RGB);
		Graphics2D g = img.createGraphics();
		g.setColor(Color.BLACK);
		g.fillRect(0, 0, 16, 16);
		
		QuestEditorProperties.currentlySelectedTile = new MapTile(img, new Rectangle(0, 0, 16, 16));
	}
	
	public void paintComponent(Graphics g) {
		super.paintComponent(g);
		
		this.setIgnoreRepaint(true);
		
		Graphics2D g2 = _bg.createGraphics();
		
		if(_tiles.size() > 0) {
			for(int i = 0; i < _tiles.size(); i++) {
				g2.drawImage(_tiles.get(i).getImage(), _tiles.get(i).getRect().x, _tiles.get(i).getRect().y, _tiles.get(i).getRect().width, _tiles.get(i).getRect().height, null);
			}
		}
		
		if(QuestEditorProperties.showHover) {
			g2.setColor(selectColor);
			g2.fillRect(_left, _top, 16, 16);
		}
		
		g.drawImage(_bg, 0, 0, this);		
	}
	
	public void update(Graphics g) {
		paintComponent(g);
	}

	@Override
	public void mouseDragged(MouseEvent arg0) { }

	@Override
	public void mouseMoved(MouseEvent arg0) {
		int left = (int)((Math.floor(arg0.getX() / 16) * 16));
		int top = (int)((Math.floor(arg0.getY() / 16) * 16));
		
		if(left != _left || top != _top) {
			_left = left;
			_top = top;
			
			if(QuestEditorProperties.showHover) {
				this.repaint();
			}
			
			QuestEditorProperties.statusField.setText("X: " + (_left / 16) + " | Y: " + (_top / 16) + " | " + QuestEditorProperties.questFile + " is " + QuestEditorProperties.savedMessage + " saved.");
		}
	}

	@Override
	public void mouseClicked(MouseEvent arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void mouseEntered(MouseEvent arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void mouseExited(MouseEvent arg0) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void mousePressed(MouseEvent arg0) {
		int left = (int)((Math.floor(arg0.getX() / 16) * 16));
		int top = (int)((Math.floor(arg0.getY() / 16) * 16));
		
		Rectangle r = new Rectangle(left, top, QuestEditorProperties.currentlySelectedTile.getRect().width, QuestEditorProperties.currentlySelectedTile.getRect().height);
		
		MapTile mt = new MapTile(QuestEditorProperties.currentlySelectedTile.getImage(), r);
		addTile(mt);
		
		QuestEditorProperties.mapSaved = false;
		QuestEditorProperties.savedMessage = "not";
		QuestEditorProperties.statusField.setText("X: " + (_left / 16) + " | Y: " + (_top / 16) + " | " + QuestEditorProperties.questFile + " is " + QuestEditorProperties.savedMessage + " saved.");
	}

	@Override
	public void mouseReleased(MouseEvent arg0) {
		// TODO Auto-generated method stub
		
	}
	
}
