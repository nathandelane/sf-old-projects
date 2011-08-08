package com.nathandelane.education;

import java.awt.Font;
import java.awt.Insets;

import javax.swing.JButton;

public final class OptionButton extends JButton {

    private static final long serialVersionUID = 4478306351853837737L;

    public OptionButton(String label) {
	super(label);

	initialize();
    }

    private void initialize() {
	this.setFont(new Font("Times New Roman", Font.PLAIN, 24));
	this.setMargin(new Insets(8, 30, 8, 30));
    }

}
