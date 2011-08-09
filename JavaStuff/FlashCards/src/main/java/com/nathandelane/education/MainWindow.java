package com.nathandelane.education;

import java.awt.Dimension;
import java.awt.Font;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowEvent;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import javax.swing.Box;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.UIManager;
import javax.swing.UnsupportedLookAndFeelException;

import org.apache.log4j.Logger;

public final class MainWindow extends JFrame {

    private static final long serialVersionUID = -3629780808393423526L;
    private static final Logger logger = Logger.getLogger(MainWindow.class);

    private static CardCollection cardCollection;
    private static Card currentCard;
    private static List<Result> results;
    private static long initialTime;

    private int minimumWidth;
    private int minimumHeight;

    public MainWindow() throws ClassNotFoundException, InstantiationException, IllegalAccessException, UnsupportedLookAndFeelException {
	super("Nathandelane's Flash Cards");

	this.minimumWidth = 800;
	this.minimumHeight = 600;

	MainWindow.cardCollection = new CardCollection();
	MainWindow.results = new ArrayList<Result>();

	initializeWindow();
	initializeUi();
    }

    private void initializeWindow() throws ClassNotFoundException, InstantiationException, IllegalAccessException, UnsupportedLookAndFeelException {
	final Dimension minimumWindowSize = new Dimension(minimumWidth, minimumHeight);

	UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());

	this.setSize(minimumWindowSize);
	this.setMinimumSize(minimumWindowSize);
	this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
	this.setLocationByPlatform(true);
    }

    private void initializeUi() {
	this.setContentPane(Box.createVerticalBox());

	if (MainWindow.cardCollection.hasNext()) {
	    showNextCard();
	} else {
	    showResults();
	}
    }

    private void showResults() {
	JPanel resultsPanel = new JPanel();
	Box resultsPanelBox = Box.createVerticalBox();
	JLabel resultsLabel = new JLabel("Results");
	resultsLabel.setFont(new Font("Times New Roman", Font.PLAIN, 26));
	resultsPanelBox.add(resultsLabel);

	StringBuilder stringBuilder = new StringBuilder();
	stringBuilder.append("<html><table>");
	stringBuilder.append("<tr><td>Number of Problems:</td><td>" + MainWindow.cardCollection.numberOfCards() + "</td></tr>");

	int correct = 0;
	int incorrect = 0;

	for (Result nextResult : this.results) {
	    if (nextResult.getPassedOrFailed() == PassedOrFailed.FAILED) {
		incorrect++;
	    } else {
		correct++;
	    }
	}

	stringBuilder.append("<tr><td>Total Correct:</td><td>" + correct + "</td></tr>");
	stringBuilder.append("<tr><td>Total Incorrect:</td><td>" + incorrect + "</td></tr>");
	stringBuilder.append("</table></html>");

	resultsPanelBox.add(new JLabel(stringBuilder.toString()));
	resultsPanel.add(resultsPanelBox);

	Box buttonPanel = Box.createHorizontalBox();

	OptionButton optionButton = new OptionButton("Restart");
	optionButton.addActionListener(new ActionListener() {

	    public void actionPerformed(ActionEvent e) {
		if (e.getSource() instanceof OptionButton) {
		    OptionButton optionButton = (OptionButton) e.getSource();

		    if (optionButton.getText().equals("Restart")) {
			MainWindow.cardCollection.reset();
			MainWindow.this.initializeUi();
		    }
		}
	    }

	});

	OptionButton exitButton = new OptionButton("Exit");
	exitButton.addActionListener(new ActionListener() {

	    public void actionPerformed(ActionEvent e) {
		if (e.getSource() instanceof OptionButton) {
		    OptionButton optionButton = (OptionButton) e.getSource();

		    if (optionButton.getText().equals("Exit")) {
			MainWindow.this.dispatchEvent(new WindowEvent(MainWindow.this, WindowEvent.WINDOW_CLOSING));
		    }
		}
	    }

	});

	buttonPanel.add(optionButton);
	buttonPanel.add(Box.createRigidArea(new Dimension(60, 30)));
	buttonPanel.add(exitButton);

	this.add(resultsPanel);
	this.add(buttonPanel);

	this.pack();
    }

    private void showNextCard() {
	MainWindow.currentCard = MainWindow.cardCollection.getNext();

	this.getContentPane().add(MainWindow.currentCard);

	List<Integer> options = OptionsGenerator.generateOptionsFor(MainWindow.currentCard.getOperands(), Operation.ADDITION, 3);
	Box box = Box.createHorizontalBox();
	int optionCounter = 0;

	for (Integer nextOption : options) {
	    OptionButton nextOptionButton = new OptionButton(nextOption.toString());
	    nextOptionButton.addActionListener(new ActionListener() {

		public void actionPerformed(ActionEvent e) {
		    if (e.getSource() instanceof OptionButton) {
			long finishTime = Calendar.getInstance().getTimeInMillis();
			OptionButton optionButton = (OptionButton) e.getSource();
			String value = optionButton.getText();

			if (value.equals(MainWindow.currentCard.getCorrectResult().toString())) {
			    MainWindow.results.add(new Result(new Date(finishTime - MainWindow.initialTime), PassedOrFailed.PASSED));
			    MainWindow.logger.info(String.format("The correct answer was chosen: %1$s", optionButton.getText()));
			} else {
			    MainWindow.results.add(new Result(new Date(finishTime - MainWindow.initialTime), PassedOrFailed.FAILED));
			    MainWindow.logger.info(String.format("The INCORRECT answer was chosen: %1$s", optionButton.getText()));
			}

			MainWindow.this.initializeUi();
		    }
		}

	    });

	    box.add(nextOptionButton);

	    if (optionCounter < (options.size() - 1)) {
		box.add(Box.createRigidArea(new Dimension(10, 30)));
	    }

	    optionCounter++;
	}

	this.getContentPane().add(box);
	this.pack();

	MainWindow.initialTime = Calendar.getInstance().getTimeInMillis();
    }

}
