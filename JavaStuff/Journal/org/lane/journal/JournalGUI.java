/*
 * Author: Nathan Lane
 * Last Updated: 03/16/2007
 * 
 * This class initializes the GUI for the Journal program.
 */

package org.lane.journal;

import javax.swing.*;

import java.awt.*;
import java.util.Locale;
import java.util.Hashtable;

public class JournalGUI extends JFrame {
	
	public static final long serialVersionUID = 1;
	public static Dimension screenDimensions;
	
	public Hashtable<String, JToggleButton> pluginButtonsTable;

	private JPanel journalContainer;
	private JPanel journalPluginPanel;
	private JScrollPane journalToolbar;
	private JScrollPane journalScrollPanel;
	private JTextArea journalEditField;
	
	public JournalGUI() {
		super("Journal");
		addWindowListener(new JournalGUIWindowEventHandler());
		// Set up the User Interface
		setupUserInterface();
		centerJournalGUI();
		pluginButtonsTable = new Hashtable<String, JToggleButton>();
	}
	
	public JPanel getJournalContainer() {
		return journalContainer;
	}
	
	public JPanel getJournalPluginPanel() {
		return journalPluginPanel;
	}
	
	public JScrollPane getJournalToolbar() {
		return journalToolbar;
	}
	
	public JScrollPane getJournalScrollPanel() {
		return journalScrollPanel;
	}
	
	public JTextArea getJournalEditField() {
		return journalEditField;
	}
	
	public void centerJournalGUI() {
		screenDimensions = Toolkit.getDefaultToolkit().getScreenSize();
		int width = this.getWidth();
		int height = this.getHeight();
		this.setLocation(((screenDimensions.width / 2) - (width / 2)), ((screenDimensions.height / 2) - (height / 2)));
	}
	
	public String getJournalText() {
		String value = "";
		
		if(journalEditField.getText() != null) {
			value = journalEditField.getText();
		}
		
		return value;
	}
	
	public String setJournalEditField(String newText) {
		String text = "";
		
		journalEditField.setText(newText);
		text = getJournalText();
		
		return text;
	}
	
	private void setupUserInterface() {
		// Set up components
		setupComponents();
		// Content Pane
		this.setContentPane(getJournalContainer());
		this.setSize(new Dimension(408, 425));
	}
	
	private void setupComponents() {
		try {
			setupJournalContainer();
			setupJournalPluginPanel();
			setupJournalToolbar();
			setupJournalEditField();
			setupJournalScrollPanel();
		} catch(Exception e) {
			e.printStackTrace();
		}
	}
	private void setupJournalContainer() {
		journalContainer = new JPanel();
		journalContainer.setLayout(new BorderLayout());
	}
	
	private void setupJournalPluginPanel() {
		journalPluginPanel = new JPanel(new GridLayout(3, 0));
		journalPluginPanel.setBackground(Color.BLACK);
	}
	
	private void setupJournalToolbar() throws Exception {
		if(journalContainer != null) {
			journalToolbar = new JScrollPane(journalPluginPanel);
			journalContainer.add(journalToolbar, BorderLayout.NORTH);
		} else {
			throw new Exception("No initialized JournalContainer was found");
		}
	}
	
	private void setupJournalScrollPanel() throws Exception {
		if(journalContainer != null && journalEditField != null) {
			journalScrollPanel = new JScrollPane(journalEditField);
			journalContainer.add(journalScrollPanel, BorderLayout.CENTER);
		} else {
			throw new Exception("No initialized JournalContainer was found");
		}
	}
	
	private void setupJournalEditField() {
		journalEditField = new JTextArea();
		journalEditField.setWrapStyleWord(true);
		journalEditField.setLineWrap(true);
		journalEditField.setFont(new Font("Sans Serif", Font.PLAIN, 14));
		journalEditField.setLocale(Locale.US);
		journalEditField.addKeyListener(new JournalKeyEventHandler());
	}
	
}
