package com.nathandelane.education;

import javax.swing.UnsupportedLookAndFeelException;

public class Main {

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
	    e.printStackTrace();
	}
    }

}
