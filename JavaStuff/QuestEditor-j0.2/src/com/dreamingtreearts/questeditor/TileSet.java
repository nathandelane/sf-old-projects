package com.dreamingtreearts.questeditor;

import java.awt.*;
import java.awt.image.*;
import javax.swing.*;

public class TileSet {
	
	private BufferedImage _tileImage = null;
	private int _tilesX = 0;
	private int _tilesY = 0;
	private int _tileWidth = 16;
	private int _tileHeight = 16;
	
	public TileSet(String pathToTileSet, int tilesX, int tilesY, int tileWidth, int tileHeight) {
		_tilesX = tilesX;
		_tilesY = tilesY;
		_tileWidth = tileWidth;
		_tileHeight = tileHeight;
		
		ImageIcon i = new ImageIcon(pathToTileSet);
		BufferedImage bi = new BufferedImage(i.getIconWidth(), i.getIconHeight(), BufferedImage.TYPE_INT_RGB);
		Graphics2D g2 = bi.createGraphics();
		g2.drawImage(i.getImage(), 0, 0, null);
		_tileImage = bi;
	}
	
	public BufferedImage getImageAt(int index) {
		int x = (index % _tilesX);
		int y = (index / _tilesY);
		
		BufferedImage result = getImageAt(x, y);
		
		return result;
	}
	
	public BufferedImage getImageAt(int x, int y) {
		BufferedImage result = _tileImage.getSubimage((x * _tileWidth), (y * _tileHeight), _tileWidth, _tileHeight);
		
		return result;
	}
	
	public MapTile getTileAt(int index) {
		MapTile result = new MapTile(getImageAt(index), new Rectangle(0, 0, 16, 16));
		
		return result;
	}
	
	public MapTile getTileAt(int x, int y) {
		MapTile result = new MapTile(getImageAt(x, y), new Rectangle(0, 0, 16, 16));
		
		return result;
	}

}
