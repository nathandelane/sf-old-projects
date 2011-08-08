package com.nathandelane.education;

import javax.swing.UnsupportedLookAndFeelException;

import org.apache.log4j.Logger;

public class Main {

    private static final Logger logger = Logger.getLogger(Main.class);

    private Main() throws ClassNotFoundException, InstantiationException, IllegalAccessException, UnsupportedLookAndFeelException {
	MainWindow flashCards = new MainWindow();
	flashCards.setVisible(true);
    }

    /**
     * @param args
     */
    public static void main(String[] args) {
	try {
	    new Main();
	} catch(Exception e) {
	    logger.fatal("Exception was caught from the MainWindow.", e);
	}
    }

}
