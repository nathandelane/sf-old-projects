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
	// TODO: Make this use ranges to populate the deck.
    }

}
