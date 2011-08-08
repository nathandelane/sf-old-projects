package com.nathandelane.education;

import java.awt.Dimension;
import java.util.List;

import javax.swing.Box;
import javax.swing.JFrame;
import javax.swing.UIManager;
import javax.swing.UnsupportedLookAndFeelException;

public final class MainWindow extends JFrame {

    private static final long serialVersionUID = -3629780808393423526L;

    private int minimumWidth;
    private int minimumHeight;
    private CardCollection cardCollection;

    public MainWindow() throws ClassNotFoundException, InstantiationException, IllegalAccessException, UnsupportedLookAndFeelException {
	super("Nathandelane's Flash Cards");

	this.minimumWidth = 800;
	this.minimumHeight = 600;
	this.cardCollection = new CardCollection();

	initializeWindow();
	initializeUi();

	this.pack();
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

	Card nextCard = this.cardCollection.getNext();

	this.getContentPane().add(nextCard);

	List<Integer> options = OptionsGenerator.generateOptionsFor(nextCard.getOperands(), Operation.ADDITION, 3);
	Box box = Box.createHorizontalBox();
	int optionCounter = 0;

	for (Integer nextOption : options) {
	    box.add(new OptionButton(nextOption.toString()));

	    if (optionCounter < (options.size() - 1)) {
		box.add(Box.createRigidArea(new Dimension(10, 30)));
	    }

	    optionCounter++;
	}

	this.getContentPane().add(box);
    }

}
