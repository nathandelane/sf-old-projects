package com.nathandelane.education;

import java.awt.Font;

import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.SwingConstants;

public final class Card extends JPanel {

    private static final long serialVersionUID = -5463775878737359323L;

    private int[] operands;
    private Operation operation;
    private Integer correctResult;

    public Card(int[] operands, Operation operation, Integer correctResult) {
	super();

	this.operands = operands;
	this.operation = operation;

	initialize();
    }

    public int[] getOperands() {
	return this.operands;
    }

    public Integer getCorrectResult() {
	return this.correctResult;
    }

    private void initialize() {
	StringBuilder stringBuilder = new StringBuilder("<html><table>");

	for (int operandIndex = 0; operandIndex < this.operands.length; operandIndex++) {
	    Integer nextOperand = this.operands[operandIndex];

	    if (operandIndex == (this.operands.length - 1) && operandIndex != 0) {
		stringBuilder.append("<tr><td>" + this.operation + "</td><td>" + nextOperand.intValue() + "</td></tr>");
	    } else {
		stringBuilder.append("<tr><td></td><td>" + nextOperand.intValue() + "</td></tr>");
	    }
	}

	stringBuilder.append("</html><table>");

	JLabel label = new JLabel(stringBuilder.toString(), SwingConstants.RIGHT);
	label.setFont(new Font("Times New Roman", Font.PLAIN, 180));

	this.add(label);
    }

}
