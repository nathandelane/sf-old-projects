package org.lane.opentcards.userinterface;

import java.awt.*;
import java.awt.event.*;
import java.util.ArrayList;
import javax.swing.*;
import net.miginfocom.layout.*;
import net.miginfocom.swing.*;
import org.lane.opentcards.types.*;

public class OpenTCardsGUI extends JFrame implements WindowListener, ActionListener, MouseListener {
	
	private ArrayList<TCardsBoard> _tCardsBoardCollection;
	private ArrayList<JPanel> _boardCollection;
	private ArrayList<TCard> _tCardsList;
	
	public OpenTCardsGUI(ArrayList<TCardsBoard> boardCollection) {
		super("Open T-Cards version 0.2");
		
		_tCardsBoardCollection = boardCollection; 
		_boardCollection = new ArrayList<JPanel>();
		_tCardsList = new ArrayList<TCard>();
		
		setupUserInterface();
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setVisible(true);
	}
	
	private void setupUserInterface() {
		setSize(800, 600);
		setMinimumSize(new Dimension(800, 600));
		//setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
		//setLayout(new MigLayout());
		setLocation(50, 20);
		addWindowListener(this);
		
		try {
			UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
		} catch(Exception e) {
			// Log the exception somewhere
		}
		
		JPanel mainPanel = new JPanel();
		mainPanel.setLayout(new BoxLayout(mainPanel, BoxLayout.Y_AXIS));
		setContentPane(mainPanel);
		
		// Set up the components
		setupComponents();
		addEventListeners();
	}
	
	private void setupComponents() {
		menuBar = new JMenuBar();
		menuBar.add((fileMenu = new JMenu("File")));
		menuBar.add((helpMenu = new JMenu("Help")));
		//fileMenu.add((newBoardItem = new JMenuItem("New board")));
		fileMenu.add((newCardItem = new JMenuItem("New card")));
		fileMenu.add(new JSeparator());
		fileMenu.add((exitItem = new JMenuItem("Exit")));
		setJMenuBar(menuBar);
		
		dataEntryPanel = new JPanel();
		//dataEntryPanel.setBackground(Color.LIGHT_GRAY);
		dataEntryPanel.setPreferredSize(new Dimension(300, 34));
		dataEntryPanel.setMaximumSize(new Dimension(6000, 34));
		dataEntryPanel.setLayout(new BoxLayout(dataEntryPanel, BoxLayout.X_AXIS));
		getContentPane().add(dataEntryPanel);
		//dataEntryPanel.setVisible(false);
		
		cardEntryPanel = new JPanel(new FlowLayout());
		cardEntryPanel.setBackground(Color.LIGHT_GRAY);
		cardEntryPanel.add(new JLabel("Enter Name for New T-Card"));
		cardNameField = new JTextField();
		cardNameField.setColumns(32);
		cardEntryPanel.add(cardNameField);
		addCardButton = new JButton("Add T-Card");
		addCardButton.setBackground(Color.LIGHT_GRAY);
		cardEntryPanel.add(addCardButton);
		cancelAddCardButton = new JButton("Cancel");
		cancelAddCardButton.setBackground(Color.LIGHT_GRAY);
		cardEntryPanel.add(cancelAddCardButton);
		cardEntryPanel.setVisible(false);
		dataEntryPanel.add(cardEntryPanel);
		
		boardEntryPanel = new JPanel(new FlowLayout());
		boardEntryPanel.setBackground(Color.LIGHT_GRAY);
		boardEntryPanel.add(new JLabel("Enter Name for New Board"));
		boardNameField = new JTextField();
		boardNameField.setColumns(32);
		boardEntryPanel.add(boardNameField);
		addBoardButton = new JButton("Add Board");
		addBoardButton.setBackground(Color.LIGHT_GRAY);
		boardEntryPanel.add(addBoardButton);
		cancelAddBoardButton = new JButton("Cancel");
		cancelAddBoardButton.setBackground(Color.LIGHT_GRAY);
		boardEntryPanel.add(cancelAddBoardButton);
		boardEntryPanel.setVisible(false);
		dataEntryPanel.add(boardEntryPanel);
		
		buttonPanel = new JPanel();
		buttonPanel.setMinimumSize(new Dimension(100, 6000));
		
		boardPanel = new JTabbedPane();
		boardPanel.setMinimumSize(new Dimension(685, 6000));
		
		horizSplitPanel = new JSplitPane(JSplitPane.HORIZONTAL_SPLIT, buttonPanel, boardPanel);
		getContentPane().add(horizSplitPanel);
	}
	
	private void addEventListeners() {
		//newBoardItem.addActionListener(this);
		newCardItem.addActionListener(this);
		exitItem.addActionListener(this);
		addBoardButton.addActionListener(this);
		addCardButton.addActionListener(this);
		cancelAddBoardButton.addActionListener(this);
		cancelAddCardButton.addActionListener(this);
	}
	
	private JMenuBar menuBar;
	
	private JMenu fileMenu;
	private JMenu helpMenu;
	
	private JMenuItem newBoardItem;
	private JMenuItem newCardItem;
	private JMenuItem exitItem;
	
	private JPanel boardEntryPanel;
	private JPanel cardEntryPanel;
	private JPanel dataEntryPanel;
	private JPanel buttonPanel;
	
	private JTabbedPane boardPanel;
	
	private JTextField boardNameField;
	private JTextField cardNameField;
	
	private JButton addBoardButton;
	private JButton addCardButton;
	private JButton cancelAddBoardButton;
	private JButton cancelAddCardButton;
	
	private JSplitPane horizSplitPanel;
	private JSplitPane vertSplitPanel;

	public void windowActivated(WindowEvent we) {}
	
	public void windowClosed(WindowEvent we) {
		System.exit(0);
	}
	
	public void windowClosing(WindowEvent we) {}
	public void windowDeactivated(WindowEvent we) {}
	public void windowDeiconified(WindowEvent we) {}
	public void windowIconified(WindowEvent we) {}
	public void windowOpened(WindowEvent we) {}

	public void actionPerformed(ActionEvent ae) {
		if(ae.getSource().getClass().getName().equals("javax.swing.JMenuItem")) {
			JMenuItem i = (JMenuItem)ae.getSource();
			
			if(i.equals(newBoardItem)) {
				//dataEntryPanel.setVisible(true);
				cardEntryPanel.setVisible(false);
				boardEntryPanel.setVisible(true);
			} else
			if(i.equals(newCardItem)) {
				//dataEntryPanel.setVisible(true);
				cardEntryPanel.setVisible(true);
				boardEntryPanel.setVisible(false);
			} else
			if(i.equals(exitItem)) {
				System.exit(0);
			}
		} else
		if(ae.getSource().getClass().getName().equals("javax.swing.JButton")) {
			JButton i = (JButton)ae.getSource();
			
			if(i.equals(cancelAddBoardButton)) {
				//dataEntryPanel.setVisible(false);
				cardEntryPanel.setVisible(false);
				boardEntryPanel.setVisible(false);
			} else
			if(i.equals(cancelAddCardButton)) {
				//dataEntryPanel.setVisible(false);
				cardEntryPanel.setVisible(false);
				boardEntryPanel.setVisible(false);
			} else
			if(i.equals(addBoardButton)) {
				// TODO: Add stuff for more than one board
			} else
			if(i.equals(addCardButton)) {
				System.out.println("Add Card");
				dataEntryPanel.setVisible(false);
				cardEntryPanel.setVisible(false);
				boardEntryPanel.setVisible(false);
			}
		}
	}
	
	public void mouseClicked(MouseEvent me) {}
	public void mouseEntered(MouseEvent me) {}
	public void mouseExited(MouseEvent me) {}
	public void mousePressed(MouseEvent me) {}
	public void mouseReleased(MouseEvent me) {}
	
}
