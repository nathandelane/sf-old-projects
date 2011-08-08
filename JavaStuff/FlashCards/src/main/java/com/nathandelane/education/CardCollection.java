package com.nathandelane.education;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public final class CardCollection {

    private List<Card> cards;
    private int pointer;

    @SuppressWarnings("serial")
    public CardCollection() {
	this.pointer = 0;
	this.cards = new ArrayList<Card>() {
	    {
		add(new Card(new int[] { 1, 1 }, Operation.ADDITION, 2));
		add(new Card(new int[] { 1, 2 }, Operation.ADDITION, 3));
		add(new Card(new int[] { 1, 3 }, Operation.ADDITION, 4));
		add(new Card(new int[] { 1, 4 }, Operation.ADDITION, 5));
		add(new Card(new int[] { 1, 5 }, Operation.ADDITION, 6));
		add(new Card(new int[] { 1, 6 }, Operation.ADDITION, 7));
		add(new Card(new int[] { 1, 7 }, Operation.ADDITION, 8));
		add(new Card(new int[] { 1, 8 }, Operation.ADDITION, 9));
		add(new Card(new int[] { 1, 9 }, Operation.ADDITION, 10));
		add(new Card(new int[] { 2, 1 }, Operation.ADDITION, 3));
		add(new Card(new int[] { 2, 2 }, Operation.ADDITION, 4));
		add(new Card(new int[] { 2, 3 }, Operation.ADDITION, 5));
		add(new Card(new int[] { 2, 4 }, Operation.ADDITION, 6));
		add(new Card(new int[] { 2, 5 }, Operation.ADDITION, 7));
		add(new Card(new int[] { 2, 6 }, Operation.ADDITION, 8));
		add(new Card(new int[] { 2, 7 }, Operation.ADDITION, 9));
		add(new Card(new int[] { 2, 8 }, Operation.ADDITION, 10));
		add(new Card(new int[] { 2, 9 }, Operation.ADDITION, 11));
	    }
	};

	Collections.shuffle(this.cards);
    }

    public boolean hasNext() {
	return this.pointer == (this.cards.size() - 1);
    }

    public Card getNext() {
	Card response = this.cards.get(this.pointer);

	this.pointer ++;

	return response;
    }

}
