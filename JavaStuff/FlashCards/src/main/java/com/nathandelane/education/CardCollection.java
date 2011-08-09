package com.nathandelane.education;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public final class CardCollection {

    private List<Card> cards;
    private int pointer;

    public CardCollection(Range[] operandRanges) {
	this.pointer = 0;
	this.cards = new ArrayList<Card>();

	populateCardCollection(operandRanges);

	Collections.shuffle(this.cards);
    }

    public int numberOfCards() {
	return this.cards.size();
    }

    public boolean hasNext() {
	return this.pointer < this.cards.size();
    }

    public Card getNext() {
	Card response = this.cards.get(this.pointer);

	this.pointer ++;

	return response;
    }

    public Card currentCard() {
	return this.cards.get(this.pointer);
    }

    public void reset() {
	Collections.shuffle(this.cards);

	this.pointer = 0;
    }

    private void populateCardCollection(Range[] operandRanges) {
	System.out.print("Loading cards");

	for (int leftOperand = operandRanges[0].getMin(); leftOperand < operandRanges[0].getMax(); leftOperand++) {
	    for (int rightOperand = operandRanges[1].getMin(); rightOperand < operandRanges[1].getMax(); rightOperand++) {
		this.cards.add(new Card(new int[] { leftOperand, rightOperand }, Operation.ADDITION, (leftOperand + rightOperand)));

		System.out.print(".");
	    }
	}

	System.out.println("FINISHED");
    }

}
