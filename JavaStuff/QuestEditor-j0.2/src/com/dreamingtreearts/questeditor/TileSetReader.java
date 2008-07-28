package com.dreamingtreearts.questeditor;

import java.io.*;
import java.util.*;
import javax.xml.parsers.*;
import org.w3c.dom.*;
//import org.xml.sax.*;

public class TileSetReader {
	
	private String _tileSetsFile;
	private ArrayList<TileSet> _tileSetsCollection;
	
	public TileSetReader(String tileSetsFile) throws FileNotFoundException {
		_tileSetsFile = tileSetsFile;
		
		createTileSets();
	}
	
	public ArrayList<TileSet> readTileSets() {
		return _tileSetsCollection;
	}
	
	private void createTileSets() {
		_tileSetsCollection = new ArrayList<TileSet>();
		
		try {
			DocumentBuilderFactory docBuilderFactory = DocumentBuilderFactory.newInstance();
			DocumentBuilder docBuilder = docBuilderFactory.newDocumentBuilder();
			Document doc = docBuilder.parse(_tileSetsFile);
			
			// Normalize text representation
			doc.getDocumentElement().normalize();
			
			if(doc.getFirstChild() != null) {
				NodeList tilesets = doc.getElementsByTagName("tileset");
			}
		} catch(Exception e) {
			e.printStackTrace();
		}
	}

}
