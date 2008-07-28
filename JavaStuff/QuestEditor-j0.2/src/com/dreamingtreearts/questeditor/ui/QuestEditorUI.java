package com.dreamingtreearts.questeditor.ui;

import com.dreamingtreearts.questeditor.resources.*;
import com.dreamingtreearts.questeditor.*;

import java.awt.*;
import java.awt.event.*;

import javax.swing.*;

public class QuestEditorUI extends JFrame implements WindowListener, ComponentListener, ActionListener {
	
	private static final long serialVersionUID = 1L;
	
	private JFileChooser jfc;
	private JMenuBar menuBar;
	private JMenu fileMenu, insertMenu;
	private JMenuItem newMap, openMap, saveMap, exitQE;
	private JMenuItem topLeft, bottomRight, wall, staircase, door, staircaseUp, staircaseDown;
	private Box verticalHighLevelBox;
	private JTabbedPane editorPane, tileSetPane;
	private JPanel mapPanel, mapSettingsPanel, mapScriptsPanel;
	private MapEditorPanel mep;
	private JPanel tilesets[];

	public QuestEditorUI() {
		super(QuestEditorProperties.qaTitle);
		
		setupUI();
		
		setVisible(true);
	}
	
	public void setSize(Dimension size) {
		super.setSize(size);
		
		QuestEditorProperties.qeDimension = size;
	}

	private void setupUI() {
		setSize(QuestEditorProperties.qeDimension);
		setResizable(true);
		setLayout(new BorderLayout());
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setLookAndFeel(QuestEditorProperties.qeLnF);
		
		initializeComponents();
	}

	private void initializeComponents() {
		UIManager.put("Button.Margin", new Insets(0, 0, 0, 0));
		
		// Create the file chooser
		jfc = new JFileChooser();
		jfc.setFileFilter(new FileChooserMapFilter());
		
		// Create the high-level container
		verticalHighLevelBox = Box.createVerticalBox();
		getContentPane().add(verticalHighLevelBox, BorderLayout.CENTER);
		
		// Create the menu bar
		menuBar = new JMenuBar();
		setJMenuBar(menuBar);
		
		// File Menu
		fileMenu = new JMenu("File");
		fileMenu.setMnemonic('F');
		newMap = new JMenuItem("New");
		newMap.setMnemonic('N');
		newMap.addActionListener(this);
		fileMenu.add(newMap);
		openMap = new JMenuItem("Open");
		openMap.setMnemonic('O');
		openMap.addActionListener(this);
		fileMenu.add(openMap);
		fileMenu.addSeparator();
		saveMap = new JMenuItem("Save");
		saveMap.setMnemonic('S');
		saveMap.addActionListener(this);
		fileMenu.add(saveMap);
		fileMenu.addSeparator();
		exitQE = new JMenuItem("Exit");
		exitQE.setMnemonic('x');
		exitQE.addActionListener(this);
		fileMenu.add(exitQE);
		
		menuBar.add(fileMenu);
		
		// Insert Menu
		insertMenu = new JMenu("Insert");
		insertMenu.setMnemonic('I');
		topLeft = new JMenuItem("Top Left");
		topLeft.setMnemonic('L');
		insertMenu.add(topLeft);
		bottomRight = new JMenuItem("Bottom Right");
		bottomRight.setMnemonic('R');
		insertMenu.add(bottomRight);
		insertMenu.addSeparator();
		wall = new JMenuItem("Wall");
		wall.setMnemonic('W');
		insertMenu.add(wall);
		insertMenu.addSeparator();
		staircase = new JMenu("Staircase");
		staircaseUp = new JMenuItem("Up");
		staircase.add(staircaseUp);
		staircaseDown = new JMenuItem("Down");
		staircase.add(staircaseDown);
		staircase.setMnemonic('t');
		insertMenu.add(staircase);
		insertMenu.addSeparator();
		door = new JMenu("Door");
		door.setMnemonic('D');
		insertMenu.add(door);
		
		menuBar.add(insertMenu);
		
		// Create editor pane
		editorPane = new JTabbedPane();
		verticalHighLevelBox.add(editorPane);
		editorPane.setPreferredSize(new Dimension(editorPane.getPreferredSize().width, 500));
		// Add tabs
		mapPanel = new JPanel();
		mapPanel.setLayout(new BorderLayout());
		mep = new MapEditorPanel(1600, 1600);
		JScrollPane jsp = new JScrollPane(mep);
		mapPanel.add(jsp, BorderLayout.CENTER);
		editorPane.add("Map Editor - " + QuestEditorProperties.questFile, mapPanel);
		mapSettingsPanel = new JPanel();
		editorPane.add("Map Settings", mapSettingsPanel);
		mapScriptsPanel = new JPanel();
		mapScriptsPanel.setLayout(new BorderLayout());
		JTextArea jta = new JTextArea();
		jta.setWrapStyleWord(true);
		jta.setLineWrap(true);
		JScrollPane jsp2 = new JScrollPane(jta);
		mapScriptsPanel.add(jsp2, BorderLayout.CENTER);
		editorPane.add("Scripts", mapScriptsPanel);
		
		// Create tile set pane
		tileSetPane = new JTabbedPane();
		verticalHighLevelBox.add(tileSetPane);
		tileSetPane.setPreferredSize(new Dimension(tileSetPane.getPreferredSize().width, 80));
		tilesets = loadTileSets();
		
		for(int i = 0; i < tilesets.length; i++) {
			tileSetPane.add(tilesets[i].getName(), tilesets[i]);
		}
		
		// Create status field
		QuestEditorProperties.statusField = new JTextField("X: 0 | Y: 0");
		QuestEditorProperties.statusField.setEnabled(false);
		QuestEditorProperties.statusField.setEditable(false);
		QuestEditorProperties.statusField.setDisabledTextColor(Color.BLACK);
		
		getContentPane().add(QuestEditorProperties.statusField, BorderLayout.SOUTH);
	}
	
	private JPanel[] loadTileSets() {
		JPanel tsets[] = new JPanel[1];
		
		tsets[0] = new JPanel();
		tsets[0].setName("Default Tileset");
		
		return tsets;
	}
	
	public void setLookAndFeel(String lnfClassName) {
		try {
			UIManager.setLookAndFeel(lnfClassName);
		} catch (ClassNotFoundException e) {
			e.printStackTrace();
		} catch (InstantiationException e) {
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			e.printStackTrace();
		} catch (UnsupportedLookAndFeelException e) {
			e.printStackTrace();
		}
	}
	
	@Override
	public void windowActivated(WindowEvent arg0) { }

	@Override
	public void windowClosed(WindowEvent arg0) { }

	@Override
	public void windowClosing(WindowEvent arg0) { }

	@Override
	public void windowDeactivated(WindowEvent arg0) { }

	@Override
	public void windowDeiconified(WindowEvent arg0) { }

	@Override
	public void windowIconified(WindowEvent arg0) { }

	@Override
	public void windowOpened(WindowEvent arg0) { }

	@Override
	public void componentHidden(ComponentEvent arg0) { }

	@Override
	public void componentMoved(ComponentEvent arg0) { }

	@Override
	public void componentResized(ComponentEvent arg0) {
		QuestEditorProperties.qeDimension = new Dimension(this.getWidth(), this.getHeight());
	}

	@Override
	public void componentShown(ComponentEvent arg0) { }

	@Override
	public void actionPerformed(ActionEvent arg0) {
		if(arg0.getSource().equals(newMap)) {
			if(!QuestEditorProperties.mapSaved) {
				int result = JOptionPane.showConfirmDialog(this, "This quest is not saved, and this operation will cause all changes to be lost. Continue?", "Warning", JOptionPane.YES_NO_OPTION, JOptionPane.WARNING_MESSAGE);
				
				switch(result) {
				case JOptionPane.YES_OPTION:
					QuestEditorProperties.questFile = "Untitled";
					editorPane.setTitleAt(0, "Map Editor - " + QuestEditorProperties.questFile);
					mep.getMapPanel().clearTiles();
					break;
				case JOptionPane.NO_OPTION:
					break;
				}
			} else {
				QuestEditorProperties.questFile = "Untitled";
				editorPane.setTitleAt(0, "Map Editor - " + QuestEditorProperties.questFile);
				mep.getMapPanel().clearTiles();
			}
		} else if(arg0.getSource().equals(openMap)) {
			if(!QuestEditorProperties.mapSaved) {
				int clearResult = JOptionPane.showConfirmDialog(this, "This quest is not saved, and this operation will cause all changes to be lost. Continue?", "Warning", JOptionPane.YES_NO_OPTION, JOptionPane.WARNING_MESSAGE);
				
				switch(clearResult) {
				case JOptionPane.YES_OPTION:
					int result = jfc.showOpenDialog(this);
					
					switch(result) {
					case JFileChooser.APPROVE_OPTION:
						QuestEditorProperties.questFile = jfc.getSelectedFile().getName();
						editorPane.setTitleAt(0, "Map Editor - " + QuestEditorProperties.questFile);
						break;
					case JFileChooser.CANCEL_OPTION:
						break;
					}
				case JOptionPane.NO_OPTION:
					break;
				}
			} else {
				int result = jfc.showOpenDialog(this);
				
				switch(result) {
				case JFileChooser.APPROVE_OPTION:
					QuestEditorProperties.questFile = jfc.getSelectedFile().getName();
					editorPane.setTitleAt(0, "Map Editor - " + QuestEditorProperties.questFile);
					break;
				case JFileChooser.CANCEL_OPTION:
					break;
				}
			}
		} else if(arg0.getSource().equals(saveMap)) {
			// TODO: Implement Save Method
			JOptionPane.showMessageDialog(this, "TODO: Implement Save Method.");
			QuestEditorProperties.savedMessage = "now";
			QuestEditorProperties.mapSaved = true;
			QuestEditorProperties.statusField.setText("X: " + (mep.getMapPanel().getLeft() / 16) + " | Y: " + (mep.getMapPanel().getTop() / 16) + " | " + QuestEditorProperties.questFile + " is " + QuestEditorProperties.savedMessage + " saved.");
		} else if(arg0.getSource().equals(exitQE)) {
			System.exit(0);
		}
	}

}
